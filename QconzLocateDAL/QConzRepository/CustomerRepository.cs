using QconzLocateDAL.QConzRepositoryInterface;
using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        QCONZEntities entity = new QCONZEntities();

        public List<CustomerModel> GetAllCustomer(int CompanyId,string Status)
        {
            try
            {
                List<CustomerModel> CustomerList = new List<CustomerModel>();
                var y = (from t in entity.tblCustomers where (t.COMPANYID == CompanyId || CompanyId == 0) && t.ARCHIVE==Status select t).ToList();
                CustomerList = y.Select(c => new CustomerModel
                {
                    Id = c.ID,
                    Address1 = c.ADDRESS1,
                    Address2 = c.ADDRESS2,
                    City = c.CITY,
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
                             City = c.CITY,
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
                             ZipCode = c.ZIPCODE,
                             Archive=c.ARCHIVE
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
            try
            {
                if (CustomerModel.Id == 0)
                {
                    var customer = new tblCustomer()
                    {
                        ADDRESS1 = CustomerModel.Address1,
                        ADDRESS2 = CustomerModel.Address2,
                        CITY = CustomerModel.City,
                        ADDED_DATE = CustomerModel.AddedDate,
                        COMPANYID = CustomerModel.CompanyId,
                        CUSTOMERCODE = CustomerModel.CustomerCode,
                        FIRSTNAME = CustomerModel.FirstName,
                        LASTNAME = CustomerModel.LastName,
                        OFFICENAME = CustomerModel.OfficeName,
                        EMAIL = CustomerModel.Email,
                        LAT = CustomerModel.Lat,
                        LNG = CustomerModel.Lng,
                        PHONE_1 = CustomerModel.Phone1,
                        PHONE_2 = CustomerModel.Phone2,
                        WEBSITE = CustomerModel.Website,
                        ZIPCODE = CustomerModel.ZipCode,
                        ARCHIVE= "A"
                    };
                    entity.tblCustomers.Add(customer);
                }
                else
                {
                    var y = entity.tblCustomers.FirstOrDefault(t => t.ID == CustomerModel.Id);
                    y.ADDRESS1 = CustomerModel.Address1;
                    y.ADDRESS2 = CustomerModel.Address2;
                    y.CITY = CustomerModel.City;
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
                    y.ARCHIVE = CustomerModel.Archive;
                }
                entity.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public void SaveBulkCustomerDetails(List<CustomerModel> CustomerModel)
        {
            try
            {
                var CompanyId = CustomerModel.Select(t => t.CompanyId).FirstOrDefault();
                var customers = entity.tblCustomers.Where(c => c.COMPANYID == CompanyId);
                foreach (var item in customers)
                {
                    entity.tblCustomers.Remove(item);
                }
                foreach (var item in CustomerModel)
                {
                    var customer = new tblCustomer()
                    {
                        ADDRESS1 = item.Address1,
                        ADDRESS2 = item.Address2,
                        CITY = item.City,
                        ADDED_DATE = item.AddedDate,
                        COMPANYID = item.CompanyId,
                        CUSTOMERCODE = item.CustomerCode,
                        FIRSTNAME = item.FirstName,
                        LASTNAME = item.LastName,
                        OFFICENAME = item.OfficeName,
                        EMAIL = item.Email,
                        LAT = item.Lat,
                        LNG = item.Lng,
                        PHONE_1 = item.Phone1,
                        PHONE_2 = item.Phone2,
                        WEBSITE = item.Website,
                        ZIPCODE = item.ZipCode,
                        ARCHIVE="A"
                    };
                    entity.tblCustomers.Add(customer);
                }
                entity.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
