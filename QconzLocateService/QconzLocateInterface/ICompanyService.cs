using QconzLocateService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.QconzLocateInterface
{
    public interface ICompanyService
    {
         List<CompanyServiceModel> GetAllCompany(int CompanyId, string Archive);
         void SaveCompanyDetails(CompanyServiceModel CompanyModel);
         CompanyServiceModel GetCompanyDetails(int Id);
         
    }
}
