using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepositoryInterface
{
    public interface ITeamRepository
    {
        List<TeamModel> GetAllTeam();
        TeamModel GetTeamDetails(int Id);
        void SaveTeamDetails(TeamModel TeamModel);
    }
}
