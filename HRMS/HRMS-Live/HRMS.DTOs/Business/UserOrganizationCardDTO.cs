﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class UserOrganizationCardDTO
    {
        public string UserDepartmentName { get; set; }
        public List<UserCardDTO> UsersCards { get; set; }
    }
}
