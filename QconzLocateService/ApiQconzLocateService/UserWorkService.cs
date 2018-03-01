using QconzLocateService.QconzLocateInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QconzLocateService.Models;
using QconzLocateDAL.ApiQconzRepositoryInterface;
using QconzLocateDAL.ApiQconzRepository;

namespace QconzLocateService.QconzLocateService
{
    public class UserWorkService : IUserWorkService
    {
        private IUserWorkRepository _IUserWorkRepository = new UserWorkRepository();
        public OverRideFullServiceModel GetUserWorkRoster(int UserId,DateTime Date)
        {
            var y=_IUserWorkRepository.GetUserWorkRoster(UserId,Date);

            return new OverRideFullServiceModel()
            {
                StartTime = y.StartTime,
                FinishTime = y.FinishTime,
                Days = y.Days,
                OverRides = y.OverRides.Select(t => new OverRideServiceModel
                {
                    EndDate=t.EndDate,
                    FinishTime=t.FinishTime,
                    StartDate=t.StartDate,
                    StartTime=t.StartTime
                }).ToList()
            };
        }
    }
}
