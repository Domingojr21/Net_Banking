using NetBanking.Core.Application.Dtos.Account;
using NetBanking.Core.Application.Dtos.User;
using NetBanking.Core.Application.Enums;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModels.User;
using NetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using NetBanking.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.User;

namespace NetBanking.Infrastructure.Identity.Service
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            
           var userList = await _userManager.Users.ToListAsync();
            List<UserDTO> userDTOList = new();
            foreach(var user in userList)
            {
                var userDto = new UserDTO();
          
                userDto.UserName = user.UserName;
                userDto.LastName = user.LastName;
                userDto.FirstName = user.Name;
                userDto.Cedula = user.Cedula;
                userDto.IsActive = user.IsActive;
                userDto.Email = user.Email;
                userDto.Phone = user.PhoneNumber;
                userDto.UserId = user.Id;
                userDto.Roles = _userManager.GetRolesAsync(user).Result.ToList();
                userDTOList.Add(userDto);
            }
            return userDTOList;
        }

        public async Task ChangeUserStatus(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user.IsActive == true)
            {
                user.IsActive = false;
                await _userManager.UpdateAsync(user);
            }
            else
            {
                user.IsActive = true;
                await _userManager.UpdateAsync(user);
            }
           
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();



            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.Email}";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account no confirmed for {request.Email}";
                return response;
            }
            if (!user.IsActive)
            {
                response.HasError = true;
                response.Error = $"Account Is Inactived for {request.Email}. You need to Contact The Admin 'domingoadmin@email.com'";
                return response;
            }

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;

            return response;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> IsaValidUser(string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                return false;
            }
            return true;
        }


        //TENGO QUE USAR AUTOMAPPER EN ESTE METODO QUE NO SE ME OLVIDE
        public async Task<UserDTO> GetUserByUserName(string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                return null;
            }
            UserDTO userDTO = new();
            userDTO.UserName = user.UserName;
            userDTO.LastName = user.LastName;
            userDTO.FirstName = user.Name;
            userDTO.Phone = user.PhoneNumber;
            userDTO.UserId = user.Id;
            return userDTO;
        }

        public async Task<UserDTO> GetUserByUserEmail(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return null;
            }
            UserDTO userDTO = new();
            userDTO.Email=user.Email;
            userDTO.UserName = user.UserName;
            userDTO.LastName = user.LastName;
            userDTO.FirstName = user.Name;
            userDTO.Phone = user.PhoneNumber;
            userDTO.Cedula = user.Cedula;
            return userDTO;
        }

        public async Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username '{request.UserName}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' is already registered.";
                return response;
            }

            var user = new AppUser
            {
                Email = request.Email,
                Name = request.FirstName,
                LastName = request.LastName,
                EmailConfirmed = true,
                Cedula = request.Cedula,
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                PhoneNumberConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if(request.IsAdmin)
            {
                await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }
            else
            {
                await _userManager.AddToRoleAsync(user, Roles.Client.ToString());
            }
            var userForId = await _userManager.FindByEmailAsync(request.Email);

            response.UserId = userForId.Id;
            return response;
        }

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No accounts registered with this user";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirmed for {user.Email}. You can now use the app";
            }
            else
            {
                return $"An error occurred wgile confirming {user.Email}.";
            }
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email}";
                return response;
            }

            var verificationUri = await SendForgotPasswordUri(user, origin);

            await _emailService.SendAsync(new Core.Application.DTOs.Email.EmailRequest()
            {
                To = user.Email,
                Body = $"Please reset your account visiting this URL {verificationUri}",
                Subject = "reset password"
            });


            return response;
        }

        public async Task UpdatePassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);


            user.PasswordHash = request.Password;

        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email}";
                return response;
            }

            request.Token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred while reset password";
                return response;
            }

            return response;
        }
        
        public async Task UpdateUser(SaveUserViewModel user)
        {
            var oldUser = await _userManager.FindByEmailAsync(user.Email);
            await _userManager.UpdateAsync(oldUser);
        }

        private async Task<string> SendVerificationEmailUri(AppUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

            return verificationUri;
        }
        private async Task<string> SendForgotPasswordUri(AppUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ResetPassword";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);

            return verificationUri;
        }

        public async Task<UserDTO> UpdateUser(UserDTO dto)
        {
            AppUser value = await _userManager.FindByIdAsync(dto.UserId);
            value.UserName = dto.UserName;  
            value.Email = dto.Email;
            value.Name = dto.FirstName;
            value.LastName = dto.LastName;
            value.Cedula = dto.Cedula;
            value.UserName = dto.UserName;
            value.PhoneNumber = dto.Phone;

            //lo Hago para obtener el Id

            dto.UserId = value.Id;
            
            await _userManager.UpdateAsync(value);

            return dto;
        }
        public async Task UpdateUserByUserName(EditUserViewModel vm)
        {
            AppUser value = await _userManager.FindByIdAsync(vm.UserId);
            value.Email = vm.Email;
            value.Name = vm.FirstName;
            value.LastName = vm.LastName;
            value.Cedula = vm.Cedula;
            value.UserName = vm.Username;

            await _userManager.UpdateAsync(value);
        }

        public async Task<RegisterRequest> GetUserById(string UserId)
        {

            var user = await _userManager.FindByIdAsync(UserId);

            if (user == null)
            {
                return null;
            }
            RegisterRequest userDTO = new();
            userDTO.Email = user.Email;
            userDTO.UserName = user.UserName;
            userDTO.LastName = user.LastName;
            userDTO.FirstName = user.Name;
            userDTO.Phone = user.PhoneNumber;
            userDTO.Cedula = user.Cedula;

            return userDTO;

        }
    }

}

