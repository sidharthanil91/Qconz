using QconzLocateService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.QconzLocateInterface
{
    public interface IRosterService
    {
        List<RosterServiceModel> GetAllRoster(int CompanyId);
        void SaveRosterDetails(RosterServiceModel RosterModel);
        RosterServiceModel GetRosterDetails(int Id);
    }
}
