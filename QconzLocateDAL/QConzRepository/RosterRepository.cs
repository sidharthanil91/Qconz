using QconzLocateDAL.QConzRepositoryInterface;
using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepository
{
    public class RosterRepository: IRosterRepository
    {
        QCONZEntities entity = new QCONZEntities();

        public List<RosterModel> GetAllRoster(int CompanyId)
        {
            try
            {
                List<RosterModel> RosterList = new List<RosterModel>();
                var y = (from t in entity.tblRoasters where t.COMPANYID==CompanyId || CompanyId==0 select t).ToList();
                RosterList = y.Select(c => new RosterModel
                {
                    Id = c.ID,
                    EndDate=c.ENDDATE,
                    FinishTime=c.FINISHTIME,
                    StartDate=c.STARTDATE,
                    StartTime=c.STARTTIME,
                    UserId=c.USERID
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
                             EndDate = c.ENDDATE,
                             FinishTime = c.FINISHTIME,
                             StartDate = c.STARTDATE,
                             StartTime = c.STARTTIME,
                             UserId = c.USERID
                         }).FirstOrDefault();
                return y;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void SaveRosterDetails(RosterModel RosterModel)
        {
            if (RosterModel.Id == 0)
            {
                var roster = new tblRoaster()
                {
                    ENDDATE=RosterModel.EndDate,
                    COMPANYID=RosterModel.CompanyId,
                    STARTTIME=RosterModel.StartTime,
                    STARTDATE=RosterModel.StartDate,
                    FINISHTIME=RosterModel.FinishTime,
                    USERID=RosterModel.UserId
                };
                entity.tblRoasters.Add(roster);
            }
            else
            {
                var y = entity.tblRoasters.FirstOrDefault(t => t.ID == RosterModel.Id);
                y.ENDDATE = RosterModel.EndDate;
                y.STARTTIME = RosterModel.StartTime;
                y.STARTDATE = RosterModel.StartDate;
                y.FINISHTIME = RosterModel.FinishTime;
                y.USERID = RosterModel.UserId;
            }
            entity.SaveChanges();
        }
    }
}
