﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MySchoolEntities : DbContext
    {
        public MySchoolEntities()
            : base("name=MySchoolEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Admin> Admins { get; set; }
        public DbSet<AssignClass2> AssignClass2 { get; set; }
        public DbSet<classes1> classes1 { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<student> students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<UserRoleMapping> UserRoleMappings { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
