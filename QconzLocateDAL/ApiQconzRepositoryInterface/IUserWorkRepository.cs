using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.ApiQconzRepositoryInterface
{
    public interface IUserWorkRepository
    {
        RosterModel GetUserWorkRoster(int UserId,DateTime Date);
    }
}
