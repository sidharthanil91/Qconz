using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepositoryInterface
{
    public interface IUserRepository
    {
        List<UserModel> GetAllUsers(int CompanyId);
        UserModel GetUserDetails(int Id);
        void SaveUserDetails(UserModel UserModel);
    }
}
