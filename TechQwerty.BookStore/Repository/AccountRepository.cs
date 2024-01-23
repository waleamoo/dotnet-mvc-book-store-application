﻿using Microsoft.AspNetCore.Identity;
using TechQwerty.BookStore.Models;
using TechQwerty.BookStore.Service;

namespace TechQwerty.BookStore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IUserService userService, IEmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel)
        {
            var user = new ApplicationUser()
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                UserName = userModel.Email
            };
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                await GenerateEmailConfirmationTokenAsync(user);
            }
            return result;
        }

        public async Task GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var token = _userManager.GenerateEmailConfirmationTokenAsync(user); // generate the token using in-built Identity GenerateEmailConfirmationTokenAsync 
            if (!string.IsNullOrEmpty(token.ToString()))
            {
                await SendEmailConfirmationEmail(user, token.Result.ToString());
            }
        }
        
        public async Task GenerateForgotPasswordTokenAsync(ApplicationUser user)
        {
            var token = _userManager.GeneratePasswordResetTokenAsync(user); // generate the token using in-built Identity GenerateEmailConfirmationTokenAsync 
            if (!string.IsNullOrEmpty(token.ToString()))
            {
                await SendForgotPasswordEmail(user, token.Result.ToString());
            }
        }

        public async Task<SignInResult> PasswordSignInAsync(SignInModel signInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, false);
            return result;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            var userId = _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId) ?? throw new Exception("User not found");
            return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }
        
        public async Task<IdentityResult> ConfirmEmailAsync(string uid, string token)
        {
            return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);
        }
        
        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            return await _userManager.ResetPasswordAsync(await _userManager.FindByIdAsync(model.UserId), model.Token, model.NewPassword);
        }

        private async Task SendEmailConfirmationEmail(ApplicationUser user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value ?? "";
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value ?? "";

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{ UserName }}", user.FirstName),
                    new KeyValuePair<string, string>("{{ Link }}", string.Format(appDomain + confirmationLink, user.Id, token)),
                }
            };
            await _emailService.SendEmailForConfirmation(options);
        }

        private async Task SendForgotPasswordEmail(ApplicationUser user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value ?? "";
            string confirmationLink = _configuration.GetSection("Application:ForgotPassword").Value ?? "";

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{ UserName }}", user.FirstName),
                    new KeyValuePair<string, string>("{{ Link }}", string.Format(appDomain + confirmationLink, user.Id, token)),
                }
            };
            await _emailService.SendEmailForForgotPassword(options);
        }

    }
}
