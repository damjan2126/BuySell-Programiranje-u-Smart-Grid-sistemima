using BuySell.Contracts.DTOs.Auth;
using BuySell.Contracts.DTOs.User;
using BuySell.Data.Entities;
using BuySell.Data.Resources;

namespace BuySell.Business.Services.Contracts
{
    public interface IUserService
    {
        Task<AuthenticateResponseDto?> AuthenticateAsync(AuthenticateRequestDto model);
        Task<bool> ChangePasswordAsync(UserChangePasswordDto requestDto, long id);
        Task<User?> CreateUserAsync(UserCreateDto dto);
        Task<UserListViewDto> GetAllUsersAsync(Query query);
        Task<User?> GetUserByIdAsync(long id, Query query);
        Task<AuthenticateResponseDto?> RefreshTokenAsync(string refreshToken);
        Task RevokeTokenAsync(long id, string tokenToRevoke);
        Task<bool> UpdateUserAsync(UserUpdateDto dto, long userId);
        Task<bool> ApproveSeller(long userId, long adminId);
        Task<bool> RejectSeller(long userId, long adminId);
        Task<UserStatus> GetCurrentStatus(long id);
    }
}