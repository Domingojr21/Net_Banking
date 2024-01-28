using NetBanking.Core.Application.Dtos.Account;
using NetBanking.Core.Application.Dtos.User;
using NetBanking.Core.Application.ViewModel.User;
using NetBanking.Core.Application.ViewModels.User;

namespace NetBanking.Application.Interfaces.Services
{

    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task SignOutAsync();
        Task UpdateUser(SaveUserViewModel user);
        Task<UserDTO> GetUserByUserName(string UserName);
        Task<bool> IsaValidUser(string UserName);
        Task<List<UserDTO>> GetAllUsers();
        Task ChangeUserStatus(string userName);
        Task<UserDTO> UpdateUser(UserDTO dto);
        Task UpdateUserByUserName(EditUserViewModel value);
        Task<UserDTO> GetUserByUserEmail(string Email);

        Task<RegisterRequest> GetUserById(string UserId);


    }
}