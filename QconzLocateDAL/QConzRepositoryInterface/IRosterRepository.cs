using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepositoryInterface
{
    public interface IRosterRepository
    {
        List<RosterModel> GetAllRoster(int CompanyId,string Status);
        RosterModel GetRosterDetails(int Id);
        void SaveRosterDetails(RosterModel RosterModel);
    }
}
