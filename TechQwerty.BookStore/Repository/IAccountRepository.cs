﻿using Microsoft.AspNetCore.Identity;
using TechQwerty.BookStore.Models;

namespace TechQwerty.BookStore.Repository
{
    public interface IAccountRepository
    {
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel);
        Task<SignInResult> PasswordSignInAsync(SignInModel signInModel);
        Task SignOutAsync();
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model);
        Task<IdentityResult> ConfirmEmailAsync(string uid, string token);
        Task GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task GenerateForgotPasswordTokenAsync(ApplicationUser user);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model);
    }
}