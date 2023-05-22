using BuyAndSell.Contracts.DTOs.Auth;
using BuyAndSell.Contracts.DTOs.User;
using BuyAndSell.Data.Entities;
using BuyAndSell.Data.Resources;

namespace BuyAndSell.Business.Services.Contracts
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
    }
}