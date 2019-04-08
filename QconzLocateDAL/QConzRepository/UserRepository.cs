﻿using QconzLocateDAL.QConzRepositoryInterface;
using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepository
{
    public class UserRepository : IUserRepository
    {
        QCONZEntities entity = new QCONZEntities();

        public List<UserModel> GetAllUsers(int CompanyId,string Status)
        {
            try
            {
                List<UserModel> UserList = new List<UserModel>();
                var y = (from t in entity.tblUserMasters where t.USERTYPE > 1 && (t.COMPANYID == CompanyId || CompanyId == 0)&& t.USERSTATUS==Status select t).ToList();
                UserList = y.Select(c => new UserModel
                {
                    Id = c.ID,
                    Cellphone = c.CELLPHONE,
                    CompanyId = c.COMPANYID,
                    Company=c.tblOrganization.TITLE,
                    EmergencyContact = c.EMERGENCYCONTACT,
                    EmergencyContactNo = c.EMERGENCYCONTACTNO,
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
                    WorkingDays = c.WORKINGDAYS,
                    BaseLatitude = c.BASE_LATITUDE,
                    BaseLongitude = c.BASE_LONGITUDE,
                    DefaultGroup = c.DEFAULT_GROUP,
                    IsContractor = c.ISCONTRACTOR
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
                             EmergencyContactNo = c.EMERGENCYCONTACTNO,
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
                             WorkingDays = c.WORKINGDAYS,
                             BaseLatitude = c.BASE_LATITUDE,
                             BaseLongitude = c.BASE_LONGITUDE,
                             DefaultGroup = c.DEFAULT_GROUP,
                             IsContractor = c.ISCONTRACTOR
                         }).FirstOrDefault();

                y.UserGroups = string.Join(",", ((from t in entity.tblUserTeams where t.USERID == y.Id select t.TEAMID).ToArray()));
                                 
                return y;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string SaveUserDetails(UserModel UserModel)
        {
            try
            {
                if(entity.tblUserMasters.Any(t=>t.EMAIL.ToLower()==UserModel.Email.ToLower() && t.ID != UserModel.Id))
                {
                    return ("Email already exists");
                }
                List<int> GroupIds = new List<int>();
                if (UserModel.UserGroups != null && UserModel.UserGroups!="")
                {
                   GroupIds = UserModel.UserGroups.Split(',').Select(int.Parse).ToList();
                }
                if (UserModel.Id == 0)
                {
                    var user = new tblUserMaster()
                    {
                        CELLPHONE = UserModel.Cellphone,
                        COMPANYID = UserModel.CompanyId,
                        EMERGENCYCONTACT = UserModel.EmergencyContact,
                        EMERGENCYCONTACTNO = UserModel.EmergencyContactNo,
                        EMAIL = UserModel.Email,
                        ENDTIME = UserModel.EndTime,
                        STARTTIME=UserModel.StartTime,
                        FIRSTNAME = UserModel.FirstName,
                        PASSWORD = UserModel.Password,
                        SURNAME = UserModel.SurName,
                        USERNAME = " ",
                        USERSTATUS = "A",
                        USERTEAMID = UserModel.UserTeamId,
                        USERTOKEN = UserModel.UserToken,
                        USERTYPE = UserModel.UserType,
                        WORKINGDAYS = UserModel.WorkingDays,
                        BASE_LATITUDE = UserModel.BaseLatitude,
                        BASE_LONGITUDE = UserModel.BaseLongitude,
                        ISCONTRACTOR = UserModel.IsContractor,
                        DEFAULT_GROUP = UserModel.DefaultGroup,
                        ONLINESTATUS=UserModel.OnlineStatus,
                        ONLINESTATUSCHANGETIME=UserModel.OnlineStatusChangeTime
                       };
                    entity.tblUserMasters.Add(user);
                    if(GroupIds != null)
                    {
                        foreach(var item in GroupIds)
                        {
                            var usergroup = new tblUserTeam()
                            {
                                TEAMID = item,
                                USERID=user.ID,
                                ARCHIVE="A"
                            };
                            entity.tblUserTeams.Add(usergroup);
                        }
                    }
                    
                }
                else
                {
                    var y = entity.tblUserMasters.FirstOrDefault(t => t.ID == UserModel.Id);
                    y.CELLPHONE = UserModel.Cellphone;
                    y.COMPANYID = UserModel.CompanyId;
                    y.EMERGENCYCONTACT = UserModel.EmergencyContact;
                    y.EMERGENCYCONTACTNO = UserModel.EmergencyContactNo;
                    y.EMAIL = UserModel.Email;
                    y.ENDTIME = UserModel.EndTime;
                    y.STARTTIME = UserModel.StartTime;
                    y.FIRSTNAME = UserModel.FirstName;
                    y.PASSWORD = UserModel.Password;
                    y.SURNAME = UserModel.SurName;
                    y.USERNAME = " ";
                    y.USERSTATUS = UserModel.UserStatus;
                    //y.USERTEAMID = 1;
                    y.USERTYPE = UserModel.UserType;
                    y.WORKINGDAYS = UserModel.WorkingDays;
                    y.BASE_LATITUDE = UserModel.BaseLatitude;
                    y.BASE_LONGITUDE = UserModel.BaseLongitude;
                    y.ISCONTRACTOR = UserModel.IsContractor;
                    y.DEFAULT_GROUP = UserModel.DefaultGroup;
                    y.ONLINESTATUS = UserModel.OnlineStatus;
                    y.ONLINESTATUSCHANGETIME = UserModel.OnlineStatusChangeTime;
                    entity.SaveChanges();
                    if (GroupIds != null)
                    {
                        var UserTeam = y.tblUserTeams.ToList();
                        var DeleteUserGroups = from p in UserTeam
                                               where !GroupIds.Contains(p.TEAMID)
                                               select p;
                        foreach (var deleteItem in DeleteUserGroups)
                        {
                            entity.tblUserTeams.Remove(deleteItem);
                        }
                        //var InsertUserGroups = from p in GroupIds
                        //                       join t in UserTeam
                        //                       on p equals t.TEAMID into tp
                        //                       from list in tp.Where(x=>x.TEAMID)
                        //                       select p;

                        var InsertUserGroups =new List<int>();
                        foreach (var item in GroupIds)
                        {
                            if((from t in entity.tblUserTeams where t.USERID== UserModel.Id && t.TEAMID==item select t.ID).Count()==0)
                            {
                                InsertUserGroups.Add(item);
                            }
                        }

                        foreach (var item in InsertUserGroups)
                        {
                            var usergroup = new tblUserTeam()
                            {
                                TEAMID = item,
                                USERID = UserModel.Id,
                                ARCHIVE = "A"
                            };
                            entity.tblUserTeams.Add(usergroup);
                        }
                    }
                }
                entity.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }

        public string SaveNotificationToken(string notificationToken,int userId)
        {
            try
            {
                if (entity.tblNotifications.Any(t => t.USERID ==userId))
                {
                    var y = entity.tblNotifications.FirstOrDefault(t => t.USERID == userId);
                    y.TOKEN = notificationToken;
                }
                else
                {
                    var notification = new tblNotification()
                    {
                        USERID = userId,
                        TOKEN = notificationToken
                    };
                    entity.tblNotifications.Add(notification);
                }
               
                entity.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }

        public UserModel GetUserDetailsByName(string username,string password)
        {
            try
            {
                var y = (from c in entity.tblUserMasters
                         where c.EMAIL == username && c.PASSWORD==password
                         select new UserModel
                         {
                             Id = c.ID,
                             Cellphone = c.CELLPHONE,
                             CompanyId = c.COMPANYID,
                             EmergencyContact = c.EMERGENCYCONTACT,
                             EmergencyContactNo = c.EMERGENCYCONTACTNO,
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
                             WorkingDays = c.WORKINGDAYS,
                             BaseLatitude = c.BASE_LATITUDE,
                             BaseLongitude = c.BASE_LONGITUDE,
                             DefaultGroup = c.DEFAULT_GROUP,
                             IsContractor = c.ISCONTRACTOR,
                             OnlineStatus=c.ONLINESTATUS,
                             OnlineStatusChangeTime=c.ONLINESTATUSCHANGETIME
                         }).FirstOrDefault();

                y.UserGroups = string.Join(",", ((from t in entity.tblUserTeams where t.USERID == y.Id select t.TEAMID).ToArray()));

                return y;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<UserModel> GetUserTeamDetails(int userId)
        {
            try
            {
                List<tblUserLog> userLog = entity.tblUserLogs.GroupBy(u => u.USERID).Select(t => t.OrderByDescending(c => c.ID).Where(t1 => t1.LAT.ToLower() != "unknown" && t1.LNG.ToLower() != "unknown").FirstOrDefault()).ToList();
                userLog.RemoveAll(item => item == null);
                var x = (int)(from t in entity.tblUserMasters where t.ID == userId select t.DEFAULT_GROUP).FirstOrDefault();
                var y = (from t in userLog
                         join t1 in entity.tblUserMasters on t.USERID equals t1.ID
                         join t2 in entity.tblUserTeams on t1.ID equals t2.USERID
                         where t2.TEAMID == x && t1.ID != userId && t1.USERSTATUS == "A" && t1.USERTYPE > 1 && t1.USERTYPE != 5
                         select new UserModel
                         {
                             Id = t1.ID,
                             Cellphone = t1.CELLPHONE,
                             CompanyId = t1.COMPANYID,
                             EmergencyContact = t1.EMERGENCYCONTACT,
                             EmergencyContactNo = t1.EMERGENCYCONTACTNO,
                             Email = t1.EMAIL,
                             EndTime = t1.ENDTIME,
                             FirstName = t1.FIRSTNAME,
                             Password = t1.PASSWORD,
                             StartTime = t1.STARTTIME,
                             SurName = t1.SURNAME,
                             UserName = t1.USERNAME,
                             UserStatus = t1.USERSTATUS,
                             UserTeamId = t1.USERTEAMID,
                             UserToken = t1.USERTOKEN,
                             UserType = t1.USERTYPE,
                             WorkingDays = t1.WORKINGDAYS,
                             BaseLatitude = t.LAT,
                             BaseLongitude = t.LNG,
                             DefaultGroup = t1.DEFAULT_GROUP,
                             IsContractor = t1.ISCONTRACTOR
                         }).ToList();


                //var y = (from t in entity.tblUserMasters
                //         join t1 in entity.tblUserTeams on t.ID equals t1.USERID where t1.TEAMID == x && t.ID != userId && t.USERSTATUS == "A" && t.USERTYPE > 1 && t.USERTYPE != 5
                //         join t2 in userLog on t.ID equals t2.USERID
                //         select new UserModel
                //         {
                //             Id = t.ID,
                //             Cellphone = t.CELLPHONE,
                //             CompanyId = t.COMPANYID,
                //             EmergencyContact = t.EMERGENCYCONTACT,
                //             EmergencyContactNo = t.EMERGENCYCONTACTNO,
                //             Email = t.EMAIL,
                //             EndTime = t.ENDTIME,
                //             FirstName = t.FIRSTNAME,
                //             Password = t.PASSWORD,
                //             StartTime = t.STARTTIME,
                //             SurName = t.SURNAME,
                //             UserName = t.USERNAME,
                //             UserStatus = t.USERSTATUS,
                //             UserTeamId = t.USERTEAMID,
                //             UserToken = t.USERTOKEN,
                //             UserType = t.USERTYPE,
                //             WorkingDays = t.WORKINGDAYS,
                //             BaseLatitude = t2.LAT,
                //             BaseLongitude = t2.LNG,
                //             DefaultGroup = t.DEFAULT_GROUP,
                //             IsContractor = t.ISCONTRACTOR
                //         }).ToList();
                return y;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
