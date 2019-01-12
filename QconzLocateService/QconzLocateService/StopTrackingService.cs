﻿using QconzLocateDAL.QConzRepository;
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
    public class StopTrackingService : IStopTrackingService
    {
        private IStopTrackingRepository _IStopTrackingRepository = new StopTrackingRepository();

        public List<StopTrackingServiceModel> GetStopTrackingList(int companyId)
        {

            var y = _IStopTrackingRepository.StopTracking(companyId);
            List<StopTrackingServiceModel> StopTracking = y.Select(t => new StopTrackingServiceModel
            {
                UserId = t.UserId,
                DateTime = t.DateTime,
                Hours = t.Hours,
                Status = t.Status,
                User = t.User
            }).ToList();
            return StopTracking;

        }

        public string SaveStopTrackingDetails(StopTrackingServiceModel StopTrackingDetails)
        {

            var stoptracking = new StopTrackingModel()
            {
                Id = StopTrackingDetails.Id,                
                UserId = StopTrackingDetails.UserId,
                Hours = StopTrackingDetails.Hours,
                Status = StopTrackingDetails.Status,
                DateTime = StopTrackingDetails.DateTime,
                User= StopTrackingDetails.User
            };
            //IStopTrackingService.SaveStopTrackingDetails(stoptracking);
            return _IStopTrackingRepository.SaveStopTrackingDetails(stoptracking);
        }
    }
}
