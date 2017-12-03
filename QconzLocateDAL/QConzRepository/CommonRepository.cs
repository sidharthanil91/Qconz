using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepository
{
    public class CommonRepository
    {
        QCONZEntities entity = new QCONZEntities();

        public CommonModel GetCompanySelectList(int CompanyId)
        {
            CommonModel CommonModel = new CommonModel();
            List<SelectList> CompanySelectList = new List<SelectList>();
            var y = (from t in entity.tblOrganizations where t.ID == CompanyId || CompanyId == 0 select new { t.ID, t.TITLE }).ToList();
            CompanySelectList = y.Select(c => new SelectList
            {
                Id = c.ID,
                Text = c.TITLE,
            }
             ).ToList();
            CommonModel.CompanyList = CompanySelectList;
            return CommonModel;
        }
        public UserAuthRepositoryModel GetLoginUserDetails(UserAuthRepositoryModel user)
        {
            UserAuthRepositoryModel UserDetails = new UserAuthRepositoryModel();
            Dictionary<int, string> UserRole
                       = new Dictionary<int, string>() {
                         {1,"SUPER"},
                         {2,"ADMIN"},
                         {3,"DASHBOARD"},
                         {4,"APP" } };
            var y= entity.tblUserMasters.Where(u => u.EMAIL.ToLower() == user.Email.ToLower() &&
                u.PASSWORD == user.Password).FirstOrDefault();
            if (y==null)
            {
                return null;
            }
            UserDetails.Roles = UserRole[y.USERTYPE];
            UserDetails.Email = y.EMAIL;
            UserDetails.CompanyId = y.COMPANYID;
            return UserDetails;

        }
    }
}
