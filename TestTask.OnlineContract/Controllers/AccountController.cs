using Domain.Auction.Model;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestTask.OnlineContract.Hubs;
using TestTask.OnlineContract.Models;

namespace TestTask.OnlineContract.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;
                using (AuctionContext db = new AuctionContext())
                {

                    user = db.Users.FirstOrDefault(u => u.Name == model.Login);

                }

                if (user != null)
                {
                    var sha = new SHA1CryptoServiceProvider();
                    var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(user.PasswordHash + model.BigNumber));
                    var hashString = BitConverter.ToString(hash).Replace("-", "");

                    if (hashString == model.HashValue.ToUpper())
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);

                        DefaultHubManager hd = new DefaultHubManager(GlobalHost.DependencyResolver);
                        var hub = hd.ResolveHub("AuctionHub") as AuctionHub;
                        if (hub != null)
                        {
                            hub.AddUser(user);
                        }

                        return Json(new {success = true });
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Logoff()
        {
            DefaultHubManager hd = new DefaultHubManager(GlobalHost.DependencyResolver);
            var hub = hd.ResolveHub("AuctionHub") as AuctionHub;
            if (hub != null)
            {
                hub.DeleteUser(User.Identity.Name);
            }

            FormsAuthentication.SignOut();
            return Json(new { success = true });
        }
    }
}