﻿using QconzLocateService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.QconzLocateInterface
{
    interface IUserService
    {
        List<UserServiceModel> GetAllUsers();
        UserServiceModel GetUserDetails(int Id);
    }
}
