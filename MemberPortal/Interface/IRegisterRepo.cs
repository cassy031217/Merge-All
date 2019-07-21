using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberPortal.Repository;
using MemberPortal.Models;

namespace MemberPortal.Interface
{
    public interface IRegisterRepo
    {
        bool IsCIFKeyExist(string lastname, string firstname, string middlename, string emailaddress, DateTime birthdate);
        void InsertRegisterAcct(string _username, string _password, string _activationcode);
        bool IsUserEmailAvailable(string EmailId);
        bool IsUserNameAvailable(string username);
        bool CheckUserAccessCodeIfExist(string userName, string passWord, string passcode);
        bool CheckIfValidUser(string userName, string passWord, string acctstatus);
        bool CheckUserIfExist(string userName, string passWord);
        void Dispose();
    }
}
