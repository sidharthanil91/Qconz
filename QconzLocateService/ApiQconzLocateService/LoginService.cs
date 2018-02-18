using QconzLocateDAL.ApiQconzRepository;
using QconzLocateDAL.ApiQconzRepositoryInterface;
using QconzLocateDAL.QConzRepositoryModel;
using QconzLocateService.ApiQconzLocateInterface;
using QconzLocateService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace QconzLocateService.ApiQconzLocateService
{
    public class LoginService:ILoginService
    {
        private ILoginRepository _ILoginRepository = new LoginRepository();

        public ClientTokenServiceModel ValidateUser(LoginServiceModel Login)
        {
            try
            {
                var LoginDetails = new LoginModel()
                {
                    UserName = Login.UserName,
                    Password = Login.Password
                };
                var Status = _ILoginRepository.ValidateUser(LoginDetails);
                ClientTokenServiceModel ClientToken = new ClientTokenServiceModel();
                if (Status > 0)
                {
                    var UserData = new TokenServiceModel()
                    {
                        UserId = Status,
                        CompanyId = 1
                    };
                    var token = GenerateToken(UserData);
                    _ILoginRepository.SaveToken(token, Status);
                    ClientToken.Token = token;
                    ClientToken.Message = "Success";
                    ClientToken.Status = "1";
                    return ClientToken;
                }
                else
                {
                    ClientToken.Token = null;
                    ClientToken.Message = "Failed";
                    ClientToken.Status = "0";
                    return ClientToken;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
           
        }

        public String GenerateToken(TokenServiceModel UserData)
        {
            try
            {
                byte[] data;
                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    bf.Serialize(ms, UserData);
                    data = ms.ToArray();
                }
                var encryptedData = MachineKey.Protect(data, "TokenDataUrl");
                var token = Convert.ToBase64String(encryptedData);
                return token;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public TokenServiceModel ValidateToken(string token)
        {
            try
            {
                byte[] encryptedData = Convert.FromBase64String(token);
                var data = MachineKey.Unprotect(encryptedData, "TokenDataUrl");
                TokenServiceModel content;
                BinaryFormatter bf = new BinaryFormatter();
                using (var ms = new MemoryStream(data))
                {
                    object obj = bf.Deserialize(ms);
                    content = (TokenServiceModel)obj;
                }
                if (_ILoginRepository.ValidateToken(token, content.UserId))
                {
                    return content;
                }
                return null;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
