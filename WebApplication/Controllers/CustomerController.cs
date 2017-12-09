using ExcelDataReader;
using QconzLocate.Models;
using QconzLocateService.Models;
using QconzLocateService.QconzLocateInterface;
using QconzLocateService.QconzLocateService;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QconzLocate.Controllers
{
    [SessionExpireFilter]
    [Authorize(Roles = "SUPER,ADMIN")]
    public class CustomerController : Controller
    {
        private ICustomerService _ICustomerService = new CustomerService();
        private CommonService _commonService = new CommonService();
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

        public ActionResult CustomerDetails(int id)
        {
            int CompanyId = (int)(Session["CompanyId"]);
            CustomerViewModel CustomerViewModel = new CustomerViewModel();
            CustomerListViewModel CustomerDetails;
            var c = _ICustomerService.GetCustomerDetails(id);
            if (id != 0)
            {
                CustomerDetails = new CustomerListViewModel
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
                };
                
            }
            else
            {
                CustomerDetails = new CustomerListViewModel
                {
                    Id = 0,
                    Address1 = null,
                    Address2 = null,
                    //AddedDate = null,
                    CompanyId = CompanyId,
                    CustomerCode = null,
                    FirstName = null,
                    LastName = null,
                    OfficeName = null,
                    Email = null,
                    Lat = null,
                    Lng = null,
                    Phone1 = null,
                    Phone2 = null,
                    Website = null,
                    ZipCode = null
                };
            }
            CustomerViewModel.CustomerDetails = CustomerDetails;
           
            var y = _commonService.GetCompanySelectList(CompanyId);
            CustomerViewModel.CompanyList = y.CompanyList.Select(t => new SelectListItems
            {
                id = t.Id,
                text = t.Text
            }).ToList();
            return View("CustomerDetails", CustomerViewModel);
        }

        [HttpPost]
        public JsonResult SaveDetails(CustomerListViewModel customer)
        {
            CustomerServiceModel CustomerModel;
            CustomerModel = new CustomerServiceModel()
            {
                Id = customer.Id,
                Address1 = customer.Address1,
                Address2 = customer.Address2,
                AddedDate = customer.AddedDate,
                CompanyId = customer.CompanyId,
                CustomerCode = customer.CustomerCode,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                OfficeName = customer.OfficeName,
                Email = customer.Email,
                Lat = customer.Lat,
                Lng = customer.Lng,
                Phone1 = customer.Phone1,
                Phone2 = customer.Phone2,
                Website = customer.Website,
                ZipCode = customer.ZipCode
            };
            _ICustomerService.SaveCustomerDetails(CustomerModel);
            bool success = true;
            return Json(success, JsonRequestBehavior.AllowGet);
        }

        public FileResult DownloadExcel()
        {
            string path = "/Doc/Customer.xlsx";
            return File(path, "application/vnd.ms-excel", "Customer.xlsx");
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            int CompanyId = (int)(Session["CompanyId"]);
            if (upload != null && upload.ContentLength > 0)
            {
               
                Stream stream = upload.InputStream;
                IExcelDataReader reader = null;
                if (upload.FileName.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (upload.FileName.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                else
                {
                    ModelState.AddModelError("File", "This file format is not supported");
                    return View();
                }
               var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });
                reader.Close();

                var CustomerBulk = result.Tables[0].Rows.Cast<DataRow>().Select(r => new CustomerServiceModel
                {

                   Address1 = r["Address1"].ToString(),
                   CompanyId = CompanyId,
                   AddedDate = DateTime.Now,
                   Address2 = r["Address2"].ToString(),
                   CustomerCode=r["CustomerCode"].ToString(),
                   FirstName=r["FirstName"].ToString(),
                   LastName= r["LastName"].ToString(),
                   OfficeName=r["OfficeName"].ToString(),
                   Email =  r["Email"].ToString(),
                   Lat = r["Lat"].ToString(),
                   Lng = r["Lng"].ToString(),
                   Phone1 = r["Phone1"].ToString(),
                   Phone2 = r["Phone2"].ToString(),
                   Website = r["Website"].ToString(),
                   ZipCode = r["ZipCode"].ToString()
                }).ToList();
                _ICustomerService.SaveBulkCustomerDetails(CustomerBulk);
                
            }
            else
            {
                ModelState.AddModelError("File", "Please Upload Your file");
            }
        
            return View();
        }
    }
}