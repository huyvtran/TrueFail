﻿using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Controls;

namespace Admin.Commands.GeoCommand.Districts
{
    public class DeleteCommand : BaseCommand<int>
    {
        public int DistrictId { set; get; }
        public DeleteCommand(int districtId)
        {
            DistrictId = districtId;
        }
    }
}
