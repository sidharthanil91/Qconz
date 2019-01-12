using QconzLocateDAL.ApiQconzRepositoryInterface;
using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.ApiQconzRepository
{
    public class UserLocationRepository : IUserLocationRepository
    {
        QCONZEntities entity = new QCONZEntities();

        public void SaveUserEmergencyLocation(List<UserLocationModel> UserLocation)
        {
            foreach (var item in UserLocation)
            {
                tblEmergency UserLog = new tblEmergency()
                {
                    LAT = item.Latitude,
                    LNG = item.Longitude,
                    LOGTIME = item.DateTime,
                    USERID = item.UserId
                };
                entity.tblEmergencies.Add(UserLog);
            }
            entity.SaveChanges();
        }

        public void SaveUserLocation(List<UserLocationModel> UserLocation)
        {
            foreach (var item in UserLocation)
            {
                tblUserLog UserLog = new tblUserLog()
                {
                    LAT = item.Latitude,
                    LNG = item.Longitude,
                    LOGTIME = item.DateTime,
                    USERID = item.UserId
                };
                entity.tblUserLogs.Add(UserLog);
            }
            entity.SaveChanges();
        }

        public string SaveUserLocationDetails(UserLocationModel UserLocationDetails)
        {
            try
            {
                //if (UserLocationDetails.Id == 0)
                //{
                    var userlocationdetails = new tblUserLog()
                    {
                        LAT = UserLocationDetails.Latitude,
                        LNG = UserLocationDetails.Longitude,
                        LOGTIME = UserLocationDetails.DateTime,
                        USERID = UserLocationDetails.UserId
                    };
                    entity.tblUserLogs.Add(userlocationdetails);
                //}
                //else
                //{
                //    var y = entity.tblStopTrackings.FirstOrDefault(t => t.ID == StopTrackingModel.Id);
                //    y.USERID = StopTrackingModel.UserId;
                //    y.HOURS = StopTrackingModel.Hours;
                //    y.STATUS = StopTrackingModel.Status;
                //    y.LOGTIME = StopTrackingModel.DateTime;
                //}
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
