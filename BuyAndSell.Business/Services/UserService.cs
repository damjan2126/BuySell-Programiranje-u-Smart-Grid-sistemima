using AutoMapper;
using BuySell.Contracts.Exceptions;
using BuySell.Business.Services.Contracts;
using BuySell.Business.Settings;
using BuySell.Contracts.DTOs.Auth;
using BuySell.Contracts.DTOs.User;
using BuySell.Data.Entities;
using BuySell.Data.Enums;
using BuySell.Data.Repositories.Contracts;
using BuySell.Data.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IUserStatusRepository _userStatusRepository;
        private readonly AuthorizationSettings _authSettings;
        private readonly IMapper _mapper;

        public UserService(IOptions<AuthorizationSettings> authSettings,
                           SignInManager<User> signInManager,
                           UserManager<User> userManager,
                           IUserRepository userRepository,
                           IUserStatusRepository userStatusRepository,
                           IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
            _userStatusRepository = userStatusRepository;
            _authSettings = authSettings.Value;
            _mapper = mapper;
        }

        public async Task<AuthenticateResponseDto?> AuthenticateAsync(AuthenticateRequestDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (!result.Succeeded)
                return null;

            var user = await _userManager.FindByNameAsync(model.UserName)!;

            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateJwtToken(user, roles, DateTime.Now.AddMinutes(_authSettings.ExpiryInMinutes));
            var refresh = GenerateRefreshToken();

            user.RefreshTokens.Add(new()
            {
                Token = refresh,
                RefreshTokenExpiryTime = DateTime.Now.AddMinutes(_authSettings.RefreshTokenExpiryInMinutes)
            });
            user.LastLoginDate = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return new AuthenticateResponseDto
            {
                Token = token,
                RefreshToken = refresh
            };
        }

        public async Task<AuthenticateResponseDto?> RefreshTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users
                .Include(x => x.RefreshTokens)
                .FirstOrDefaultAsync(x => x.RefreshTokens.Any(y => y.Token == refreshToken)) ??
                throw new InvalidTokenException("Invalid token");

            var oldRefreshToken = user.RefreshTokens.FirstOrDefault(x =>
                x.Token == refreshToken && x.RefreshTokenExpiryTime - DateTime.Now > TimeSpan.Zero) ??
                throw new InvalidTokenException("Invalid token");

            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateJwtToken(user, roles, DateTime.Now.AddMinutes(_authSettings.ExpiryInMinutes));
            var refresh = GenerateRefreshToken();

            user.RefreshTokens.Remove(oldRefreshToken);
            user.RefreshTokens.Add(new()
            {
                Token = refresh,
                RefreshTokenExpiryTime = DateTime.Now.AddMinutes(_authSettings.RefreshTokenExpiryInMinutes)
            });
            await _userManager.UpdateAsync(user);

            return new AuthenticateResponseDto
            {
                Token = token,
                RefreshToken = refresh
            };
        }

        public async Task RevokeTokenAsync(long id, string tokenToRevoke)
        {
            var user = await _userManager.Users.Include(x => x.RefreshTokens).FirstOrDefaultAsync(x => x.Id == id) ??
                throw new InvalidTokenException("Invalid token");

            var oldRefreshToken = user.RefreshTokens.FirstOrDefault(x => x.Token == tokenToRevoke) ??
                throw new InvalidTokenException("Invalid token");

            user.RefreshTokens.Remove(oldRefreshToken);
            await _userManager.UpdateAsync(user);
        }

        public async Task<UserListViewDto> GetAllUsersAsync(Query query)
        {
            query.AsNoTracking = true;
            var users = await _userRepository.GetAllAsync(query);
            long count = await _userRepository.CountAllAsync(query);

            var response = new UserListViewDto()
            {
                Count = count,
                Users = _mapper.Map<IEnumerable<UserViewDto>>(users)
            };
            return response;
        }

        public async Task<User?> GetUserByIdAsync(long id, Query query)
        {
            return await _userManager.Users
                .Include(u => u.Roles)
                .Include(x => x.RefreshTokens)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> CreateUserAsync(UserCreateDto dto)
        {
            var existingUserEmail = await _userRepository.GetAsync(x => x.Email.Equals(dto.Email));
            var existingUserUserName = await _userRepository.GetAsync(x => x.UserName.Equals(dto.UserName));

            if (existingUserEmail is not null) throw new AlreadyExistsException("Email is in use");
            if (existingUserUserName is not null) throw new AlreadyExistsException("Username is in use");

            var user = _mapper.Map<User>(dto);
            user.CreatedAtUtc = DateTime.UtcNow;

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded) throw new DatabaseException("Greška prilikom kreiranja novog korisnika");

            result = await _userManager.AddToRoleAsync(user, dto.Role);

            if (!result.Succeeded) throw new DatabaseException("Greška prilikom dodavanja uloge korisniku");

            if (dto.Role.Equals(RoleEnum.Buyer.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                await _userManager.AddToRoleAsync(user, RoleEnum.Active.ToString());
                return user;
            }

            user = await _userRepository.GetAsync(x => x.UserName == dto.UserName) ??
                throw new NotFoundException("Nije pronadjes korisnik sa datim username-om");

            UserStatus status = new()
            {
                CreatedByUser = user,
                UpdatedByUser = user,
                User = user,
                Status = UserStatusEnum.Processing
            };

            await _userStatusRepository.CreateAsync(status);

            return user;
        }

        public async Task<bool> UpdateUserAsync(UserUpdateDto dto, long id)
        {
            var query = new Query();
            var user = await GetUserByIdAsync(id, query) ??
                throw new NotFoundException("Nije pronađen korisnik");

            user = _mapper.Map<User>(dto);

            var result = await _userManager.UpdateAsync(user);

            return !result.Succeeded
                ? throw new DatabaseException("Greška prilikom izmene korisnika")
                : true;
        }

        public async Task<bool> ChangePasswordAsync(UserChangePasswordDto requestDto, long id)
        {
            var query = new Query();
            var user = await GetUserByIdAsync(id, query) ??
                throw new NotFoundException("Nije pronađen korisnik");

            await _userManager.RemovePasswordAsync(user);

            var result = await _userManager.AddPasswordAsync(user, requestDto.Password);

            return !result.Succeeded
                ? throw new DatabaseException("Greška prilikom izmene sifre")
                : true;
        }

        public async Task<bool> ApproveSeller(long userId, long adminId)
        {
            var status = await _userStatusRepository.GetCurrentStatus(userId) ??
                throw new NotFoundException("Nije pronadjen status za korisnika sa zadatim id-jem");

            if (status.Status != UserStatusEnum.Processing)
            {
                throw new MethodNotAllowedException("Korisnik nije u stanju processing");
            }

            UserStatus newStatus = new()
            {
                CreatedByUserId = adminId,
                UpdatedByUserId = adminId,
                UserId = userId,
                Status = UserStatusEnum.Accepted
            };

            await _userStatusRepository.CreateAsync(newStatus);

            var user = await GetUserByIdAsync(userId, new Query()) ??
                throw new NotFoundException("Nije pronadjen korisnik sa datim id-jem");

            await _userManager.AddToRoleAsync(user, RoleEnum.Active.ToString());

            return true;
        }

        public async Task<bool> RejectSeller(long userId, long adminId)
        {
            var status = await _userStatusRepository.GetCurrentStatus(userId) ??
                throw new NotFoundException("Nije pronadjen status za korisnika sa zadatim id-jem");

            if (status.Status != UserStatusEnum.Processing)
            {
                throw new MethodNotAllowedException("Korisnik nije u stanju processing");
            }

            UserStatus newStatus = new()
            {
                CreatedByUserId = adminId,
                UpdatedByUserId = adminId,
                UserId = userId,
                Status = UserStatusEnum.Rejected
            };

            await _userStatusRepository.CreateAsync(newStatus);

            return true;
        }

        private string GenerateJwtToken(User user, IEnumerable<string> userRoles, DateTime expirationTime)
        {
            var key = Encoding.ASCII.GetBytes(_authSettings.Secret);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var claims = new List<Claim>
            {
                new(ClaimTypeEnum.Id.ToString(), user.Id.ToString())
            };
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypeEnum.Roles.ToString(), role)));
            var token = new JwtSecurityToken(
                claims: claims,
                expires: expirationTime,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
