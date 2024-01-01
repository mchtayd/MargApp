using Microsoft.AspNetCore.Mvc;
using static MVC_WebInterface.Models.Shared.MvcInfos;

namespace MVC_WebInterface.Controllers
{
    public class BaseController : Controller
    {

        internal SessionUser GetSessionUser()
        {
            var user = HttpContext.User;
            string sicilNo = user.Identity.Name;
            int roleId = -1;
            string name = "-";
            if (user.Claims.Any())
            {
                //roleId = user.Claims.FirstOrDefault(x => x.Type.EndsWith("/identity/claims/role")).Value.ConInt();
                //name = user.Claims.FirstOrDefault(x => x.Type.EndsWith("/identity/claims/givenname")).Value;
            }
            return new SessionUser() { SicilNo = sicilNo, Name = name, RoleId = roleId };
        }

        internal string GetSessionParameter(string key) => HttpContext.Session.GetString(key);

        internal void SetSessionParameters(string key, string value) => HttpContext.Session.SetString(key, value);


    }
}
