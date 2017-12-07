using QconzLocateDAL.QConzRepository;
using QconzLocateDAL.QConzRepositoryInterface;
using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepository
{
    public class LocationRepository:ILocationRepository
    {
        private QCONZEntities entity = new QCONZEntities();
        public  List<LocationModel> GetCustomerLocation()
        {
            var y = (from t in entity.tblCustomers select new { t.LAT, t.LNG, t.ADDRESS1, t.ADDRESS2 }).ToList();
            List<LocationModel> location;
            location = y.Select(c => new LocationModel
            {
               Lat=c.LAT,
               Lng=c.LNG,
               Address=c.ADDRESS1+" "+c.ADDRESS2
            }
               ).ToList();
            return location;

        }
    }
}
