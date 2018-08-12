using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QconzLocate.Models
{
    public class CustomerViewModel
    {
        public List<CustomerListViewModel> CustomerList { get; set; }
        public CustomerListViewModel CustomerDetails { get; set; }
        public List<SelectListItems> CompanyList { get; set; }
    }
    public class CustomerListViewModel
    {
        public int Id { get; set; }
        public string CustomerCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OfficeName { get; set; }
        public string City { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public DateTime? AddedDate { get; set; }
        public int? CompanyId { get; set; }
        public string Archive { get; set; }
    }
}