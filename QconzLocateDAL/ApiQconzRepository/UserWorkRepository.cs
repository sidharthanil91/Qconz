using QconzLocateDAL.ApiQconzRepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QconzLocateDAL.QConzRepositoryModel;
using System.Data.Entity;

namespace QconzLocateDAL.ApiQconzRepository
{
    public class UserWorkRepository : IUserWorkRepository
    {
        QCONZEntities entity = new QCONZEntities();
        enum Days { monday=1, tuesday, wednesday, thursday, friday, saturday, sunday };
        public OverRideFullModel GetUserWorkRoster(int UserId, DateTime Date)
        {
            OverRideFullModel RosterDetails = new OverRideFullModel();
            List<int> days = new List<int>();
            var userDetails = (from t in entity.tblUserMasters where t.ID == UserId select t).FirstOrDefault();
            days = userDetails.WORKINGDAYS.Split(',').Select(int.Parse).ToList();
            RosterDetails.Days = new List<string>();
            foreach (var item in days)
            {
                 string day= (Enum.GetName(typeof(Days),item));
                 RosterDetails.Days.Add(day);
            }
            RosterDetails.StartTime = userDetails.STARTTIME.Value.ToString("HH:mm:ss");
            RosterDetails.FinishTime = userDetails.ENDTIME.Value.ToString("HH:mm:ss");
            var UserRoaster = (from t in entity.tblUserRoasters where t.USERID == UserId && t.tblRoaster.ARCHIVE == "A" && t.tblRoaster.ENDDATE >= Date select t.tblRoaster).ToList();
            var TeamRoaster = (from ut in entity.tblUserTeams
                               join t1 in entity.tblTeams on new { X1 = ut.TEAMID, X2 = ut.USERID } equals new { X1 = t1.ID, X2 = UserId }
                               join tr in entity.tblTeamRoasters on t1.ID equals tr.TEAMID
                               join r in entity.tblRoasters on tr.ROASTERID equals r.ID
                               where r.ARCHIVE == "A"
                               select r).ToList();

            RosterDetails.OverRides = (UserRoaster.Select(t => new OverRideModel
            {
                StartDate = t.STARTDATE.Value.ToString("yyyy-MM-dd"),
                EndDate = t.ENDDATE.Value.ToString("yyyy-MM-dd"),
                StartTime = t.STARTTIME.Value.ToString("HH:mm:ss"),
                FinishTime = t.FINISHTIME.Value.ToString("HH:mm:ss")
            }).ToList());
            RosterDetails.OverRides.AddRange((TeamRoaster.Select(t => new OverRideModel
            {
                StartDate = t.STARTDATE.Value.ToString("yyyy-MM-dd"),
                EndDate = t.ENDDATE.Value.ToString("yyyy-MM-dd"),
                StartTime = t.STARTTIME.Value.ToString("HH:mm:ss"),
                FinishTime = t.FINISHTIME.Value.ToString("HH:mm:ss")
            }).ToList()));
            return RosterDetails;
        }      
    }
}
