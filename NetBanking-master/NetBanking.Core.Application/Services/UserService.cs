using AutoMapper;
using NetBanking.Core.Application.Dtos.Account;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModels.User;
using NetBanking.Core.Application.Dtos.User;
using NetBanking.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.SavingAccount;
using NetBanking.Core.Application.ViewModel.User;
using System.Security.Principal;

namespace NetBanking.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ISavingAccountService _savingAccountService;

        public UserService(IAccountService accountService, IMapper mapper, ISavingAccountService savingAccountService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _savingAccountService = savingAccountService;
        }

        public async Task ChangeUserStatus(string userName)
        {
            await _accountService.ChangeUserStatus(userName);
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
            return userResponse;
        }
        public async Task UpdateUser(SaveUserViewModel user)
        {
            await _accountService.UpdateUser(user);
        }

        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }

        public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin)
        {

            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            var response = await _accountService.RegisterBasicUserAsync(registerRequest, origin);

            if (!vm.IsAdmin && !response.HasError)
            {
                await _savingAccountService.Add(new SaveSavingAccountVM
                {
                    Amount = vm.InitialAmount,
                    IsPrincipal = true,
                    UserNameofOwner = response.UserId,

                });
            }

            return response;

        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin)
        {
            ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(vm);
            return await _accountService.ForgotPasswordAsync(forgotRequest, origin);
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm)
        {
            ResetPasswordRequest resetRequest = _mapper.Map<ResetPasswordRequest>(vm);
            return await _accountService.ResetPasswordAsync(resetRequest);
        }

        public async Task<RegisterRequest> GetUserDTOAsync(string userId)
        {
            return await _accountService.GetUserById(userId);
        }

        public async Task<bool> IsaValidUser(string UserName)
        {
            return await _accountService.IsaValidUser(UserName);
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
           return await _accountService.GetAllUsers();
        }

        public async Task<UserDTO> UpdateUserByUserId(UserDTO dto)
        {
            return await _accountService.UpdateUser(dto);
        }
    }
}
