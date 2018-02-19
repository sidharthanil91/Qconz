using QconzLocateService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.QconzLocateInterface
{
    public interface ITeamService
    {
        List<TeamServiceModel> GetAllTeam(int CompanyId, string Status);
        void SaveTeamDetails(TeamServiceModel TeamModel);
        TeamServiceModel GetTeamDetails(int Id);
    }
}
