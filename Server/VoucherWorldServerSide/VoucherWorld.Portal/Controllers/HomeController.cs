using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Repository.Pattern.Ef6;
using VoucherWorld.Data;
using VoucherWorld.Data.Entities;
using VoucherWorld.Data.Enums;

namespace VoucherWorld.Portal.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(User inputUser)
        {
            try
            {
                UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());

                var user =
                    myUnitOfWork.Repository<User>()
                        .Query(u => u.UserName == inputUser.UserName && u.Password == inputUser.Password)
                        .Select()
                        .ToList()
                        .First();
                if (user == null)
                {
                    return View();
                }
                else if (user.UserType == UserType.MerchantManager)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("Dashboard", "Manager");
                }
                else if (user.UserType == UserType.MerchantClient)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("CheckGiftCode", "Clients");
                }
                else
                {
                    return View();
                }
            }
            catch (System.Exception ex)
            {
                return View();
            }
        }

    }
}
