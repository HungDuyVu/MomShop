using MOMShop.Dto.Dashboard;
using System;

namespace MOMShop.Services.Interfaces
{
    public interface IDashboardService
    {
        DashboardDto Info();
        DashboardSecondDto GetByTime(int? month, int? year);
    }
}
