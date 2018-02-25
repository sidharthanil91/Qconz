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

        public List<TeamModel> GetAllTeam(int CompanyId,string Status)
        {
            try
            {
                List<TeamModel> TeamList = new List<TeamModel>();
                // var y = (from t in entity.tblTeams select t).ToList();
                var y = entity.tblTeams.Select(t1=>t1).Where(t => (t.COMPANYID == CompanyId || CompanyId == 0)&& t.TEAMSTATUS==Status).ToList();
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
                var users = entity.tblUserTeams.Where(t => t.TEAMID == Id).Select(t1 => t1.USERID).ToList();
                if(users!=null)
                y.UserId = string.Join(",", users.Select(t=>t.ToString()));
                return y;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void SaveTeamDetails(TeamModel TeamModel)
        {
            List<int> UserIds = new List<int>();
            if (TeamModel.UserId != null)
            {
                UserIds=TeamModel.UserId.Split(',').Select(int.Parse).ToList();
            }
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
                foreach (var item in UserIds)
                {
                    var userteam = new tblUserTeam()
                    {
                        ARCHIVE = "A",
                        TEAMID = team.ID,
                        USERID = item
                    };
                    entity.tblUserTeams.Add(userteam);
                }
            }
            else
            {
                var y = entity.tblTeams.FirstOrDefault(t => t.ID == TeamModel.Id);
                y.COMPANYID = TeamModel.CompanyId;
                y.TEAMCREATED = DateTime.Now;
                y.TEAMDESC = TeamModel.Teamdesc;
                y.TEAMNAME = TeamModel.TeamName;
                y.TEAMSTATUS = TeamModel.TeamStatus;
                var ExistingUserTeam = entity.tblUserTeams.Where(t => t.TEAMID == y.ID).Select(t1 => t1.USERID).ToList();
                var NewUserTeam = UserIds.Except(ExistingUserTeam);
                foreach (var item in NewUserTeam)
                {
                    var userteam = new tblUserTeam()
                    {
                        ARCHIVE = "A",
                        TEAMID = y.ID,
                        USERID = item
                    };
                    entity.tblUserTeams.Add(userteam);
                }
                var DeleteUserTeam = ExistingUserTeam.Except(UserIds);
                foreach (var item in DeleteUserTeam)
                {
                    var deleteItem = entity.tblUserTeams.FirstOrDefault(t => t.USERID == item && t.TEAMID == y.ID);
                    entity.tblUserTeams.Remove(deleteItem);
                }
            }
            entity.SaveChanges();
        }
    }
}
