using QconzLocateDAL.QConzRepository;
using QconzLocateDAL.QConzRepositoryModel;
using QconzLocateService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.QconzLocateService
{
    public class CommonService
    {
        CommonRepository _commonRepository = new CommonRepository();
        public CommonServiceModel GetCompanySelectList(int CompanyId)
        {
            CommonServiceModel CommonServiceModel = new CommonServiceModel();
            List<SelectLists> CompanySelectList = new List<SelectLists>();
            var y = _commonRepository.GetCompanySelectList(CompanyId);
           foreach(var item in y.CompanyList)
            {
                CompanySelectList.Add(new SelectLists { Id = item.Id, Text = item.Text });
            }
            CommonServiceModel.CompanyList = CompanySelectList;
            return CommonServiceModel;
        }

        public CommonServiceModel GetGroupSelectList(int CompanyId)
        {
            CommonServiceModel CommonServiceModel = new CommonServiceModel();
            List<SelectLists> GroupSelectList = new List<SelectLists>();
            var y = _commonRepository.GetGroupSelectList(CompanyId);
            foreach (var item in y.GroupList)
            {
                GroupSelectList.Add(new SelectLists { Id = item.Id, Text = item.Text });
            }
            CommonServiceModel.GroupList = GroupSelectList;
            return CommonServiceModel;
        }

        public CommonServiceModel GetCustomerSelectList(int CompanyId)
        {
            CommonServiceModel CommonServiceModel = new CommonServiceModel();
            List<SelectLists> CustomerSelectList = new List<SelectLists>();
            var y = _commonRepository.GetCustomerSelectList(CompanyId);
            foreach (var item in y.GroupList)
            {
                CustomerSelectList.Add(new SelectLists { Id = item.Id, Text = item.Text });
            }
            CommonServiceModel.CustomerList = CustomerSelectList;
            return CommonServiceModel;
        }

        public CommonServiceModel GetUserSelectList(int CompanyId)
        {
            CommonServiceModel CommonServiceModel = new CommonServiceModel();
            List<SelectLists> UserSelectList = new List<SelectLists>();
            var y = _commonRepository.GetUserSelectList(CompanyId);
            foreach (var item in y.UserList)
            {
                UserSelectList.Add(new SelectLists { Id = item.Id, Text = item.Text });
            }
            CommonServiceModel.UserList = UserSelectList;
            return CommonServiceModel;
        }
        public CommonServiceModel GetMapUserSelectList(int CompanyId,int GroupId)
        {
            CommonServiceModel CommonServiceModel = new CommonServiceModel();
            List<SelectLists> UserSelectList = new List<SelectLists>();
            var y = _commonRepository.GetMapUserSelectList(CompanyId,GroupId);
            foreach (var item in y.UserList)
            {
                UserSelectList.Add(new SelectLists { Id = item.Id, Text = item.Text });
            }
            CommonServiceModel.UserList = UserSelectList;
            return CommonServiceModel;
        }

        public UserAuthServiceModel GetLoginUserDetails(UserAuthServiceModel user)
        {
            UserAuthRepositoryModel UserDetails = new UserAuthRepositoryModel();
            UserDetails.Email = user.Email;
            UserDetails.Password = user.Password;
            var y = _commonRepository.GetLoginUserDetails(UserDetails);
            if(y==null)
            {
                return null;
            }
            UserAuthServiceModel UserAuthDetails = new UserAuthServiceModel();
            UserAuthDetails.Email = y.Email;
            UserAuthDetails.CompanyId = y.CompanyId;
            UserAuthDetails.Roles = y.Roles;
            UserAuthDetails.Image = y.Image == null ? "" : y.Image;
            return UserAuthDetails;
            
        }
    }
}
