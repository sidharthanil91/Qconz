using QconzLocateService.QconzLocateInterface;
using QconzLocateService.QconzLocateService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QconzLocate.Models;
using QconzLocateService.Models;
using System.IO;

namespace QconzLocate.Controllers
{
    [SessionExpireFilter]
    [Authorize(Roles = "SUPER,ADMIN")]
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
            try
            {
                int CompanyId = (int)(Session["CompanyId"]);
                CompanyListViewModel companies = new CompanyListViewModel();
                CompanyList = _ICompanyService.GetAllCompany(CompanyId).Select(c => new CompanyViewModel
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
                return View("Company", companies);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ActionResult CompanyDetails(int id)
        {
            try
            {
                CompanyViewModel CompanyDetails;
                if (id != 0)
                {
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
                    return View("CompanyDetails", CompanyDetails);
                }
                else
                {
                    CompanyDetails = new CompanyViewModel
                    {
                        Id = 0,
                        Address1 = null,
                        Address2 = null,
                        ContactName = null,
                        Email = null,
                        Lat = null,
                        Lng = null,
                        Phone1 = null,
                        Phone2 = null,
                        Title = null,
                        Website = null,
                        ZipCode = null
                    };
                    return View("CompanyDetails", CompanyDetails);
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public JsonResult SaveDetails(FormCollection data)
        {
            try
            {
                var pic = System.Web.HttpContext.Current.Request.Files["file"];
                var pic2 = System.Web.HttpContext.Current.Request.Params["company"];
                byte[] imgData;

                using (var reader = new BinaryReader(pic.InputStream))
                {
                    imgData = reader.ReadBytes(pic.ContentLength);
                }
                CompanyServiceModel CompanyModel;
                CompanyModel = new CompanyServiceModel()
                {
                    Id = Convert.ToInt32(data["Id"]),
                    Address1 = data["Address1"],
                    Address2 = data["Address2"],
                    ContactName = data["ContactName"],
                    Email = data["Email"],
                    Lat = data["Lat"],
                    Lng = data["Lng"],
                    Phone1 = data["Phone1"],
                    Phone2 = data["Phone2"],
                    Image = imgData,
                    Title = data["Title"],
                    Website = data["Website"],
                    ZipCode = data["ZipCode"]
                };
                _ICompanyService.SaveCompanyDetails(CompanyModel);
                bool success = true;
                return Json(success, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}