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
    
    public partial class User
    {
        public User()
        {
            this.UserRoleMappings = new HashSet<UserRoleMapping>();
        }
    
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    
        public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; }
    }
}
