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
        public  List<LocationModel> GetCustomerLocation(int CompanyId, string Customer, int GroupId)
        {
             var y = (from t in entity.tblCustomers where ((t.COMPANYID==CompanyId || CompanyId==0) ) select t).Where(x=>x.CUSTOMERCODE.Contains(Customer)||x.OFFICENAME.Contains(Customer)||
             x.LAT.Contains(Customer)||x.LNG.Contains(Customer)||x.ADDRESS1.Contains(Customer)||(x.ADDRESS2).Contains(Customer));
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
