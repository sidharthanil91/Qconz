using QconzLocateDAL.QConzRepositoryInterface;
using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepository
{
    public class CustomerRepository: ICustomerRepository
    {
        QCONZEntities entity = new QCONZEntities();

        public List<CustomerModel> GetAllCustomer()
        {
            try
            {
                List<CustomerModel> CustomerList = new List<CustomerModel>();
                var y = (from t in entity.tblCustomers select t).ToList();
                CustomerList = y.Select(c => new CustomerModel
                {
                    Id = c.ID,
                    Address1 = c.ADDRESS1,
                    Address2 = c.ADDRESS2,
                    AddedDate= c.ADDED_DATE,
                    CompanyId= c.COMPANYID,
                    CustomerCode=c.CUSTOMERCODE,
                    FirstName=c.FIRSTNAME,
                    LastName=c.LASTNAME,
                    OfficeName=c.OFFICENAME,
                    Email = c.EMAIL,
                    Lat = c.LAT,
                    Lng = c.LNG,
                    Phone1 = c.PHONE_1,
                    Phone2 = c.PHONE_2,
                    Website = c.WEBSITE,
                    ZipCode = c.ZIPCODE
                }
                ).ToList();
                return CustomerList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public CustomerModel GetCustomerDetails(int Id)
        {
            try
            {
                var y = (from c in entity.tblCustomers
                         where c.ID == Id
                         select new CustomerModel
                         {
                             Id = c.ID,
                             Address1 = c.ADDRESS1,
                             Address2 = c.ADDRESS2,
                             AddedDate = c.ADDED_DATE,
                             CompanyId = c.COMPANYID,
                             CustomerCode = c.CUSTOMERCODE,
                             FirstName = c.FIRSTNAME,
                             LastName = c.LASTNAME,
                             OfficeName = c.OFFICENAME,
                             Email = c.EMAIL,
                             Lat = c.LAT,
                             Lng = c.LNG,
                             Phone1 = c.PHONE_1,
                             Phone2 = c.PHONE_2,
                             Website = c.WEBSITE,
                             ZipCode = c.ZIPCODE
                         }).FirstOrDefault();
                return y;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void SaveCustomerDetails(CustomerModel CustomerModel)
        {
            if (CustomerModel.Id == 0)
            {
                var customer = new tblCustomer()
                {
                    ADDRESS1 = CustomerModel.Address1,
                    ADDRESS2 = CustomerModel.Address2,
                    ADDED_DATE=CustomerModel.AddedDate,
                    COMPANYID=CustomerModel.CompanyId,
                    CUSTOMERCODE=CustomerModel.CustomerCode,
                    FIRSTNAME=CustomerModel.FirstName,
                    LASTNAME=CustomerModel.LastName,
                    OFFICENAME=CustomerModel.OfficeName,
                    EMAIL = CustomerModel.Email,
                    LAT = CustomerModel.Lat,
                    LNG = CustomerModel.Lng,
                    PHONE_1 = CustomerModel.Phone1,
                    PHONE_2 = CustomerModel.Phone2,                 
                    WEBSITE = CustomerModel.Website,
                    ZIPCODE = CustomerModel.ZipCode
                };
                entity.tblCustomers.Add(customer);
            }
            else
            {
                var y = entity.tblCustomers.FirstOrDefault(t => t.ID == CustomerModel.Id);
                    y.ADDRESS1 = CustomerModel.Address1;
                    y.ADDRESS2 = CustomerModel.Address2;
                    y.ADDED_DATE = CustomerModel.AddedDate;
                    y.COMPANYID = CustomerModel.CompanyId;
                    y.CUSTOMERCODE = CustomerModel.CustomerCode;
                    y.FIRSTNAME = CustomerModel.FirstName;
                    y.LASTNAME = CustomerModel.LastName;
                    y.OFFICENAME = CustomerModel.OfficeName;
                    y.EMAIL = CustomerModel.Email;
                    y.LAT = CustomerModel.Lat;
                    y.LNG = CustomerModel.Lng;
                    y.PHONE_1 = CustomerModel.Phone1;
                    y.PHONE_2 = CustomerModel.Phone2;                 
                    y.WEBSITE = CustomerModel.Website;
                    y.ZIPCODE = CustomerModel.ZipCode;
            }
            entity.SaveChanges();
        }
    }
}
