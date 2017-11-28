using QconzLocateDAL.QConzRepository;
using QconzLocateService.Models;
using QconzLocateService.QconzLocateInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.QconzLocateService
{
    public class UserService:IUserService
    {
        private UserRepository _IUserRepository = new UserRepository();
        //Get all companies
        public List<UserServiceModel> GetAllUsers()
        {
            try
            {
                var y = _IUserRepository.GetAllUsers();
                return y.Select(c => new UserServiceModel
                {
                    Id = c.Id,
                    Cellphone = c.Cellphone,
                    CompanyId = c.CompanyId,
                    EmergencyContact = c.EmergencyContact,
                    Email = c.Email,
                    EndTime = c.EndTime,
                    FirstName = c.FirstName,
                    Password = c.Password,
                    StartTime = c.StartTime,
                    SurName = c.SurName,
                    UserName = c.UserName,
                    UserStatus = c.UserStatus,
                    UserTeamId = c.UserTeamId,
                    UserToken = c.UserToken,
                    UserType = c.UserType,
                    WorkingDays = c.WorkingDays
                }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Get companies by Id

        public UserServiceModel GetUserDetails(int Id)
        {
            try
            {
                var c = _IUserRepository.GetUserDetails(Id);
                return new UserServiceModel
                {
                    Id = c.Id,
                    Cellphone = c.Cellphone,
                    CompanyId = c.CompanyId,
                    EmergencyContact = c.EmergencyContact,
                    Email = c.Email,
                    EndTime = c.EndTime,
                    FirstName = c.FirstName,
                    Password = c.Password,
                    StartTime = c.StartTime,
                    SurName = c.SurName,
                    UserName = c.UserName,
                    UserStatus = c.UserStatus,
                    UserTeamId = c.UserTeamId,
                    UserToken = c.UserToken,
                    UserType = c.UserType,
                    WorkingDays = c.WorkingDays
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
