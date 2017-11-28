using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepositoryInterface
{
    interface IUserRepository
    {
        List<UserModel> GetAllUsers();
        UserModel GetUserDetails(int Id);
    }
}
