using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Hotel.DAL.Entities;
using Hotel.BLL.Interfaces;
using Hotel.BLL.IdentityLogic;
using System.Collections.Generic;

namespace Hotel.BLL.Services
{
    public class AccountService : IAccountService
    {
        /// <summary>
        /// Gets or sets using for store current information
        /// It is used by methods in creating new users operations
        /// </summary>
        private ApplicationUser currentNewUser { get; set; }
        /// <summary>
        /// Gets or sets using for store current information
        /// It is used by methods in external logging operations
        /// </summary>
        private ApplicationUser currentExternalLoginUser { get; set; }
        /// <summary>
        /// Gets or sets using for store current information
        /// It is used by methods in reseting password operations
        /// </summary>
        private ApplicationUser currentResetPasswordUser { get; set; }
        private string currentUserId { get; set; }



        public Task<IdentityResult> AddNewUser(string userName, string email
            , string password, ApplicationUserManager UserManager)
        {
            currentNewUser = new ApplicationUser { UserName = userName, Email = email };
            return UserManager.CreateAsync(currentNewUser, password);
        }

        public Task AddNewUser(ApplicationSignInManager SignInManager)
        {
            return SignInManager.SignInAsync(currentNewUser, isPersistent: false, rememberBrowser: false);
        }

        public Task AddNewUserToRole(ApplicationUserManager UserManager)
        {
            return UserManager.AddToRoleAsync(currentNewUser.Id, "user");
        }

        public Task<IdentityResult> ExternalLoginUserCreate(string userName, string email
             , ApplicationUserManager UserManager)
        {
            currentExternalLoginUser = new ApplicationUser
            {
                UserName = userName
                ,
                Email = email
            };
            return UserManager.CreateAsync(currentExternalLoginUser);
        }

        public Task<IdentityResult> ExternalLoginUserAddLoginAsync(ExternalLoginInfo info,
             ApplicationUserManager UserManager)
        {
            return UserManager.AddLoginAsync(currentExternalLoginUser.Id, info.Login);
        }

        public Task ExternalLoginSignIn(ApplicationSignInManager SignInManager)
        {
            return SignInManager.SignInAsync(currentExternalLoginUser
                , isPersistent: false, rememberBrowser: false);
        }
        public Task<SignInStatus> PasswordSignInAsync
            (string email, string password, bool rememberMe, ApplicationSignInManager SignInManager)
        {
            return SignInManager.PasswordSignInAsync(email, password, rememberMe, shouldLockout: false);
        }

        public Task<bool> HasBeenVerifiedAsync(ApplicationSignInManager SignInManager)
        {
            return SignInManager.HasBeenVerifiedAsync();
        }

        public Task<SignInStatus> TwoFactorSignInAsync
            (string provider, string code, bool RememberMe, bool RememberBrowser
            , ApplicationSignInManager SignInManager)
        {
            return SignInManager.TwoFactorSignInAsync(provider, code, isPersistent: RememberMe, rememberBrowser: RememberBrowser);
        }

        public Task<IdentityResult> ConfirmEmailAsync(string userId, string code
            , ApplicationUserManager UserManager)
        {
            return UserManager.ConfirmEmailAsync(userId, code);
        }

        public async Task<bool> IsEmailConfirmedAsync(string email
            , ApplicationUserManager UserManager)
        {
            var user = await UserManager.FindByNameAsync(email);
            if (user is null)
                return false;
            return await UserManager.IsEmailConfirmedAsync(user.Id);
        }

        public async Task<bool> FindByNameAsync(string email, ApplicationUserManager UserManager)
        {
            currentResetPasswordUser = await UserManager.FindByNameAsync(email);
            if (currentResetPasswordUser is null)
                return false;
            return true;
        }

        public async Task<IdentityResult> ResetPasswordAsync(string code
            , string password, ApplicationUserManager UserManager)
        {
            return await UserManager.ResetPasswordAsync(currentResetPasswordUser.Id, code, password);
        }

        public async Task<bool> GetVerifiedUserIdAsync(ApplicationSignInManager SignInManager)
        {
            currentUserId = await SignInManager.GetVerifiedUserIdAsync();
            if (currentUserId is null)
                return false;
            return true;
        }

        public Task<IList<string>> GetValidTwoFactorProvidersAsync(ApplicationUserManager UserManager)
        {
            return UserManager.GetValidTwoFactorProvidersAsync(currentUserId);
        }

        public Task<bool> SendTwoFactorCodeAsync(string SelectedProvider,
            ApplicationSignInManager SignInManager)
        {
            return  SignInManager.SendTwoFactorCodeAsync(SelectedProvider);
        }

        public Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo,
            bool isPersistent, ApplicationSignInManager SignInManager)
        {
            return SignInManager.ExternalSignInAsync(loginInfo, isPersistent: isPersistent);
        }

        public void Dispose(ApplicationSignInManager SignInManager,
            ApplicationUserManager UserManager)
        {
            if (UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }

            if (SignInManager != null)
            {
                SignInManager.Dispose();
                SignInManager = null;
            }
        }
    }
}
