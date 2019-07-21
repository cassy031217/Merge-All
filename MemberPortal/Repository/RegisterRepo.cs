using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.Serialization;
using MemberPortal.Models;
using MemberPortal.Interface;
using System.Net.Mail;
using System.Web.Security;
using System.Web.Mvc;

namespace MemberPortal.Repository
{
    public class RegisterRepo: IRegisterRepo, IDisposable
    {
        private PORTALEntities db;

        //constructor dependency
        public RegisterRepo(PORTALEntities _context)
        {
            this.db = _context;
        }

        public bool IsCIFKeyExist(string lastname, string firstname, string middlename, string emailaddress, DateTime birthdate)
        {
            
            bool status;
            var acctIfExist = (from u in db.CIFOnlineUsers
                               where u.LastName.ToUpper().Trim() == lastname.ToUpper().Trim()
                               && u.FirstName.ToUpper().Trim() == firstname.ToUpper().Trim()
                               && u.MiddleName.ToUpper().Trim() == middlename.ToUpper().Trim()
                               && u.EmailAddress == emailaddress
                               && u.Birthdate.Value.Year == birthdate.Year
                               && u.Birthdate.Value.Month == birthdate.Month
                               && u.Birthdate.Value.Day == birthdate.Day
                               select u).FirstOrDefault();

            if (acctIfExist != null)
            {
                Global.AcctGuidLink = acctIfExist.ActivationCode.ToString();
                status = true;
            }
            else
            {
                status = false;
            }

            return status;
        }


        public void InsertRegisterAcct(string _username, string _password, string _activationcode)
        {
            try
            {
                string password = Crypto.Hash(_password);
                db.UpdateCIFOnlineRegistration(_username, password, Guid.NewGuid(), new Guid(_activationcode));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool IsUserEmailAvailable(string emailId)
        {
            var RegEmailId = (from u in db.CIFOnlineUsers
                              where u.EmailAddress.ToUpper() == emailId.ToUpper()
                              select new { emailId }).FirstOrDefault();
            bool status;
            if (RegEmailId != null)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return status;
        }

        public bool IsUserNameAvailable(string username)
        {
            var RegUserId = (from u in db.CIFOnlineUsers
                             where u.Username.ToUpper() == username.ToUpper()
                             select new { username }).FirstOrDefault();

            bool status;
            if (RegUserId != null)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return status;
        }

        public bool CheckUserAccessCodeIfExist(string userName, string passWord, string passcode)
        {
            //var checkUserAccessCode = (from c in db.CIFOnlineUsers where c.Username == userName && c.Password == passWord && c.AcessCode == passcode select c).SingleOrDefault();
            var checkUserAccessCode = (from c in db.CIFOnlineUsers where c.Username == userName && c.Password == passWord  select c).SingleOrDefault();
            bool status;
            if (checkUserAccessCode != null)
            {
                //update
                Guid guidID = Guid.Parse(checkUserAccessCode.GUID.ToString());
                bool isonline = true;
                var updateDate = db.CIFOnlineUsers.First(x => x.GUID == guidID);
                updateDate.AccountStatus = "ACTIVE";
                updateDate.LastLoginDate = DateTime.Now;
                updateDate.CreationDate = DateTime.Now;
                updateDate.IsOnLine = isonline;
                updateDate.ConfirmedEmail = isonline;
                db.SaveChanges();


                status = true;
            }
            else
            {
                status = false;
            }

            return status;
        }


        public bool CheckIfValidUser(string userName, string passWord, string acctstatus)
        {
            var userlogin = (from c in db.CIFOnlineUsers where c.Username == userName && c.Password == passWord && c.AccountStatus == acctstatus select c).ToList();
            bool status;
            if (userlogin.Count() > 0)
            {
                foreach (var item in userlogin)
                {
                    Global.UserGUID = item.GUID.ToString();
                    Global.ID = item.ID;
                    Global.FirstName = item.FirstName.ToString();
                    Global.LastName = item.LastName.ToString();
                    Global.MiddleName = item.MiddleName.ToString();
                    Global.Gender = item.Sex.ToString();
                    Global.AccountName = item.AccountName.ToString();
                    Global.Cifkey = item.CIFkey.ToString();
                    Global.LastLoginDate = Convert.ToDateTime(item.LastLoginDate);
                    Global.Birthdate = Convert.ToDateTime(item.Birthdate);
                    Global.EmailAddress = item.EmailAddress.ToString();
                    Global.UserName = item.Username.ToString();
                    Global.Branchcode = item.BranchCode.ToString();
                }
                status = true;
            }
            else
            {
                status = false;
            }

            return status;
        }

        public bool CheckUserIfExist(string userName, string passWord)
        {
            bool status;


            var checkUser = (from c in db.CIFOnlineUsers where c.Username == userName select c).FirstOrDefault();
            if (checkUser != null)
            {   
                //check email id 
                if (checkUser.ConfirmedEmail == false)
                {
                    status = false;
                }

                //Global.AcctCreation = Convert.ToDateTime(checkUser.CreationDate);
                if (string.Compare(Crypto.Hash(passWord), checkUser.Password) == 0)
                {
                    string _password = Crypto.Hash(passWord);
                    //reheck if username and password our valid to proceed and ACTIVE
                    var userlogin = (from c in db.CIFOnlineUsers where c.Username == userName && c.Password == _password && c.AccountStatus == "ACTIVE" select c).ToList();

                    foreach (var item in userlogin)
                    {
                        Global.UserGUID = item.GUID.ToString();
                        Global.ID = item.ID;
                        Global.FirstName = item.FirstName.ToString();
                        Global.LastName = item.LastName.ToString();
                        Global.MiddleName = item.MiddleName.ToString();
                        Global.Gender = item.Sex.ToString();
                        Global.AccountName = item.AccountName.ToString();
                        Global.Cifkey = item.CIFkey.ToString();
                        Global.LastLoginDate = Convert.ToDateTime(item.LastLoginDate);
                        Global.Birthdate = Convert.ToDateTime(item.Birthdate);
                        Global.EmailAddress = item.EmailAddress.ToString();
                        Global.UserName = item.Username.ToString();
                        Global.Branchcode = item.BranchCode.ToString();
                    }
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            else
            {
                status = false;
            }
            return status;

        }

 





        // ****  Disposed Pattern *** //

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                };
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }



    }
}