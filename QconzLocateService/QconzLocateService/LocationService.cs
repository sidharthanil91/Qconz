﻿using QconzLocateDAL.QConzRepository;
using QconzLocateDAL.QConzRepositoryInterface;
using QconzLocateService.Models;
using QconzLocateService.QconzLocateInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.QconzLocateService
{
    public class LocationService:ILocationService
    {
        private ILocationRepository _ILocationRepository = new LocationRepository();
        public List<LocationServiceModel> GetCustomerLocation(int CompanyId,string Customer,int GroupId)
        {
            var y = _ILocationRepository.GetCustomerLocation(CompanyId, Customer, GroupId);
            List<LocationServiceModel> locations = y.Select(t => new LocationServiceModel
            {
                Address = t.Address,
                Lat = t.Lat,
                Lng = t.Lng
            }).ToList();
            return locations;
        }
    }
}
