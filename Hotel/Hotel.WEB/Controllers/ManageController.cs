// <copyright file="ManageController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.WEB.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using Hotel.BLL.DTOs;
    using Hotel.BLL.IdentityLogic;
    using Hotel.BLL.Interfaces;
    using Hotel.BLL.Services;
    using Hotel.WEB.Models.Identity;
    using Hotel.WEB.Models.Profile;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IManageService manageService;
        private IProfileService profileService;

        public ManageController(IProfileService profileService
            , IManageService manageService)
        {
            this.manageService = manageService;
            this.profileService = profileService;
        }

        public ManageController(ApplicationUserManager userManager
            , ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            profileService = new ProfileService();
            manageService = new ManageService();
        }


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index

         public ActionResult ChangeName(string UserName)
        {

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize (Roles = "user")]
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            this.ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "The password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Настроен поставщик двухфакторной проверки подлинности."
                : message == ManageMessageId.Error ? "Error happened."
                : message == ManageMessageId.AddPhoneSuccess ? "Ваш номер телефона добавлен."
                : message == ManageMessageId.RemovePhoneSuccess ? "Ваш номер телефона удален."
                : message == ManageMessageId.Paid ? "You have just paid successfully"
                : string.Empty;

            var userId = this.User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = this.HasPassword(),
                PhoneNumber = await this.manageService.GetPhoneNumberAsync(userId, this.UserManager),
                TwoFactor = await this.manageService.GetTwoFactorEnabledAsync(userId, this.UserManager),
                Logins = await this.manageService.GetLoginsAsync(userId, this.UserManager),
                BrowserRemembered = await this.AuthenticationManager.TwoFactorBrowserRememberedAsync(userId),
                ActiveBookings = this.profileService.GetActiveUserBooking(userId).
                Select(Mapper.Map<BookingDTO, BookingViewModel>).OrderBy(x => x.BegginingTime).ToList(),
                BookingsToConfirm = this.profileService.GetUserBookingToConfirm(userId).
                Select(Mapper.Map<BookingDTO, BookingViewModel>).OrderBy(x => x.BegginingTime).ToList(),
                PastBookings = this.profileService.GetPastUserBooking(userId).
                Select(Mapper.Map<BookingDTO, BookingViewModel>).OrderBy(x => x.BegginingTime).ToList(),
            };
            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<ActionResult> Index(string bookingId)
        {
            profileService.PayForRoom(int.Parse(bookingId));
            return RedirectToAction("Index", new { Message = ManageMessageId.Paid });
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await manageService.RemoveLoginAsync(User.Identity.GetUserId()
                , new UserLoginInfo(loginProvider, providerKey), UserManager);
            if (result.Succeeded)
            {
                await manageService.FindByIdAsyncAndSignIn(User.Identity.GetUserId(),
                    isPersistent: false, rememberBrowser: false, SignInManager,
                    UserManager);

                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Создание и отправка маркера
            await manageService.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(),
                model.Number, UserManager);

            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await manageService.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true, UserManager);
            await manageService.FindByIdAsyncAndSignIn(User.Identity.GetUserId(), false, false, SignInManager, UserManager);
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await manageService.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false, UserManager);

            await manageService.FindByIdAsyncAndSignIn(User.Identity.GetUserId(), false, false, SignInManager, UserManager);
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await manageService.GenerateChangePhoneNumberTokenAsyncWithoutMessage
                (User.Identity.GetUserId(), phoneNumber, UserManager);
            // Отправка SMS через поставщик SMS для проверки номера телефона
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await manageService.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code
                , UserManager);
            if (result.Succeeded)
            {
                await manageService.FindByIdAsyncAndSignIn(User.Identity.GetUserId(), false, false, SignInManager, UserManager);
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // Это сообщение означает наличие ошибки; повторное отображение формы
            ModelState.AddModelError("", "Не удалось проверить телефон");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await manageService.SetPhoneNumberAsync(User.Identity.GetUserId(), null, UserManager);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            await manageService.FindByIdAsyncAndSignIn(User.Identity.GetUserId(), false, false, SignInManager, UserManager);
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await manageService.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword
                , model.NewPassword, UserManager);
            if (result.Succeeded)
            {
                await manageService.FindByIdAsyncAndSignIn(User.Identity.GetUserId(), false, false, SignInManager, UserManager);
                if (User.IsInRole("admin"))
                    return RedirectToAction("UserManagment", "Admin", new { message = "Your password has been changed" });
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await manageService.AddPasswordAsync(User.Identity.GetUserId()
                    , model.NewPassword, UserManager);
                if (result.Succeeded)
                {
                    await manageService.FindByIdAsyncAndSignIn(User.Identity.GetUserId(), false, false, SignInManager, UserManager);
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // Это сообщение означает наличие ошибки; повторное отображение формы
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "Внешнее имя входа удалено."
                : message == ManageMessageId.Error ? "Произошла ошибка."
                : "";
            if (await manageService.CheckForNull(User.Identity.GetUserId(), UserManager))
            {
                return View("Error");
            }
         
            var userLogins = await manageService.GetLoginsAsync(User.Identity.GetUserId(), UserManager);
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = 
                await manageService.GetPasswordHash(User.Identity.GetUserId(), UserManager) != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Запрос перенаправления к внешнему поставщику входа для связывания имени входа текущего пользователя
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }

            var result = await manageService.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login
                , UserManager);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                manageService.Dispose(_userManager);
            }

            base.Dispose(disposing);
        }

#region Вспомогательные приложения
        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            return manageService.HasPassword(User.Identity.GetUserId(), UserManager);
        }

        private bool HasPhoneNumber()
        {
            return manageService.HasPhoneNumber(User.Identity.GetUserId(), UserManager);
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Paid,
            Error
        }

#endregion
    }
}