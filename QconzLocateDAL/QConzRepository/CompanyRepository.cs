using QconzLocateDAL.QConzRepositoryInterface;
using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepository
{
    public class CompanyRepository : ICompanyRepository
    {
        QCONZEntities entity = new QCONZEntities();

        public List<CompanyModel> GetAllCompany(int CompanyId, string Archive)
        {
            try
            {
                List<CompanyModel> CompanyList = new List<CompanyModel>();
                var y = (from t in entity.tblOrganizations where (t.ID == CompanyId || CompanyId == 0) && t.ARCHIVE == Archive select t).ToList();
                CompanyList = y.Select(c => new CompanyModel
                {
                    Id = c.ID,
                    Address1 = c.ADDRESS1,
                    Address2 = c.ADDRESS2,
                    Image=c.IMAGE,
                    ContactName = c.CONTACTNAME,
                    City = c.CITY,
                    Email = c.EMAIL,
                    Lat = c.LAT,
                    Lng = c.LNG,
                    Phone1 = c.PHONE_1,
                    Phone2 = c.PHONE_2,
                    Title = c.TITLE,
                    Website = c.WEBSITE,
                    ZipCode = c.ZIPCODE
                }
                ).ToList();
                return CompanyList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public CompanyModel GetCompanyDetails(int Id)
        {
            try
            {
                var y = (from t in entity.tblOrganizations
                         where t.ID == Id
                         select new CompanyModel
                         {
                             Id = t.ID,
                             Address1 = t.ADDRESS1,
                             Address2 = t.ADDRESS2,
                             Image=t.IMAGE,
                             City = t.CITY,
                             ContactName = t.CONTACTNAME,
                             Email = t.EMAIL,
                             Phone1 = t.PHONE_1,
                             Phone2 = t.PHONE_2,
                             Title = t.TITLE,
                             Website = t.WEBSITE,
                             Lat = t.LAT,
                             Lng = t.LNG,
                             ZipCode = t.ZIPCODE,
                             Archive = t.ARCHIVE
                         }).FirstOrDefault();
                return y;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void SaveCompanyDetails(CompanyModel CompanyModel)
        {
            try
            {
                if (CompanyModel.Id == 0)
                {
                    var company = new tblOrganization()
                    {
                        ADDRESS1 = CompanyModel.Address1,
                        ADDRESS2 = CompanyModel.Address2,
                        CONTACTNAME = CompanyModel.ContactName,
                        IMAGE=CompanyModel.Image,
                        EMAIL = CompanyModel.Email,
                        CITY = CompanyModel.City,
                        LAT = CompanyModel.Lat,
                        LNG = CompanyModel.Lng,
                        PHONE_1 = CompanyModel.Phone1,
                        PHONE_2 = CompanyModel.Phone2,
                        TITLE = CompanyModel.Title,
                        WEBSITE = CompanyModel.Website,
                        ZIPCODE = CompanyModel.ZipCode,
                        ARCHIVE = CompanyModel.Archive
                    };
                    entity.tblOrganizations.Add(company);
                }
                else
                {
                    var y = entity.tblOrganizations.FirstOrDefault(t => t.ID == CompanyModel.Id);
                    y.ADDRESS1 = CompanyModel.Address1;
                    y.ADDRESS2 = CompanyModel.Address2;
                    y.CITY = CompanyModel.City;
                    y.CONTACTNAME = CompanyModel.ContactName;
                    y.EMAIL = CompanyModel.Email;
                    y.IMAGE = CompanyModel.Image;
                    y.LAT = CompanyModel.Lat;
                    y.LNG = CompanyModel.Lng;
                    y.PHONE_1 = CompanyModel.Phone1;
                    y.PHONE_2 = CompanyModel.Phone2;
                    y.TITLE = CompanyModel.Title;
                    y.WEBSITE = CompanyModel.Website;
                    y.ZIPCODE = CompanyModel.ZipCode;
                    y.ARCHIVE = CompanyModel.Archive;
                }
                entity.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
