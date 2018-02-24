using QconzLocateDAL.QConzRepository;
using QconzLocateDAL.QConzRepositoryInterface;
using QconzLocateDAL.QConzRepositoryModel;
using QconzLocateService.Models;
using QconzLocateService.QconzLocateInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.QconzLocateService
{
    public class TeamService: ITeamService
    {
        private ITeamRepository _ITeamRepository = new TeamRepository();
        //Get all companies
        public List<TeamServiceModel> GetAllTeam(int CompanyId,string Status)
        {
            try
            {
                var y = _ITeamRepository.GetAllTeam(CompanyId, Status);
                return y.Select(c => new TeamServiceModel
                {
                    Id = c.Id,
                    CompanyId=c.CompanyId,
                    TeamCreatedDate=c.TeamCreatedDate,
                    Teamdesc=c.Teamdesc,
                    TeamName=c.TeamName,
                    TeamStatus=c.TeamStatus,
                    CompanyName=c.CompanyName
                }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Get companies by Id

        public TeamServiceModel GetTeamDetails(int Id)
        {
            try
            {
                var c = _ITeamRepository.GetTeamDetails(Id);
                return new TeamServiceModel
                {
                    Id = c.Id,
                    CompanyId = c.CompanyId,
                    TeamCreatedDate = c.TeamCreatedDate,
                    Teamdesc = c.Teamdesc,
                    TeamName = c.TeamName,
                    TeamStatus = c.TeamStatus,
                    UserId=c.UserId
                };
            }
            catch
            {
                return null;
            }
        }

        public void SaveTeamDetails(TeamServiceModel TeamDetails)
        {

            var team = new TeamModel()
            {
                Id = TeamDetails.Id,
                CompanyId = TeamDetails.CompanyId,
                UserId=TeamDetails.UserId,
                TeamCreatedDate = TeamDetails.TeamCreatedDate,
                Teamdesc = TeamDetails.Teamdesc,
                TeamName = TeamDetails.TeamName,
                TeamStatus = TeamDetails.TeamStatus
            };
            _ITeamRepository.SaveTeamDetails(team);
        }
    }
}
