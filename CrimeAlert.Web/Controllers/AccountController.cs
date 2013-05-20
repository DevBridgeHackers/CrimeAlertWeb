using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CrimeAlert.Web.Models;

namespace CrimeAlert.Web.Controllers
{
    public partial class AccountController : Controller
    {
        public virtual ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public virtual ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if(Membership.ValidateUser(model.UserName, model.Password))
                {
                    var ticket = new FormsAuthenticationTicket(1, model.UserName, DateTime.Now, DateTime.Now.AddMonths(1), model.RememberMe, string.Empty);
                    var ticketHash = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketHash) { Expires = DateTime.Now.AddMonths(1) };
                    Response.Cookies.Add(cookie);

                    return RedirectToAction(MVC.Admin.Index());
                }
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        public virtual ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            // force session refresh
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            return RedirectToAction(MVC.Home.Index());
        }
    }
}
