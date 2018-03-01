using QconzLocateService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.QconzLocateInterface
{
    public interface IUserWorkService
    {
        OverRideFullServiceModel GetUserWorkRoster(int UserId,DateTime Date);
    }
}
