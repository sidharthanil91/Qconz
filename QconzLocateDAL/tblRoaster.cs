//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class tblRoaster
    {
        public int ID { get; set; }
        public int USERID { get; set; }
        public Nullable<System.DateTime> STARTDATE { get; set; }
        public Nullable<System.DateTime> ENDDATE { get; set; }
        public System.TimeSpan STARTTIME { get; set; }
        public System.TimeSpan FINISHTIME { get; set; }
    
        public virtual tblUserMaster tblUserMaster { get; set; }
    }
}