using Microsoft.AspNetCore.Mvc;
using MOMShop.Dto.Collection;
using MOMShop.Services.Interfaces;
using System.Collections.Generic;
using System;
using MOMShop.Dto.Dashboard;
using MOMShop.Services.Implements;

namespace MOMShop.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private IDashboardService _services;

        public DashboardController(IDashboardService service)
        {
            _services = service;
        }

        [HttpGet]
        public DashboardDto GetAll()
        {
            try
            {
                var result = _services.Info();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("get-by-time")]
        public DashboardSecondDto GetByTime(int? month, int? year)
        {
            try
            {
                var result = _services.GetByTime(month, year);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
