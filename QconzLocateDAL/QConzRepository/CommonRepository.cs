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
            var y = (from t in entity.tblOrganizations where (t.ID == CompanyId || CompanyId == 0)&& t.ARCHIVE=="Y" select new { t.ID, t.TITLE }).ToList();
            CompanySelectList = y.Select(c => new SelectList
            {
                Id = c.ID,
                Text = c.TITLE,
            }
             ).ToList();
            CommonModel.CompanyList = CompanySelectList;
            return CommonModel;
        }

        public CommonModel GetGroupSelectList(int CompanyId)
        {
            CommonModel CommonModel = new CommonModel();
            List<SelectList> GroupSelectList = new List<SelectList>();
            var y = (from t in entity.tblTeams where (t.COMPANYID == CompanyId || CompanyId == 0)&& t.TEAMSTATUS=="A" select new { t.ID, t.TEAMNAME }).ToList();
            GroupSelectList = y.Select(c => new SelectList
            {
                Id = c.ID,
                Text = c.TEAMNAME,
            }
             ).ToList();
            CommonModel.GroupList = GroupSelectList;
            return CommonModel;
        }
        public CommonModel GetUserSelectList(int CompanyId)
        {
            CommonModel CommonModel = new CommonModel();
            List<SelectList> UserSelectList = new List<SelectList>();
            var y = (from t in entity.tblUserMasters where (t.COMPANYID == CompanyId || CompanyId == 0)&&(t.USERSTATUS=="A" && t.USERTYPE>1) select new { t.ID, t.FIRSTNAME,t.SURNAME}).ToList();
            UserSelectList = y.Select(c => new SelectList
            {
                Id = c.ID,
                Text = c.FIRSTNAME+" "+c.SURNAME,
            }
             ).ToList();
            CommonModel.UserList = UserSelectList;
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
                u.PASSWORD == user.Password && u.USERTYPE!=4 && u.USERSTATUS=="A").FirstOrDefault();
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
