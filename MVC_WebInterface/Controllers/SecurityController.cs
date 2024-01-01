using Entity.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MVC_WebInterface.Controllers
{
    public class SecurityController : BaseController
    {
        private readonly LogService logService;
        private readonly LoginManager loginManager;
        private readonly PersonelManager personelManager;

        public SecurityController(LogService logService, LoginManager loginManager, PersonelManager personelManager)
        {
            this.logService = logService;
            this.loginManager = loginManager;
            this.personelManager = personelManager;
        }

        [AllowAnonymous]
        public ActionResult Login(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            Login login = await loginManager.LoginAsync(user.UserName, user.Password);
            //if (user.UserName == "admin" && user.Password == "12345") //12345
            if (login != null)
            {
                // TO Do Get Personnel From Database
                await SignInAsync(await personelManager.GetAsync(login.Sicil, login.Sicil));
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Security", new { message = "Lütfen Kullanıcı Adı ve Şifrenizi Eksiksiz Giriniz" });
            }
        }

        private async Task SignInAsync(Personel personnel)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, personnel.Sicil),
                    new Claim(ClaimTypes.GivenName, personnel.PersonelAdSoyad)
                };
                ClaimsIdentity userIdentity = new(claims, "Login");
                ClaimsPrincipal principal = new(userIdentity);
                await HttpContext.SignInAsync(principal);
            }
            catch (Exception ex)
            {
                await logService.ErrorAsync(ex.Message, nameof(SignInAsync), personnel.ToString());
            }
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
