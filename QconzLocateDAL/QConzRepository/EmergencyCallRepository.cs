using QconzLocateDAL.QConzRepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QconzLocateDAL.QConzRepositoryModel;

namespace QconzLocateDAL.QConzRepository
{
    public class EmergencyCallRepository : IEmergencyCallRepository
    {
        private QCONZEntities entity = new QCONZEntities();
        public List<EmergencyCallModel> GetEmergencyCall(int CompanyId, int UserId)
        {
            List<EmergencyCallModel> EmergencyCallList = (from t in entity.tblEmergencies
                                                          where (t.tblUserMaster.ID == UserId || UserId == 0) && (t.tblUserMaster.tblOrganization.ID == CompanyId || CompanyId == 0)
                                                          select new EmergencyCallModel
            {UserId=t.ID, DateTime = t.LOGTIME, Latitude = t.LAT, Longitude = t.LNG, User = t.tblUserMaster.FIRSTNAME + " " + t.tblUserMaster.SURNAME }).ToList();
            return EmergencyCallList;
        }
    }
}
