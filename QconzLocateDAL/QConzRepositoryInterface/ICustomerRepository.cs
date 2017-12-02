﻿using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepositoryInterface
{
    public interface ICustomerRepository
    {
        List<CustomerModel> GetAllCustomer();
        CustomerModel GetCustomerDetails(int Id);
        void SaveCustomerDetails(CustomerModel CustomerModel);
    }
}
