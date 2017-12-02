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
    public class CustomerService: ICustomerService
    {
        private ICustomerRepository _ICustomerRepository = new CustomerRepository();
        //Get all companies
        public List<CustomerServiceModel> GetAllCustomer()
        {
            try
            {
                var y = _ICustomerRepository.GetAllCustomer();
                return y.Select(c => new CustomerServiceModel
                {
                    Id = c.Id,
                    Address1 = c.Address1,
                    Address2 = c.Address2,
                    AddedDate=c.AddedDate,
                    CompanyId=c.CompanyId,
                    CustomerCode=c.CustomerCode,
                    FirstName=c.FirstName,
                    LastName=c.LastName,
                    OfficeName=c.OfficeName,
                    Email = c.Email,
                    Lat = c.Lat,
                    Lng = c.Lng,
                    Phone1 = c.Phone1,
                    Phone2 = c.Phone2,
                    Website = c.Website,
                    ZipCode = c.ZipCode
                }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Get companies by Id

        public CustomerServiceModel GetCustomerDetails(int Id)
        {
            try
            {
                var c = _ICustomerRepository.GetCustomerDetails(Id);
                return new CustomerServiceModel
                {
                    Id = c.Id,
                    Address1 = c.Address1,
                    Address2 = c.Address2,
                    AddedDate = c.AddedDate,
                    CompanyId = c.CompanyId,
                    CustomerCode = c.CustomerCode,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    OfficeName = c.OfficeName,
                    Email = c.Email,
                    Lat = c.Lat,
                    Lng = c.Lng,
                    Phone1 = c.Phone1,
                    Phone2 = c.Phone2,
                    Website = c.Website,
                    ZipCode = c.ZipCode
                };
            }
            catch
            {
                return null;
            }
        }

        public void SaveCustomerDetails(CustomerServiceModel CustomerDetails)
        {

            var customer = new CustomerModel()
            {
                Id = CustomerDetails.Id,
                Address1 = CustomerDetails.Address1,
                Address2 = CustomerDetails.Address2,
                AddedDate = CustomerDetails.AddedDate,
                CompanyId = CustomerDetails.CompanyId,
                CustomerCode = CustomerDetails.CustomerCode,
                FirstName = CustomerDetails.FirstName,
                LastName = CustomerDetails.LastName,
                OfficeName = CustomerDetails.OfficeName,
                Email = CustomerDetails.Email,
                Lat = CustomerDetails.Lat,
                Lng = CustomerDetails.Lng,
                Phone1 = CustomerDetails.Phone1,
                Phone2 = CustomerDetails.Phone2,
                Website = CustomerDetails.Website,
                ZipCode = CustomerDetails.ZipCode
            };
            _ICustomerRepository.SaveCustomerDetails(customer);
        }
    }
}
