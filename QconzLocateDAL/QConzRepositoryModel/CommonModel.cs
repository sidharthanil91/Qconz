using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepositoryModel
{
    public class CommonModel
    {
        public List<SelectList> GroupList { get; set; }
        public List<SelectList> CompanyList { get; set; }
        public List<SelectList> UserList { get; set; }
    }
    public class SelectList
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
