using QconzLocateDAL.QConzRepositoryInterface;
using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepository
{
    public class UserRepository:IUserRepository
    {
        QCONZEntities entity = new QCONZEntities();

        public List<UserModel> GetAllUsers()
        {
            try
            {
                List<UserModel> UserList = new List<UserModel>();
                var y = (from t in entity.tblUserMasters select t).ToList();
                UserList = y.Select(c => new UserModel
                {
                    Id = c.ID,
                    Cellphone = c.CELLPHONE,
                    CompanyId = c.COMPANYID,
                    EmergencyContact = c.EMERGENCYCONTACT,
                    Email = c.EMAIL,
                    EndTime = c.ENDTIME,
                    FirstName = c.FIRSTNAME,
                    Password = c.PASSWORD,
                    StartTime = c.STARTTIME,
                    SurName = c.SURNAME,
                    UserName = c.USERNAME,
                    UserStatus = c.USERSTATUS,
                    UserTeamId=c.USERTEAMID,
                    UserToken=c.USERTOKEN,
                    UserType=c.USERTYPE,
                    WorkingDays=c.WORKINGDAYS
                }
                ).ToList();
                return UserList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public UserModel GetUserDetails(int Id)
        {
            try
            {
                var y = (from c in entity.tblUserMasters
                         where c.ID == Id
                         select new UserModel
                         {
                             Id = c.ID,
                             Cellphone = c.CELLPHONE,
                             CompanyId = c.COMPANYID,
                             EmergencyContact = c.EMERGENCYCONTACT,
                             Email = c.EMAIL,
                             EndTime = c.ENDTIME,
                             FirstName = c.FIRSTNAME,
                             Password = c.PASSWORD,
                             StartTime = c.STARTTIME,
                             SurName = c.SURNAME,
                             UserName = c.USERNAME,
                             UserStatus = c.USERSTATUS,
                             UserTeamId = c.USERTEAMID,
                             UserToken = c.USERTOKEN,
                             UserType = c.USERTYPE,
                             WorkingDays = c.WORKINGDAYS
                         }).FirstOrDefault();
                return y;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
