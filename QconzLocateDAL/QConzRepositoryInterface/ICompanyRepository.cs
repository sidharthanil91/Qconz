using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepositoryInterface
{
    public interface ICompanyRepository
    {
         List<CompanyModel> GetAllCompany(int CompanyId, string Archive);
         CompanyModel GetCompanyDetails(int Id);
         void SaveCompanyDetails(CompanyModel CompanyModel);
    }
}
