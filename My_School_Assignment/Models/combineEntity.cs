using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace My_School_Assignment.Models
{
    public class combineEntity
    {
        public int id { get; set; }
        public int T_Id { get; set; }
        
        public string studentname { get; set; }
        public int classid { get; set; }

        
        public string classname { get; set; }
        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? studob1 { get; set; }
        public string studob { get; set; }
       

    }
    public class StudentCombineEntity
    {
        public int id { get; set; }

        [StringLength(30, MinimumLength = 3, ErrorMessage = "String length must be between 3 to 30 characters")]

        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Please enter upper and lower case alphabets only")]
        [Required(ErrorMessage = "Please enter student name.")]
        public string studentname { get; set; }
        public int classid { get; set; }
        [Required(ErrorMessage = "Please enter class name.")]
        public string classname { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? studob2 { get; set; }
       
        public string studob { get; set; }
    }

   public class classModel
    {
        public int classid { get; set; }
        public string classname { get; set; }
        public bool classselected { get; set; }
       
    }
}
    