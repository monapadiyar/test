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
    
    public partial class classes1
    {
        public classes1()
        {
            this.AssignClass2 = new HashSet<AssignClass2>();
            this.students = new HashSet<student>();
        }
    
        public int classid { get; set; }
        public string classname { get; set; }
    
        public virtual ICollection<AssignClass2> AssignClass2 { get; set; }
        public virtual ICollection<student> students { get; set; }
    }
}
