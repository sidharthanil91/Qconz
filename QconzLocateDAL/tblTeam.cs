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
    
    public partial class tblTeam
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblTeam()
        {
            this.tblUserMasters = new HashSet<tblUserMaster>();
        }
    
        public int ID { get; set; }
        public int TEAMNAME { get; set; }
        public string TEAMDESC { get; set; }
        public Nullable<System.DateTime> TEAMCREATED { get; set; }
        public string TEAMSTATUS { get; set; }
        public int COMPANYID { get; set; }
    
        public virtual tblOrganization tblOrganization { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblUserMaster> tblUserMasters { get; set; }
    }
}
