using QconzLocateDAL.QConzRepository;
using QconzLocateDAL.QConzRepositoryInterface;
using QconzLocateDAL.QConzRepositoryModel;
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
        private IUserRepository _IUserRepository = new UserRepository();
        //Get all companies
        public List<UserServiceModel> GetAllUsers(int CompanyId)
        {
            try
            {
                var y = _IUserRepository.GetAllUsers(CompanyId);
                return y.Select(c => new UserServiceModel
                {
                    Id = c.Id,
                    Cellphone = c.Cellphone,
                    CompanyId = c.CompanyId,
                    Company=c.Company,
                    EmergencyContact = c.EmergencyContact,
                    EmergencyContactNo=c.EmergencyContactNo,
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
                    EmergencyContactNo=c.EmergencyContactNo,
                    Email = c.Email,
                    EndTime = c.EndTime,
                    FirstName = c.FirstName,
                    Password = c.Password,
                    StartTime = c.StartTime,
                    SurName = c.SurName,
                    UserGroups=c.UserGroups,
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

        public string SaveUserDetails(UserServiceModel UserDetails)
        {

            var user = new UserModel()
            {
                Id = UserDetails.Id,
                Cellphone = UserDetails.Cellphone,
                CompanyId = UserDetails.CompanyId,
                EmergencyContact = UserDetails.EmergencyContact,
                EmergencyContactNo=UserDetails.EmergencyContactNo,
                Email = UserDetails.Email,
                EndTime = UserDetails.EndTime,
                FirstName = UserDetails.FirstName,
                Password = UserDetails.Password,
                StartTime = UserDetails.StartTime,
                SurName = UserDetails.SurName,
                UserGroups=UserDetails.UserGroups,
                UserName = UserDetails.UserName,
                UserStatus = UserDetails.UserStatus,
                UserTeamId = UserDetails.UserTeamId,
                UserToken = UserDetails.UserToken,
                UserType = UserDetails.UserType,
                WorkingDays = UserDetails.WorkingDays
            };
            return _IUserRepository.SaveUserDetails(user);
        }
    }
}
