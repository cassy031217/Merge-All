using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MemberPortal.Models;
using MemberPortal.Interface;
using MemberPortal.Repository;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web.Security;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;

namespace MemberPortal.Controllers
{
    public class MemberAccountController : Controller
    {

        //call repository
        private IRegisterRepo regClass;


        public MemberAccountController()
        {
            this.regClass = new RegisterRepo(new PORTALEntities());
        }

        public MemberAccountController(IRegisterRepo _registerRepository)
        {
            this.regClass = _registerRepository;
        }



        //TEST CONNECTION FOR THIS

        public async Task<ActionResult> Index()
        {
            HttpClient client;
            //string url = "http://localhost:11905/api/CIFOnlineUsers1";
            string url = "http://localhost:11905/api/CIFOnlineUser";
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            int id = 16;
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var employee = JsonConvert.DeserializeObject<CIFOnlineUser>(responseData);

                return View(employee);
            }
            return View("Error");
        }



        // **** LOGIN **** //
        public ActionResult Login(string returnUrl)
        {


            try
            {
                if (this.Request.IsAuthenticated)
                {
                    HttpCookie usernameCookie = Request.Cookies["username"];
                    HttpCookie acctCookie = Request.Cookies["acctName"];
                    if (usernameCookie != null)
                    {
                        Global.UserName = usernameCookie.Value;
                        Global.AccountName = acctCookie.Value;
                    }

                    return this.RedirectToLocal(returnUrl);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return this.View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                //Call Login Repo
                LoginRepo logs = new LoginRepo();

                // If Data is Valid.
                if (ModelState.IsValid)
                {
                
                    HttpClient client = new HttpClient();
                    string url = "http://localhost:11905/api/CIFOnlineUser";
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(url + "/" + model.Username + "/" + model.Password);


                        if (response.IsSuccessStatusCode)
                        {
                            //continue business logic

                            var responseData = response.Content.ReadAsStringAsync().Result;
                            var employee = JsonConvert.DeserializeObject<EmpData>(responseData);

                            Global.ID = employee.ID;
                            Global.LastName = employee.FirstName.ToString();
                            Global.MiddleName = employee.MiddleName.ToString();
                            Global.FirstName = employee.FirstName.ToString();
                            Global.AccountName = employee.AccountName.ToString();
                            Global.Cifkey = employee.Cifkey.ToString();
                            Global.LastLoginDate = employee.LastLoginDate;
                            Global.Birthdate = employee.Birthdate;
                            Global.EmailAddress = employee.EmailAddress.ToString();
                            Global.UserName = employee.Username.ToString();
                            Global.Gender = employee.Sex.ToString();
                            Global.Branchcode = employee.Branchcode.ToString();
                            Global.AcctGuid = employee.GUID;

                            //Insert Login Logs
                            //logs.InsertLoginLog(employee.ID);

                            //Update cifonlineusers lastlogindate                         
                            logs.UpdateClientLoginDate(employee.ID, employee.GUID);


                            //create  cookies account
                            HttpCookie usernameCookie = new HttpCookie("username", Global.UserName);
                            Response.Cookies.Add(usernameCookie);
                            HttpCookie AcctNameCookie = new HttpCookie("acctName", Global.AccountName);
                            Response.Cookies.Add(AcctNameCookie);

                            //Apply OWIN authentication
                            this.SignInUser(model.Username, false);

                            return this.RedirectToLocal(returnUrl);

                        }
                        else
                        {

                        //**********************************************************************************************//
                        // 3 Attempt login  NOT YET FINISH
                        //**********************************************************************************************//
                            Session["LoginCount"] = Convert.ToInt32(Session["LoginCount"]) + 1;
                            if (Convert.ToInt32(Session["LoginCount"]) == 3)
                            {

                              //Change Account Status to Locked and client must call coop. based in username ang ma locked nya
                              //www.aspdotnet-pools.com/2017/03/lock-user-account-on-three-failed-login.html
                              
                              //DeActivate Account()
                              ModelState.AddModelError(string.Empty, "Login Failed. You are not an Authorized User.");
                              return View(model);
                            }
                            else
                            {
                               int countattempt = 3 - Convert.ToInt32(Session["LoginCount"]);
                               ModelState.AddModelError(string.Empty, "Login Failed. Require correct username and password. " + countattempt + " Attempt Remaining.");
                              return View(model);
                            }

                    }

                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Credential Provided.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        // **** LOGIN **** //

  

        // **** REGISTER **** //

        public ActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel ObjModel)
        {
            if (ModelState.IsValid)
            {
                
                string AcctName = ObjModel.LastName + ", " + ObjModel.FirstName + " " + ObjModel.MiddlleName;

                if (regClass.IsCIFKeyExist(ObjModel.LastName, ObjModel.FirstName, ObjModel.MiddlleName, ObjModel.UserEmailId, ObjModel.Birthdate) == false)
                {
                    //Tag As Invalid Registration  Change UI design
                    return RedirectToAction("InvalidRegistration", "MemberAccount");
                }
                else
                {
                    //Insert Data  and Tag user as inActive   OK 
                    regClass.InsertRegisterAcct(ObjModel.UserName, ObjModel.Password, Global.AcctGuidLink);

                    //update status sa ACTIVATION
                    var verifyUrl = "/MemberAccount/VerifyAccount/" + Global.AcctGuidLink;
                    var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

                    //Email id hard coded
                    var fromEmail = new MailAddress("ekoopportal@gmail.com", "Coop Online account");
                    var toEmail = new MailAddress("johnrobertfenix@yahoo.com");
                    var fromEmailPassword = "P@ssw0rd2019"; // Replace with actual password
                    string subject = "Member Portal Account is Successfully Created.";

                    string body = "<br/><br/>We are excited to tell you that your Coop Online account is" +
                        " successfully created. Please click on the below link to verify your account" +
                        " <br/><br/><a href='" + link + "'>" + link + "</a> ";

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
                    };

                    using (var message = new MailMessage(fromEmail, toEmail)
                    {
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    })
                    smtp.Send(message);
                    
                    //Change UI design
                    return RedirectToAction("VerificationLink", "MemberAccount", new { Email = ObjModel.UserEmailId });
                }
            }
            else
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {

            bool Status = false;

            //check uniques Id(activation code)
            ClientAccount db = new ClientAccount();
            DateTime userCreationDate = db.GetCreationDate(Guid.Parse(id));
            TimeSpan difference = userCreationDate.Subtract(DateTime.Now);
            double daterange = difference.TotalHours * (-1);
            if (daterange < 24)
            {
                using (PORTALEntities dc = new PORTALEntities())
                {
                    //dc.Configuration.ValidateOnSaveEnabled = false; // This line I have added here to avoid 
                    
                    //Confirm password does not match issue on save changes

                    var v = dc.CIFOnlineUsers.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                    if (v != null)
                    {
                        v.AccountStatus = "ACTIVE";
                        v.ConfirmedEmail = true;
                        dc.SaveChanges();
                        Status = true;
                    }
                    else
                    {
                        ViewBag.Message = "Invalid Request";
                    }
                }
            }
            else
            {
                //ModelState.AddModelError(string.Empty, "Email Verification has been expired. Please Register Again.");
                return this.RedirectToAction("ExpiredVerification", "MemberAccount");
                //return View();
            }
          
            ViewBag.Status = Status;
            return View();

        }

        public ActionResult VerificationLink()
        {
            return View();
        }

        public ActionResult ExpiredVerification()
        {
            return View();
        }

        public ActionResult InvalidRegistration()
        {
            return View();
        }

        public ActionResult SuccessRegister()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ConfirmedEmail(string token, string Email)
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmedEmail(ActivateAccount objAcct)
        {
            if (ModelState.IsValid)
            {
                //Check if valid username and password 
                if (regClass.CheckUserIfExist(objAcct.Username, objAcct.Password) == true)
                {
                    //check if accescode is correct
                    if (regClass.CheckUserAccessCodeIfExist(objAcct.Username, objAcct.Password, objAcct.Accesscode) == true)
                    {

                        //Goto Login Page
                        return this.RedirectToAction("SuccessRegister", "MemberAccount");
                    }
                    else
                    {
                        ModelState.AddModelError("Warning", "Incorrect Access Code, Please check your Email.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Warning", "Incorrect Username and Password.");
                }
            }
            else
            {
                return View();

            }
            return View();
        }


        // **** REGISTER **** //

        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                // Verification.    
                if (Url.IsLocalUrl(returnUrl))
                {
                    // Info.    
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
            return this.RedirectToAction("Index", "Dashboard");
        }

        // Custom validations : Check Email if exist 
        [HttpPost]
        public JsonResult IsAlreadySigned(string UserEmailId)
        {
            return Json(regClass.IsUserEmailAvailable(UserEmailId));
        }


        // Custom validations : UserName if exist
        [HttpPost]
        public JsonResult IsUserNameExist(string UserName)
        {
            return Json(regClass.IsUserNameAvailable(UserName));
        }

      
        //*********************************************************************************************************//
        // Loan Account
        //*********************************************************************************************************//


        public ActionResult LoanAcctSummary()
        {
            return this.View();
        }



        private void SignInUser(string username, bool isPersistent)
        {
            // Initialization.    
            var claims = new List<Claim>();
            try
            {
                // Setting    
                claims.Add(new Claim(ClaimTypes.Name, username));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                // Sign In.    
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, claimIdenties);
            }
            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            try
            {
                FormsAuthentication.SignOut();

                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                //Delete cookies
                DeleteCookieAssign();
            }

            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
            // Info.    
            return this.RedirectToAction("Index", "Home");
        }

        public void DeleteCookieAssign()
        {
            try
            {
                if (Request.Cookies["username"] != null || Request.Cookies["acctName"] != null)
                {
                    HttpCookie myUserNameCookie = new HttpCookie("username");
                    myUserNameCookie.Expires = DateTime.Now.AddDays(-1d);
                    Response.Cookies.Add(myUserNameCookie);

                    HttpCookie myacctNameCookie = new HttpCookie("acctName");
                    myacctNameCookie.Expires = DateTime.Now.AddDays(-1d);
                    Response.Cookies.Add(myacctNameCookie);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        protected override void Dispose(bool disposing)
        {
            regClass.Dispose();
            base.Dispose(disposing);
        }


    }
}