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
    public class CompanyService:ICompanyService
    {
        private ICompanyRepository _ICompanyRepository = new CompanyRepository();
        //Get all companies
        public List<CompanyServiceModel> GetAllCompany(int CompanyId,string Archive)
        {
            try
            {
                var y = _ICompanyRepository.GetAllCompany(CompanyId, Archive);
                return y.Select(c => new CompanyServiceModel
                {
                    Id = c.Id,
                    Address1 = c.Address1,
                    Address2 = c.Address2,
                    ContactName = c.ContactName,
                    City=c.City,
                    Email = c.Email,
                    Lat = c.Lat,
                    Lng = c.Lng,
                    Phone1 = c.Phone1,
                    Phone2 = c.Phone2,
                    Title = c.Title,
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

        public CompanyServiceModel GetCompanyDetails(int Id)
        {
            try
            {
                var y = _ICompanyRepository.GetCompanyDetails(Id);
                return new CompanyServiceModel
                {
                    Id = y.Id,
                    Address1 = y.Address1,
                    Address2 = y.Address2,
                    City=y.City,
                    Image=y.Image,
                    ContactName = y.ContactName,
                    Email = y.Email,
                    Lat = y.Lat,
                    Lng = y.Lng,
                    Phone1 = y.Phone1,
                    Phone2 = y.Phone2,
                    Title = y.Title,
                    Website = y.Website,
                    ZipCode = y.ZipCode,
                    Archive=y.Archive
                
                };
            }
            catch
            {
                return null;
            }
        }

        public void SaveCompanyDetails(CompanyServiceModel CompanyDetails)
        {

            var company = new CompanyModel()
            {
                Id = CompanyDetails.Id,
                Address1 = CompanyDetails.Address1,
                Address2 = CompanyDetails.Address2,
                ContactName = CompanyDetails.ContactName,
                City=CompanyDetails.City,
                Email = CompanyDetails.Email,
                Image=CompanyDetails.Image,
                Lat = CompanyDetails.Lat,
                Lng = CompanyDetails.Lng,
                Phone1 = CompanyDetails.Phone1,
                Phone2 = CompanyDetails.Phone2,
                Title = CompanyDetails.Title,
                Website = CompanyDetails.Website,
                ZipCode = CompanyDetails.ZipCode,
                Archive=CompanyDetails.Archive
            };
            _ICompanyRepository.SaveCompanyDetails(company);
        }
    }
}
