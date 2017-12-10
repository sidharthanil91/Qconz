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
         List<CompanyModel> GetAllCompany(int CompanyId);
         CompanyModel GetCompanyDetails(int Id);
         void SaveCompanyDetails(CompanyModel CompanyModel);
    }
}
