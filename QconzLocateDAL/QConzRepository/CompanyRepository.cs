using QconzLocateDAL.QConzRepositoryInterface;
using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepository
{
    public class CompanyRepository: ICompanyRepository
    {
        QCONZEntities entity = new QCONZEntities();

        public List<CompanyModel> GetAllCompany()
        {   try
            {
                List<CompanyModel> CompanyList = new List<CompanyModel>();
                var y = (from t in entity.tblOrganizations select t).ToList();
                CompanyList = y.Select(c => new CompanyModel
                {
                    Id = c.ID,
                    Address1 = c.ADDRESS1,
                    Address2 = c.ADDRESS2,
                    ContactName = c.CONTACTNAME,
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
                var y = (from t in entity.tblOrganizations where t.ID == Id select new CompanyModel
                { Id=t.ID,
                  Address1=t.ADDRESS1,
                  Address2=t.ADDRESS2,
                  ContactName=t.CONTACTNAME,
                  Email=t.EMAIL,
                  Phone1=t.PHONE_1,
                  Phone2=t.PHONE_2,
                  Title=t.TITLE,
                  Website=t.WEBSITE,
                  Lat=t.LAT,
                  Lng=t.LNG,
                  ZipCode=t.ZIPCODE
                }).FirstOrDefault();
                return y;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
