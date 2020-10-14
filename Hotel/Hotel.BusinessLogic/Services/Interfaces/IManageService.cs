using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.BLL.IdentityLogic;
using Hotel.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Hotel.BLL.Interfaces
{
    public interface IManageService //перенес логику идентити из контроллеров сюда
    {
        Task<string> GetPhoneNumberAsync(string userId, ApplicationUserManager UserManager);

        bool IsAdmin(string userId, ApplicationUserManager UserManager);

        Task<bool> GetTwoFactorEnabledAsync(string userId, ApplicationUserManager UserManager);
        
        Task<IList<UserLoginInfo>> GetLoginsAsync(string userId, ApplicationUserManager UserManager);
        
        Task<IdentityResult> RemoveLoginAsync
            (string userId, UserLoginInfo userLoginInfo, ApplicationUserManager UserManager);
        
        Task FindByIdAsyncAndSignIn(string userId, bool isPersistent,
            bool rememberBrowser, ApplicationSignInManager SignInManager,
             ApplicationUserManager UserManager);

        Task GenerateChangePhoneNumberTokenAsync(string userId, string Number
            , ApplicationUserManager UserManager);

        Task SetTwoFactorEnabledAsync(string userId, bool enabled,
            ApplicationUserManager UserManager);

        Task<string> GenerateChangePhoneNumberTokenAsyncWithoutMessage(string userid, string phoneNumber,
            ApplicationUserManager UserManager);

        Task<IdentityResult> ChangePhoneNumberAsync(string userdId, string phoneNumber, string code,
           ApplicationUserManager UserManager);

        Task<IdentityResult> SetPhoneNumberAsync(string userId, string phoneNumber
            , ApplicationUserManager UserManager);

        Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword
            , string newPassword, ApplicationUserManager UserManager);

        Task<IdentityResult> AddPasswordAsync(string userId
            , string newPassword, ApplicationUserManager UserManager);

        Task<ApplicationUser> FindByIdAsync(string userId, ApplicationUserManager UserManager);

        Task<bool> CheckForNull(string userId, ApplicationUserManager UserManager);

        Task<string> GetPasswordHash(string userId, ApplicationUserManager UserManager);
        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login,
            ApplicationUserManager UserManager);

        void Dispose(ApplicationUserManager UserManager);

        bool HasPassword(string userId, ApplicationUserManager UserManager);

        bool HasPhoneNumber(string userId, ApplicationUserManager UserManager);
    }
}
