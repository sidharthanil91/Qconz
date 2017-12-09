using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.Models
{
    public class CommonServiceModel
    {
        public List<SelectLists> GroupList { get; set; }
        public List<SelectLists> CompanyList { get; set; }
        public List<SelectLists> UserList { get; set; }
    }
    public class SelectLists
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

}
