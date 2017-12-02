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

        public void SaveUserDetails(UserModel UserModel)
        {
            if (UserModel.Id == 0)
            {
                var user = new tblUserMaster()
                {
                    CELLPHONE = UserModel.Cellphone,
                    COMPANYID = UserModel.CompanyId,
                    EMERGENCYCONTACT = UserModel.EmergencyContact,
                    EMAIL = UserModel.Email,
                    ENDTIME = UserModel.EndTime,
                    FIRSTNAME = UserModel.FirstName,
                    PASSWORD = UserModel.Password,
                    SURNAME = UserModel.SurName,
                    USERNAME = UserModel.UserName,
                    USERSTATUS = UserModel.UserStatus,
                    USERTEAMID = UserModel.UserTeamId,
                    USERTOKEN = UserModel.UserToken,
                    USERTYPE = UserModel.UserType,
                    WORKINGDAYS = UserModel.WorkingDays
                };
                entity.tblUserMasters.Add(user);
            }
            else
            {
                var y = entity.tblUserMasters.FirstOrDefault(t => t.ID == UserModel.Id);
                    y.CELLPHONE = UserModel.Cellphone;
                    y.COMPANYID = UserModel.CompanyId;
                    y.EMERGENCYCONTACT = UserModel.EmergencyContact;
                    y.EMAIL = UserModel.Email;
                    y.ENDTIME = UserModel.EndTime;
                    y.FIRSTNAME = UserModel.FirstName;
                    y.PASSWORD = UserModel.Password;
                    y.SURNAME = UserModel.SurName;
                    y.USERNAME = UserModel.UserName;
                    y.USERSTATUS = UserModel.UserStatus;
                    y.USERTEAMID = UserModel.UserTeamId;
                    y.USERTOKEN = UserModel.UserToken;
                    y.USERTYPE = UserModel.UserType;
                    y.WORKINGDAYS = UserModel.WorkingDays;
            }
            entity.SaveChanges();
        }
    }
}
