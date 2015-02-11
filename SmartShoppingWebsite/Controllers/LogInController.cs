using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartShoppingWebsite.Models;
using System.Web.Security;

namespace SmartShoppingWebsite.Controllers
{
    public class LogInController : Controller
    {
        // GET: LogIn
        SmartShoppingEntities db = new SmartShoppingEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Members member)
        {
            //先做一個salt再存Password
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            byte[] buf = new byte[15];
            rng.GetBytes(buf);
            string salt = Convert.ToBase64String(buf);
            member.Salt = salt;
            string Password = member.Password;
            Password = FormsAuthentication.HashPasswordForStoringInConfigFile(Password + salt, "sha1");
            member.Password = Password;
            
            db.Members.Add(member);
            db.SaveChanges();
            return View();
        }

    }
}