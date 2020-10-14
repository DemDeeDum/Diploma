using Hotel.BLL.IdentityLogic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Interfaces
{
    public interface IAccountService //transferred identity logic form controllers 
    {
        Task<IdentityResult> AddNewUser(string userName, string email
            , string password, ApplicationUserManager UserManager);

        Task AddNewUser(ApplicationSignInManager SignInManager);

        Task AddNewUserToRole(ApplicationUserManager UserManager);

        Task<IdentityResult> ExternalLoginUserCreate(string userName, string email
             , ApplicationUserManager UserManager);

        Task<IdentityResult> ExternalLoginUserAddLoginAsync(ExternalLoginInfo info,
             ApplicationUserManager UserManager);

        Task ExternalLoginSignIn(ApplicationSignInManager SignInManager);

        Task<SignInStatus> PasswordSignInAsync
            (string email, string password, bool rememberMe
            , ApplicationSignInManager SignInManager);

        Task<bool> HasBeenVerifiedAsync(ApplicationSignInManager SignInManager);

        Task<SignInStatus> TwoFactorSignInAsync
            (string provider, string code, bool RememberMe, bool RememberBrowser
            , ApplicationSignInManager SignInManager);

        Task<IdentityResult> ConfirmEmailAsync(string userId, string code
            , ApplicationUserManager UserManager);

        Task<bool> IsEmailConfirmedAsync(string email
            , ApplicationUserManager UserManager);

        Task<bool> FindByNameAsync(string email
            , ApplicationUserManager UserManager);

        Task<IdentityResult> ResetPasswordAsync(string code
            , string password, ApplicationUserManager UserManager);

        Task<bool> GetVerifiedUserIdAsync(ApplicationSignInManager SignInManager);

        Task<IList<string>> GetValidTwoFactorProvidersAsync(ApplicationUserManager UserManager);

        Task<bool> SendTwoFactorCodeAsync(string SelectedProvider,
            ApplicationSignInManager SignInManager);

        Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo,
            bool isPersistent, ApplicationSignInManager SignInManager);

        void Dispose(ApplicationSignInManager SignInManager,
            ApplicationUserManager UserManager);
    }

}

