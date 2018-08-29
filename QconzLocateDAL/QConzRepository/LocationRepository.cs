using QconzLocateDAL.QConzRepository;
using QconzLocateDAL.QConzRepositoryInterface;
using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepository
{
    public class LocationRepository : ILocationRepository
    {
        private QCONZEntities entity = new QCONZEntities();
        public List<LocationModel> GetCustomerLocation(int CompanyId, int Customer, int GroupId, int[] ShowMap, int[] ShowPin, int[] ShowBase)
        {
            
            var y = (from t in entity.tblCustomers where ((t.COMPANYID == CompanyId || CompanyId == 0)) && t.ID==Customer select t);
            List<LocationModel> location;
            location = y.Select(c => new LocationModel
            {
                UserId = c.ID,
                Name = c.OFFICENAME,
                Lat = c.LAT,
                Lng = c.LNG,
                Contact = c.FIRSTNAME + (c.PHONE_1==null?"":(" - Ph:"+c.PHONE_1))+(c.EMAIL == null ? "" : (" - Email:" + c.EMAIL)),
                Address=c.ADDRESS1+" "+c.ADDRESS2+" "+c.CITY,
                Type = "C"
            }
             ).ToList();
            var users = GetUserLocation(CompanyId, 0, GroupId, ShowMap, ShowPin,ShowBase);
            foreach (var item in users)
            {
                location.Add(item);
            }
            return location;

        }

        public List<LocationModel> GetUserLocation(int CompanyId, int UserId, int GroupId, int[] ShowMap, int[] ShowPin, int[] ShowBase)
        {

            List<tblUserLog> users = entity.tblUserLogs.GroupBy(x => x.USERID).Select(t => t.OrderByDescending(c => c.ID).Where(t1 => t1.LAT.ToLower() != "unknown" && t1.LNG.ToLower() != "unknown").FirstOrDefault()).ToList();
            users.RemoveAll(item => item == null);
            List<LocationModel> SelectedUsers = (from t in users
                                                 join t1 in entity.tblUserMasters on t.USERID equals t1.ID
                                                 where (CompanyId == 0 || t1.COMPANYID == CompanyId) && (GroupId == 0 || t1.tblUserTeams.Select(t2 => t2.TEAMID).Contains(GroupId)) && (UserId == 0 || t.USERID == UserId)&&(ShowMap!=null &&  ShowMap.Contains(t.USERID))
                                                 select new LocationModel
                                                 { UserId = t1.ID, Name = t1.FIRSTNAME + " " + t1.SURNAME, Address = t.LOGTIME.ToString("dd/MM/yyyy   hh:mm:ss tt"), Lat = t.LAT, Lng = t.LNG, Type = "U" }).ToList();
            if (ShowPin!=null)
            {
                foreach (var item in SelectedUsers)
                {
                    if (ShowPin.Contains(item.UserId))
                    {
                        item.ShowPin = true;
                    }
                }
            }
            if(ShowBase!=null)
            {
                List<LocationModel> BaseLocations = (from t in entity.tblUserMasters
                                                     where ShowBase.Contains(t.ID) && t.BASE_LATITUDE != null && t.BASE_LONGITUDE != null
                                                     select new LocationModel
                                                     { UserId = t.ID, Name = t.FIRSTNAME + " " + t.SURNAME, Address = "", Lat = t.BASE_LATITUDE, Lng = t.BASE_LONGITUDE, Type = "B",ShowPin=false }).ToList();
                foreach (var item in BaseLocations)
                {
                    SelectedUsers.Add(item);
                }
            }
            return SelectedUsers;
        }

        public List<LocationModel> GetHistoryLocation(int CompanyId, int UserId, DateTime? StartDate, DateTime? EndDate, string Mode)
        {
            if (Mode == null)
            {
                var y = (from t in entity.tblUserLogs
                         where t.USERID == UserId && t.LAT.ToLower() != "unknown" && t.LNG.ToLower() != "unknown" && (t.LOGTIME >= StartDate || StartDate == null) && (t.LOGTIME < EndDate || EndDate == null)
                         select t).ToList();
                List<LocationModel> UserHistory = (from t in y
                                                   join t1 in entity.tblUserMasters on t.USERID equals t1.ID
                                                   orderby t.LOGTIME descending
                                                   select new LocationModel
                                                   {UserId=t1.ID, Name = t1.FIRSTNAME + " " + t1.SURNAME, Address = t.LOGTIME.ToString("dd/MM/yyyy   hh:mm:ss tt"), Lat = t.LAT, Lng = t.LNG, Type = "U" }).ToList();
                return UserHistory;
            }
            else
            {
                var y = (from t in entity.tblEmergencies
                         where t.ID == UserId && t.LAT.ToLower() != "unknown" && t.LNG.ToLower() != "unknown" 
                         select t).ToList();
                List<LocationModel> UserHistory = (from t in y
                                                   join t1 in entity.tblUserMasters on t.USERID equals t1.ID
                                                   orderby t.LOGTIME descending
                                                   select new LocationModel
                                                   {UserId=t1.ID, Name = t1.FIRSTNAME + " " + t1.SURNAME, Address = t.LOGTIME.ToString("dd/MM/yyyy   hh:mm:ss tt"), Lat = t.LAT, Lng = t.LNG, Type = "U" }).ToList();
                return UserHistory;
            }

        }
    }
}
