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
    public class LoginRepo
    {
        PORTALEntities db = new PORTALEntities();

        //Insert Login Logs 
        public void InsertLoginLog(int clientID)
        {
            db.InsertLoginLogs(clientID);
            db.SaveChanges();
        }

        //Update Logs
        public void UpdateClientLoginDate(int clientID, Guid _guid)
        {
            try
            {
                var checkdata = (from p in db.CIFOnlineUsers where p.ID == clientID && p.GUID == _guid select p).FirstOrDefault();
                if (checkdata != null)
                {
                    checkdata.LastLoginDate = DateTime.Now;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        //server date
        public DateTime getServeDatetime()
        {
            try
            {
                return Convert.ToDateTime(this.db.GetServerDate());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}