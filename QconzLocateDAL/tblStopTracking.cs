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
    
    public partial class tblStopTracking
    {
        public int ID { get; set; }
        public int USERID { get; set; }
        public Nullable<int> HOURS { get; set; }
        public string STATUS { get; set; }
        public System.DateTime LOGTIME { get; set; }
    
        public virtual tblUserMaster tblUserMaster { get; set; }
    }
}