using Microsoft.Extensions.DependencyInjection;
using MODEL;
using MODEL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using REPOSITORY.UnitOfWork;
using BAL.IServices.Login_Register;
using BAL.Services.Login_Register;

namespace BAL.Shared;

public class ServiceManager
{
    public static void SetServiceInfo(IServiceCollection services, AppSettings appSettings)
    {
        services.AddDbContextPool<DataContext>(options =>
        {
            options.UseSqlServer(appSettings.ConnectionString);
        });

        services.AddScoped< IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRegister, Register>();
        services.AddScoped<ILogin, Login>();
    }
}
