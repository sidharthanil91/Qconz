using QconzLocateDAL.ApiQconzRepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QconzLocateDAL.QConzRepositoryModel;

namespace QconzLocateDAL.ApiQconzRepository
{
    public class UserWorkRepository : IUserWorkRepository
    {
        QCONZEntities entity = new QCONZEntities();
        public RosterModel GetUserWorkRoster(int UserId, DateTime Date)
        {
            RosterModel RosterDetails;
            var Roster = GetOverRideDetails(UserId, Date);
            if (Roster.Status == "1")
            {
                return Roster;
            }
            else
            {
                var day = (Int32)Date.DayOfWeek;
                var userDetails = (from t in entity.tblUserMasters where t.ID == UserId select t).FirstOrDefault();
                List<int> days = new List<int>();
                if (userDetails.WORKINGDAYS != null)
                {
                    days = userDetails.WORKINGDAYS.Split(',').Select(int.Parse).ToList();

                    if (days.Any(t => t == day))
                    {
                        RosterDetails = new RosterModel()
                        {
                            StartTime = userDetails.STARTTIME,
                            FinishTime = userDetails.ENDTIME,
                            Status = "1"
                        };
                        return RosterDetails;
                    }
                }

                RosterDetails = new RosterModel()
                {
                    Status = "0"
                };

                return RosterDetails;
            }
        }
        public RosterModel GetOverRideDetails(int UserId, DateTime Date)
        {
            //var RosterG = (from t in entity.tblTeamRoasters where t.tblTeam.tblUserTeams.USE == UserId select t.tblRoaster).FirstOrDefault();
            //RosterModel RosterGDetails;
            //if (RosterG == null)
            //{
            //    RosterGDetails = new RosterModel()
            //    {
            //        Status = "0"
            //    };
            //    return RosterGDetails;
            //}
            //if (RosterG.STARTDATE <= Date && RosterG.ENDDATE >= Date)
            //{
            //    RosterGDetails = new RosterModel()
            //    {
            //        StartTime = RosterG.STARTTIME,
            //        FinishTime = RosterG.FINISHTIME,
            //        Status = "1"
            //    };
            //}
            //else
            //{
            //    RosterGDetails = new RosterModel()
            //    {
            //        Status = "0"
            //    };
            //}
            var Roster = (from t in entity.tblUserRoasters where t.USERID == UserId select t.tblRoaster).FirstOrDefault();
            RosterModel RosterDetails;
            if (Roster == null)
            {
                RosterDetails = new RosterModel()
                {
                    Status = "0"
                };
                return RosterDetails;
            }
            if (Roster.STARTDATE <= Date && Roster.ENDDATE >= Date)
            {
                RosterDetails = new RosterModel()
                {
                    StartTime = Roster.STARTTIME,
                    FinishTime = Roster.FINISHTIME,
                    Status = "1"
                };
            }
            else
            {
                RosterDetails = new RosterModel()
                {
                    Status = "0"
                };
            }
            return RosterDetails;
        }
    }
}
