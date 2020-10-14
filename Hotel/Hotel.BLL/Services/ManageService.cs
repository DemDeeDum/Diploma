using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.BLL.IdentityLogic;
using Hotel.DAL.Entities;
using Microsoft.AspNet.Identity;
using Hotel.BLL.Interfaces;

namespace Hotel.BLL.Services
{
    public class ManageService : IManageService
    {
        public Task<string> GetPhoneNumberAsync(string userId, ApplicationUserManager UserManager)
        {
            return UserManager.GetPhoneNumberAsync(userId);
        }
        public bool IsAdmin(string userId, ApplicationUserManager UserManager)
        {
            return UserManager.GetRoles(userId).Contains("admin");
        }
        public Task<bool> GetTwoFactorEnabledAsync(string userId, ApplicationUserManager UserManager)
        {
            return UserManager.GetTwoFactorEnabledAsync(userId);
        }
        public Task<IList<UserLoginInfo>> GetLoginsAsync(string userId, ApplicationUserManager UserManager)
        {
            return UserManager.GetLoginsAsync(userId);
        }
        public Task<IdentityResult> RemoveLoginAsync
            (string userId, UserLoginInfo userLoginInfo, ApplicationUserManager UserManager)
        {
            return UserManager.RemoveLoginAsync(userId, userLoginInfo);
        }
        public async Task FindByIdAsyncAndSignIn(string userId, bool isPersistent,
            bool rememberBrowser, ApplicationSignInManager SignInManager,
             ApplicationUserManager UserManager)
        {
            var user = await UserManager.FindByIdAsync(userId);
            if (user is null)
                return;
            await SignInManager.SignInAsync(user, isPersistent: isPersistent, rememberBrowser: rememberBrowser);
        }

        public async Task GenerateChangePhoneNumberTokenAsync(string userId, string Number
            , ApplicationUserManager UserManager)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(userId, Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = Number,
                    Body = "Ваш код безопасности: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
        }

        public Task SetTwoFactorEnabledAsync(string userId, bool enabled,
            ApplicationUserManager UserManager)
        {
            return UserManager.SetTwoFactorEnabledAsync(userId, enabled);
        }

        public Task<string> GenerateChangePhoneNumberTokenAsyncWithoutMessage(string userid, string phoneNumber,
            ApplicationUserManager UserManager)
        {
            return UserManager.GenerateChangePhoneNumberTokenAsync(userid, phoneNumber);
        }

        public Task<IdentityResult> ChangePhoneNumberAsync(string userdId, string phoneNumber, string code,
           ApplicationUserManager UserManager)
        {
            return UserManager.ChangePhoneNumberAsync(userdId, phoneNumber, code);
        }

        public Task<IdentityResult> SetPhoneNumberAsync(string userId, string phoneNumber
            , ApplicationUserManager UserManager)
        {
            return UserManager.SetPhoneNumberAsync(userId, phoneNumber);
        }

        public Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword
            , string newPassword, ApplicationUserManager UserManager)
        {
            return UserManager.ChangePasswordAsync(userId, oldPassword, newPassword);
        }

        public Task<IdentityResult> AddPasswordAsync(string userId
            , string newPassword, ApplicationUserManager UserManager)
        {
            return UserManager.AddPasswordAsync(userId, newPassword);
        }
        
        public async Task<ApplicationUser> FindByIdAsync (string userId, ApplicationUserManager UserManager)
        {
            return await UserManager.FindByIdAsync(userId);
        }

        public async Task<bool> CheckForNull(string userId, ApplicationUserManager UserManager)
        {
            return await FindByIdAsync(userId, UserManager) is null;
        }

        public async Task<string> GetPasswordHash(string userId, ApplicationUserManager UserManager)
        {
            return (await FindByIdAsync(userId, UserManager)).PasswordHash;
        }

        public Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login,
            ApplicationUserManager UserManager)
        {
            return UserManager.AddLoginAsync(userId, login);
        }

        public void Dispose(ApplicationUserManager UserManager)
        {
            UserManager.Dispose();
            UserManager = null;
        }

        public bool HasPassword(string userId, ApplicationUserManager UserManager)
        {
            var user = UserManager.FindById(userId);
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public bool HasPhoneNumber(string userId, ApplicationUserManager UserManager)
        {
            var user = UserManager.FindById(userId);
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }
    }
}
