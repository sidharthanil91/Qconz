﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QconzLocateDAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QCONZEntities : DbContext
    {
        public QCONZEntities()
            : base("name=QCONZEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblCustomer> tblCustomers { get; set; }
        public virtual DbSet<tblOrganization> tblOrganizations { get; set; }
        public virtual DbSet<tblRoaster> tblRoasters { get; set; }
        public virtual DbSet<tblTeam> tblTeams { get; set; }
        public virtual DbSet<tblUserLog> tblUserLogs { get; set; }
        public virtual DbSet<tblUserMaster> tblUserMasters { get; set; }
        public virtual DbSet<tblUserTeam> tblUserTeams { get; set; }
    }
}
