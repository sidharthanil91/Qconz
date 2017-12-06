using QconzLocateDAL.QConzRepositoryInterface;
using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepository
{
    public class TeamRepository: ITeamRepository
    {
        QCONZEntities entity = new QCONZEntities();

        public List<TeamModel> GetAllTeam(int CompanyId)
        {
            try
            {
                List<TeamModel> TeamList = new List<TeamModel>();
                // var y = (from t in entity.tblTeams select t).ToList();
                var y = entity.tblTeams.Select(t1=>t1).Where(t => t.COMPANYID == CompanyId || CompanyId == 0).ToList();
                TeamList = y.Select(c => new TeamModel
                {
                    Id = c.ID,
                    CompanyName=c.tblOrganization.TITLE,
                    TeamCreatedDate=c.TEAMCREATED,
                    Teamdesc=c.TEAMDESC,
                    TeamName=c.TEAMNAME,
                    TeamStatus=c.TEAMSTATUS
                }
                ).ToList();
                return TeamList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TeamModel GetTeamDetails(int Id)
        {
            try
            {
                var y = (from c in entity.tblTeams
                         where c.ID == Id
                         select new TeamModel
                         {
                             Id = c.ID,
                             CompanyId=c.COMPANYID,
                             TeamCreatedDate = c.TEAMCREATED,
                             Teamdesc = c.TEAMDESC,
                             TeamName = c.TEAMNAME,
                             TeamStatus = c.TEAMSTATUS
                         }).FirstOrDefault();
                return y;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void SaveTeamDetails(TeamModel TeamModel)
        {
            if (TeamModel.Id == 0)
            {
                var team = new tblTeam()
                {
                    COMPANYID = TeamModel.CompanyId,
                    TEAMCREATED = DateTime.Now,
                    TEAMDESC=TeamModel.Teamdesc,
                    TEAMNAME=TeamModel.TeamName,
                    TEAMSTATUS="A"
                };
                entity.tblTeams.Add(team);
            }
            else
            {
                var y = entity.tblTeams.FirstOrDefault(t => t.ID == TeamModel.Id);
                y.COMPANYID = TeamModel.CompanyId;
                y.TEAMCREATED = DateTime.Now;
                y.TEAMDESC = TeamModel.Teamdesc;
                y.TEAMNAME = TeamModel.TeamName;
                y.TEAMSTATUS = "A";
            }
            entity.SaveChanges();
        }
    }
}
