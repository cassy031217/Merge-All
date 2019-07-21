using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MemberPortal.Models
{

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is Required.")]
        public string Username
        {
            get;
            set;
        }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Required.")]
        public string Password
        {
            get;
            set;
        }
    }

    public class LoanCalcalculator
    {
        public decimal amount { get; set; }
        public int terms { get; set; }
    }

    public class SelectDateRange
    {
        public string DateRangeValue{ get; set; }
    }

    public class ActivateAccount
    {
        [Required(ErrorMessage = "UserName is Required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is Required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Access Code is Required.")]
        public string Accesscode { get; set; }
    }
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "First Name is Required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Middlle Name is Required.")]
        public string MiddlleName { get; set; }

        [Required(ErrorMessage = "Gender is Required.")]
        public Gender Sex { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Birth Date is Required.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthdate { get; set; }


        [Required(ErrorMessage = "Email Address is Required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        //[Remote("IsAlreadySigned", "MemberAccount", HttpMethod = "POST", ErrorMessage = "Email address already registered.")]
        public string UserEmailId { get; set; }

        [Required(ErrorMessage = "User Name is Required.")]
        [Remote("IsUserNameExist", "MemberAccount", HttpMethod = "POST", ErrorMessage = "Username already registered.")]
        public string UserName { get; set; }


        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Required.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }





}
