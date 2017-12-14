using QconzLocateService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.ApiQconzLocateInterface
{
    public interface ILoginService
    {
        ClientTokenServiceModel ValidateUser(LoginServiceModel Login);
        TokenServiceModel ValidateToken(string Token);
    }
}
