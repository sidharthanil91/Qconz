using QconzLocateService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.QconzLocateInterface
{
    public interface ICustomerService
    {
        List<CustomerServiceModel> GetAllCustomer();
        void SaveCustomerDetails(CustomerServiceModel CustomerModel);
        void SaveBulkCustomerDetails(List<CustomerServiceModel> CustomerModel);
        CustomerServiceModel GetCustomerDetails(int Id);
    }
}
