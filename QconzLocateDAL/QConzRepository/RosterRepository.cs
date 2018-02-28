using QconzLocateDAL.QConzRepositoryInterface;
using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepository
{
    public class RosterRepository : IRosterRepository
    {
        QCONZEntities entity = new QCONZEntities();

        public List<RosterModel> GetAllRoster(int CompanyId, string Status)
        {
            try
            {
                var currentdate = DateTime.Now;
                var updatelist = (from t in entity.tblRoasters
                                  let time = DbFunctions.CreateTime(t.FINISHTIME.Value.Hour,
                                                     t.FINISHTIME.Value.Minute,
                                                     t.FINISHTIME.Value.Second)
                                  where (t.ENDDATE < currentdate.Date||(t.ENDDATE==currentdate.Date && time<currentdate.TimeOfDay))  
                                  && t.ARCHIVE=="A" select t).ToList();
                foreach(var item in updatelist)
                {
                    item.ARCHIVE = "N";
                }
                entity.SaveChanges();
                List < RosterModel > RosterList = new List<RosterModel>();
                var y = (from t in entity.tblRoasters where (t.COMPANYID == CompanyId || CompanyId == 0) && t.ARCHIVE == Status select t).ToList();
                RosterList = y.Select(c => new RosterModel
                {
                    Id = c.ID,
                    EndDate = c.ENDDATE,
                    FinishTime = c.FINISHTIME,
                    StartDate = c.STARTDATE,
                    StartTime = c.STARTTIME,
                    Override = c.OVERRIDE,
                    OverrideDetails = c.OVERIDEDETAIL,
                    Status = c.ARCHIVE,
                    UserId = c.USERID
                }
                ).ToList();
                return RosterList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public RosterModel GetRosterDetails(int Id)
        {
            try
            {
                var y = (from c in entity.tblRoasters
                         where c.ID == Id
                         select new RosterModel
                         {
                             Id = c.ID,
                             Override = c.OVERRIDE,
                             OverrideDetails = c.OVERIDEDETAIL,
                             EndDate = c.ENDDATE,
                             FinishTime = c.FINISHTIME,
                             StartDate = c.STARTDATE,
                             StartTime = c.STARTTIME,
                             UserId = c.USERID,
                             Status = c.ARCHIVE
                         }).FirstOrDefault();
                var teams = entity.tblTeamRoasters.Where(t => t.ROASTERID == Id).Select(t1 => t1.TEAMID).ToList();
                if (teams != null)
                    y.TeamId = string.Join(",", teams.Select(t => t.ToString()));
                return y;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void SaveRosterDetails(RosterModel RosterModel)
        {
            try
            {
                List<int> UserIds = new List<int>();
                List<int> TeamIds = new List<int>();
                if (RosterModel.UserId!=null)
                 UserIds = RosterModel.UserId.Split(',').Select(int.Parse).ToList();
                if (RosterModel.TeamId != null)
                    TeamIds = RosterModel.TeamId.Split(',').Select(int.Parse).ToList();
                if (RosterModel.Id == 0)
                {
                    var roster = new tblRoaster()
                    {
                        ENDDATE = RosterModel.EndDate,
                        COMPANYID = RosterModel.CompanyId,
                        OVERIDEDETAIL = RosterModel.OverrideDetails,
                        OVERRIDE = RosterModel.Override,
                        STARTTIME = RosterModel.StartTime,
                        STARTDATE = RosterModel.StartDate,
                        FINISHTIME = RosterModel.FinishTime,
                        USERID = RosterModel.UserId,
                        ARCHIVE = RosterModel.Status
                    };
                    entity.tblRoasters.Add(roster);
                    foreach (var item in UserIds)
                    {
                        var userroster = new tblUserRoaster()
                        {
                            ARCHIVE = "A",
                            ROASTERID = roster.ID,
                            USERID = item
                        };
                        entity.tblUserRoasters.Add(userroster);
                    }
                    foreach (var item in TeamIds)
                    {
                        var teamroster = new tblTeamRoaster()
                        {
                            ARCHIVE = "A",
                            ROASTERID = roster.ID,
                            TEAMID = item
                        };
                        entity.tblTeamRoasters.Add(teamroster);
                    }
                }
                else
                {
                    var y = entity.tblRoasters.FirstOrDefault(t => t.ID == RosterModel.Id);
                    y.ENDDATE = RosterModel.EndDate;
                    y.STARTTIME = RosterModel.StartTime;
                    y.STARTDATE = RosterModel.StartDate;
                    y.FINISHTIME = RosterModel.FinishTime;
                    y.OVERIDEDETAIL = RosterModel.OverrideDetails;
                    y.OVERRIDE = RosterModel.Override;
                    y.USERID = RosterModel.UserId;
                    y.ARCHIVE = RosterModel.Status;
                    var ExistingUserRoaster = entity.tblUserRoasters.Where(t => t.ROASTERID == y.ID).Select(t1 => t1.USERID).ToList();
                    var NewUserRoaster = UserIds.Except(ExistingUserRoaster);
                    foreach (var item in NewUserRoaster)
                    {
                        var userroster = new tblUserRoaster()
                        {
                            ARCHIVE = "A",
                            ROASTERID = y.ID,
                            USERID = item
                        };
                        entity.tblUserRoasters.Add(userroster);
                    }
                    var DeleteUserRoaster = ExistingUserRoaster.Except(UserIds);
                    foreach (var item in DeleteUserRoaster)
                    {
                        var deleteItem = entity.tblUserRoasters.FirstOrDefault(t => t.USERID == item && t.ROASTERID == y.ID);
                        entity.tblUserRoasters.Remove(deleteItem);
                    }
                    var ExistingTeamRoaster = entity.tblTeamRoasters.Where(t => t.ROASTERID == y.ID).Select(t1 => t1.TEAMID).ToList();
                    var NewTeamRoaster = TeamIds.Except(ExistingTeamRoaster);
                    foreach (var item in NewTeamRoaster)
                    {
                        var teamroster = new tblTeamRoaster()
                        {
                            ARCHIVE = "A",
                            ROASTERID = y.ID,
                            TEAMID = item
                        };
                        entity.tblTeamRoasters.Add(teamroster);
                    }
                    var DeleteTeamRoaster = ExistingTeamRoaster.Except(TeamIds);
                    foreach (var item in DeleteTeamRoaster)
                    {
                        var deleteItemT = entity.tblTeamRoasters.FirstOrDefault(t => t.TEAMID == item && t.ROASTERID == y.ID);
                        entity.tblTeamRoasters.Remove(deleteItemT);
                    }
                }
                entity.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

    }
}
