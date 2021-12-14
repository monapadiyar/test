using System;
using System.ComponentModel.DataAnnotations;

namespace My_School_Assignment.Models
{
    public class Membership
    {
        public int Id { get; set; }
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
    public class SignupModel
    {
        public int T_Id{ get; set; }

        [RegularExpression(@"^([A-Za-z]+)$", ErrorMessage = "Please enter upper and lower case alphabets only")]
        [Required(ErrorMessage = "Please enter First name.")]
        public string firstname { get; set; }

        [RegularExpression(@"^([A-Za-z]+)$", ErrorMessage = "Please enter upper and lower case alphabets only")]
        [Required(ErrorMessage = "Please enter Last name.")]
        public string lastname { get; set; }

        [Required(ErrorMessage = "Please select the date.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? dateofjoin { get; set; }

        [Required(ErrorMessage = "Please select the date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? dateofbirth { get; set; }
       

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(15, ErrorMessage = "Must be between 8 and 15 characters", MinimumLength = 8)]
        [RegularExpression(@"^[A-Za-z0-9\.@#\$%&]+$", ErrorMessage =" at least 1 Uppercase Alphabet, Lowercase Alphabet, Number and Special Character")]
        public string password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("password")]
        public string confirmpassword { get; set; }
        public int C_Id { get; set; }
        public string classname { get; set; }

       
        public string t_dob1 { get; set; }
        public string t_doj1 { get; set; }
       

    }
   
   

}