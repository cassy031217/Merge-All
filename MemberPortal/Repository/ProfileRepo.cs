using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MemberPortal.Models;

namespace MemberPortal.Repository
{
    public class ProfileRepo
    {
        PORTALEntities db = new PORTALEntities();

        //Update password 
        public void UpdatePassword(string _acctname, Guid _guid, string _password)
        {
            var checkdata = (from p in db.CIFOnlineUsers where p.AccountName == _acctname && p.GUID == _guid select p).FirstOrDefault();
            if (checkdata != null)
            {
                checkdata.Password = _password;
                db.SaveChanges();
            }
        }

        public bool CheckInputPassword(int _clinetID, string _password)
        {
            string oldpassword = Crypto.Hash(_password);
            var checkpassword = (from u in db.CIFOnlineUsers where u.ID == _clinetID && u.Password == oldpassword select u).FirstOrDefault();
            bool status;
            if (checkpassword != null)
            {
                //Incorrect Password 
                status = false;
            }
            else
            {
                //Correct Password  
                status = true;
            }

            return status;
        }
    }
}