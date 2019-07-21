using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MemberPortal.Models;
using MemberPortal.Repository;

namespace MemberPortal.Controllers
{
    public class ProfileController : Controller
    {
        ProfileRepo context = new ProfileRepo();
        
        public ActionResult ViewProfile()
        {
            ViewBag.Acctname = Global.AccountName;
            ViewBag.Cifkey = Global.Cifkey;
            ViewBag.LastName = Global.LastName;
            ViewBag.FirstName = Global.FirstName;
            ViewBag.MiddleName = Global.MiddleName;
            ViewBag.Gender = Global.Gender;
            ViewBag.Birthday = Global.Birthdate.ToShortDateString();
            ViewBag.EmailAddress = Global.EmailAddress;
            return View();
        }

        public ActionResult UpdateAccount()
        {
            string emailAdd = Global.EmailAddress;
            Session["EmailAdd"] = emailAdd.Remove(1, 4).Insert(1, "xxxxxxxxxxx");
            return View();
        }

        //Check if old password entered is correct
        [HttpPost]
        public JsonResult CheckOldPasswordInput(string _password)
        {
            return Json(context.CheckInputPassword(Global.ID, _password));
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAccount(ChangePasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                else
                {
                    //Update password based on accountname and GUID
                    string newpassword = Crypto.Hash(model.NewPassword);
                    context.UpdatePassword(Global.AccountName, Global.AcctGuid, newpassword);
                    return RedirectToAction("SuccessUpdate", "Account");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }





    }
}