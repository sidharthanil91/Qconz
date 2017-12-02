using QconzLocate.Models;
using QconzLocateService.QconzLocateInterface;
using QconzLocateService.QconzLocateService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QconzLocate.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerService _ICustomerService = new CustomerService();
        private List<CompanyViewModel> CompanyList = new List<CompanyViewModel>();
       
        // GET: Customer
        public ActionResult CustomerReport()
        {
            CustomerViewModel customer = new CustomerViewModel();
            var customers  = _ICustomerService.GetAllCustomer().Select(c => new CustomerListViewModel
            {
                Id = c.Id,
                Address1 = c.Address1,
                Address2 = c.Address2,
                AddedDate = c.AddedDate,
                CompanyId = c.CompanyId,
                CustomerCode = c.CustomerCode,
                FirstName = c.FirstName,
                LastName = c.LastName,
                OfficeName = c.OfficeName,
                Email = c.Email,
                Lat = c.Lat,
                Lng = c.Lng,
                Phone1 = c.Phone1,
                Phone2 = c.Phone2,
                Website = c.Website,
                ZipCode = c.ZipCode
            }).ToList();
            return View("Customer", customers);
        }

        //public ActionResult CompanyDetails(int id)
        //{
        //    CompanyViewModel CompanyDetails;
        //    if (id != 0)
        //    {
        //        var y = _ICompanyService.GetCompanyDetails(id);
        //        CompanyDetails = new CompanyViewModel
        //        {
        //            Id = y.Id,
        //            Address1 = y.Address1,
        //            Address2 = y.Address2,
        //            ContactName = y.ContactName,
        //            Email = y.Email,
        //            Lat = y.Lat,
        //            Lng = y.Lng,
        //            Phone1 = y.Phone1,
        //            Phone2 = y.Phone2,
        //            Title = y.Title,
        //            Website = y.Website,
        //            ZipCode = y.ZipCode
        //        };
        //        return View("CompanyDetails", CompanyDetails);
        //    }
        //    else
        //    {
        //        CompanyDetails = new CompanyViewModel
        //        {
        //            Id = 0,
        //            Address1 = null,
        //            Address2 = null,
        //            ContactName = null,
        //            Email = null,
        //            Lat = null,
        //            Lng = null,
        //            Phone1 = null,
        //            Phone2 = null,
        //            Title = null,
        //            Website = null,
        //            ZipCode = null
        //        };
        //        return View("CompanyDetails", CompanyDetails);
        //    }
        //}

        //[HttpPost]
        //public JsonResult SaveDetails(CompanyViewModel company)
        //{
        //    CompanyServiceModel CompanyModel;
        //    CompanyModel = new CompanyServiceModel()
        //    {
        //        Id = company.Id,
        //        Address1 = company.Address1,
        //        Address2 = company.Address2,
        //        ContactName = company.ContactName,
        //        Email = company.Email,
        //        Lat = company.Lat,
        //        Lng = company.Lng,
        //        Phone1 = company.Phone1,
        //        Phone2 = company.Phone2,
        //        Title = company.Title,
        //        Website = company.Website,
        //        ZipCode = company.ZipCode
        //    };
        //    _ICompanyService.SaveCompanyDetails(CompanyModel);
        //    bool success = true;
        //    return Json(success, JsonRequestBehavior.AllowGet);
        //}
    }
}