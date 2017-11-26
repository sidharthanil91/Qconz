using QconzLocateService.QconzLocateInterface;
using QconzLocateService.QconzLocateService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QconzLocate.Models;

namespace QconzLocate.Controllers
{
    public class CompanyController : Controller
    {
        private ICompanyService _ICompanyService = new CompanyService();
        private List<CompanyViewModel> CompanyList = new List<CompanyViewModel>();
        public CompanyController()
        {
            
        }
        // GET: Company
        public ActionResult CompanyReport()
        {
            CompanyListViewModel companies = new CompanyListViewModel();
            CompanyList = _ICompanyService.GetAllCompany().Select(c => new CompanyViewModel
            {
                Id = c.Id,
                Address1 = c.Address1,
                Address2 = c.Address2,
                ContactName = c.ContactName,
                Email = c.Email,
                Lat = c.Lat,
                Lng = c.Lng,
                Phone1 = c.Phone1,
                Phone2 = c.Phone2,
                Title = c.Title,
                Website = c.Website,
                ZipCode = c.ZipCode
            }).ToList();
            companies.Companies = CompanyList;
          //  return Content("Hello");
            return View("Company", companies);
        }

        public ActionResult CompanyDetails(int id)
        {
            CompanyViewModel CompanyDetails;
            var y = _ICompanyService.GetCompanyDetails(id);
            CompanyDetails = new CompanyViewModel
            {
                Id = y.Id,
                Address1 = y.Address1,
                Address2 = y.Address2,
                ContactName = y.ContactName,
                Email = y.Email,
                Lat = y.Lat,
                Lng = y.Lng,
                Phone1 = y.Phone1,
                Phone2 = y.Phone2,
                Title = y.Title,
                Website = y.Website,
                ZipCode = y.ZipCode
            };
            return Content(CompanyDetails.Title);
        }
    }
}