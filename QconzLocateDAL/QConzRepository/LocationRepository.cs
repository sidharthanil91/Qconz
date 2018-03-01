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
    public class LocationRepository:ILocationRepository
    {
        private QCONZEntities entity = new QCONZEntities();
        public  List<LocationModel> GetCustomerLocation(int CompanyId, string Customer,int UserId, int GroupId)
        {
            var y = (from t in entity.tblCustomers where ((t.COMPANYID == CompanyId || CompanyId == 0)) select t).Where(x => x.CUSTOMERCODE.Contains(Customer) || x.OFFICENAME.Contains(Customer) ||
               x.LAT.Contains(Customer) || x.LNG.Contains(Customer) || x.ADDRESS1.Contains(Customer) || (x.ADDRESS2).Contains(Customer) || (Customer == null));
            List<LocationModel> location;
            location = y.Select(c => new LocationModel
            {
               Lat=c.LAT,
               Lng=c.LNG,
               Address=c.ADDRESS1+" "+c.ADDRESS2,
               Type="C"
            }
             ).ToList();
            var users = GetUserLocation(CompanyId, UserId, GroupId);
            foreach(var item in users)
            {
                location.Add(item);
            }
            return location;

        }

        public List<LocationModel> GetUserLocation(int CompanyId, int UserId, int GroupId)
        {

            List<tblUserLog> users =entity.tblUserLogs.GroupBy(x => x.USERID).Select(t =>t.OrderByDescending(c => c.ID).Where(t1 => t1.LAT.ToLower() != "unknown" && t1.LNG.ToLower() != "unknown").FirstOrDefault()).ToList();
            users.RemoveAll(item => item == null);
            List<LocationModel> SelectedUsers = (from t in users
                                                 join t1 in entity.tblUserMasters on t.USERID equals t1.ID
                                                 where (CompanyId==0 ||t1.COMPANYID == CompanyId) && (GroupId==0 || t1.tblUserTeams.Select(t2=>t2.TEAMID).Contains(GroupId)) &&(UserId==0 || t.USERID==UserId)
                                                 select new LocationModel
                                                 {UserId=t1.ID, Address = t1.FIRSTNAME + " " + t1.SURNAME+":"+t.LOGTIME , Lat = t.LAT, Lng = t.LNG ,Type="U"}).ToList();

            return SelectedUsers;
        }

        public List<LocationModel> GetHistoryLocation(int CompanyId, int UserId, DateTime? Date)
        {
            List<LocationModel> UserHistory= (from t in entity.tblUserLogs
                                              join t1 in entity.tblUserMasters on t.USERID equals t1.ID
                                              where (CompanyId == 0 || t1.COMPANYID == CompanyId)  && (UserId == 0 || t.USERID == UserId) &&(t.LAT.ToLower() != "unknown") && (t.LNG.ToLower() != "unknown") && (Date==null || DbFunctions.TruncateTime(t.LOGTIME)==Date)
                                              orderby t.LOGTIME
                                              select new LocationModel
                                              { Address = t1.FIRSTNAME + " " + t1.SURNAME + ":" + t.LOGTIME, Lat = t.LAT, Lng = t.LNG, Type = "U" }).ToList();
            return UserHistory;
        }
    }
}
