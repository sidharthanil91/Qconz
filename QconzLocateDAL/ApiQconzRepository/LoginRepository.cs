using QconzLocateDAL.ApiQconzRepositoryInterface;
using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.ApiQconzRepository
{
    public class LoginRepository:ILoginRepository
    {
        QCONZEntities entity = new QCONZEntities();

        public void SaveToken(string Token, int UserId,string DeviceId)
        {
            var y = entity.tblUserMasters.Where(t => t.ID == UserId).Select(x => x).FirstOrDefault();
            y.USERTOKEN = Token;
            entity.SaveChanges();
        }

        public bool ValidateToken(string Token, int UserId)
        {
            if(entity.tblUserMasters.Where(t=>t.ID==UserId).Select(x=>x.USERTOKEN).FirstOrDefault()==Token)
            {
                return true;
            }
            return false;
        }

        public int ValidateUser(LoginModel Login)
        {
            if(entity.tblUserMasters.Where(t=>t.EMAIL.ToLower()==Login.UserName.ToLower() &&(t.USERTYPE==4 || t.USERTYPE==3 || t.USERTYPE == 2) && t.PASSWORD==Login.Password && t.USERSTATUS=="A").Count()>0)
            {
                return entity.tblUserMasters.Where(t=>t.EMAIL==Login.UserName).Select(x=>x.ID).FirstOrDefault();
            }
            return 0;
        }
    }
}
