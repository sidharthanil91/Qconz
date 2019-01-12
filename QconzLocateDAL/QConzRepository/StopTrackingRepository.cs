using QconzLocateDAL.QConzRepositoryInterface;
using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepository
{
    public class StopTrackingRepository: IStopTrackingRepository
    {
        private QCONZEntities entity = new QCONZEntities();

        public List<StopTrackingModel> StopTracking(int CompanyId)
        {
            List<StopTrackingModel> StopTrackingList = (from t in entity.tblStopTrackings
                                                          where  (t.tblUserMaster.tblOrganization.ID == CompanyId || CompanyId == 0)
                                                          select new StopTrackingModel
                                                          { UserId = t.ID, DateTime = t.LOGTIME, Status = t.STATUS, Hours = t.HOURS, User = t.tblUserMaster.FIRSTNAME + " " + t.tblUserMaster.SURNAME }).ToList();
            return StopTrackingList;
        }

        public string SaveStopTrackingDetails(StopTrackingModel StopTrackingModel)
        {
            try
            {
                if (StopTrackingModel.Id == 0)
                {
                    var stoptracking = new tblStopTracking()
                    {
                        USERID = StopTrackingModel.UserId,
                        HOURS = StopTrackingModel.Hours,
                        STATUS = StopTrackingModel.Status,
                        LOGTIME = StopTrackingModel.DateTime
                    };
                    entity.tblStopTrackings.Add(stoptracking);
                }
                else
                {
                    var y = entity.tblStopTrackings.FirstOrDefault(t => t.ID == StopTrackingModel.Id);
                    y.USERID = StopTrackingModel.UserId;
                    y.HOURS = StopTrackingModel.Hours;
                    y.STATUS = StopTrackingModel.Status;
                    y.LOGTIME = StopTrackingModel.DateTime;
                }
                entity.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }
    }
}
