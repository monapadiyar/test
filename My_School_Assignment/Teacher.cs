//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace My_School_Assignment
{
    using System;
    using System.Collections.Generic;
    
    public partial class Teacher
    {
        public Teacher()
        {
            this.AssignClass2 = new HashSet<AssignClass2>();
        }
    
        public int T_Id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public System.DateTime dateofjoin { get; set; }
        public System.DateTime dateofbirth { get; set; }
        public string email { get; set; }
        public string password1 { get; set; }
    
        public virtual ICollection<AssignClass2> AssignClass2 { get; set; }
    }
}
