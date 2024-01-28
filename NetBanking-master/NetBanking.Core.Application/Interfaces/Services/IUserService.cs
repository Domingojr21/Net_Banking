using NetBanking.Core.Application.Dtos.Account;
using NetBanking.Core.Application.Dtos.User;
using NetBanking.Core.Application.ViewModel.User;
using NetBanking.Core.Application.ViewModels.User;
using System.ComponentModel;

namespace NetBanking.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm);
        Task SignOutAsync();
        Task UpdateUser(SaveUserViewModel user);
        Task<RegisterRequest> GetUserDTOAsync(string UserName);
         Task<bool> IsaValidUser(string UserName);
        Task<List<UserDTO>> GetAllUsers();
        Task ChangeUserStatus(string userName);
        Task<UserDTO> UpdateUserByUserId(UserDTO dto);
    }
}