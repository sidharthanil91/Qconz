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
    public class RosterService: IRosterService
    {
        private IRosterRepository _IRosterRepository = new RosterRepository();
        //Get all companies
        public List<RosterServiceModel> GetAllRoster()
        {
            try
            {
                var y = _IRosterRepository.GetAllRoster();
                return y.Select(c => new RosterServiceModel
                {
                    Id = c.Id,
                    EndDate=c.EndDate,
                    FinishTime=c.FinishTime,
                    StartDate=c.StartDate,
                    StartTime=c.StartTime,
                    UserId=c.UserId
                }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Get companies by Id

        public RosterServiceModel GetRosterDetails(int Id)
        {
            try
            {
                var c = _IRosterRepository.GetRosterDetails(Id);
                return new RosterServiceModel
                {
                    Id = c.Id,
                    EndDate = c.EndDate,
                    FinishTime = c.FinishTime,
                    StartDate = c.StartDate,
                    StartTime = c.StartTime,
                    UserId = c.UserId
                };
            }
            catch
            {
                return null;
            }
        }

        public void SaveRosterDetails(RosterServiceModel RosterDetails)
        {

            var roster = new RosterModel()
            {
                Id = RosterDetails.Id,
                EndDate = RosterDetails.EndDate,
                FinishTime = RosterDetails.FinishTime,
                StartDate = RosterDetails.StartDate,
                StartTime = RosterDetails.StartTime,
                UserId = RosterDetails.UserId
            };
            _IRosterRepository.SaveRosterDetails(roster);
        }
    }
}
