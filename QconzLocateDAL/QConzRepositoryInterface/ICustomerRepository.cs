using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepositoryInterface
{
    public interface ICustomerRepository
    {
        List<CustomerModel> GetAllCustomer(int CompanyId, string Status);
        CustomerModel GetCustomerDetails(int Id);
        void SaveCustomerDetails(CustomerModel CustomerModel);
        void SaveBulkCustomerDetails(List<CustomerModel> CustomerModel);
    }
}
