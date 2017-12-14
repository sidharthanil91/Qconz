using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.ApiQconzRepositoryInterface
{
    public interface ILoginRepository
    {
        int ValidateUser(LoginModel Login);
        void SaveToken(string Token,int UserId);
        bool ValidateToken(string Token, int UserId);
    }
}
