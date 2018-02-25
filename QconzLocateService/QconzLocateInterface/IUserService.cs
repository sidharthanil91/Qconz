using QconzLocateService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.QconzLocateInterface
{
    public interface IUserService
    {
        List<UserServiceModel> GetAllUsers(int CompanyId,string Status);
        UserServiceModel GetUserDetails(int Id);
        string SaveUserDetails(UserServiceModel UserModel);
    }
}
