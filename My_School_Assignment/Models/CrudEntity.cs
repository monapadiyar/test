using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace My_School_Assignment.Models
{
    public class CrudEntity
    {
        public int classid { get; set; }
        public string classname { get; set; }

    }
    public class GetClass
    {
        public int C_id { get; set; }
        [StringLength(30, MinimumLength = 3,ErrorMessage ="String length must be between 3 to 30 characters")]
        public string c_name { get; set; }
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Please enter upper and lower case alphabets only")]
        [Required(ErrorMessage = "Please enter student name.")]
        public string studentname { get; set; }


        [Required(ErrorMessage = "Please select date of birth.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ? dateofbirth { get; set; }

       

    }
    public class NewClass1
    {
        public int classid { get; set; }

        public string classname { get; set; }
    }

}