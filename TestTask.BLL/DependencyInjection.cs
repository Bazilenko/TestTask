using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TestTask.BLL.Interfaces;
using TestTask.BLL.Services;

namespace TestTask.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        services.AddScoped<IRoomManagementService, RoomManagementService>();
        services.AddScoped<IServiceManagementService, ServiceManagementService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IPriceCalculatorService, PriceCalculatorService>();

        services.AddAutoMapper(config => config.AddMaps(Assembly.GetExecutingAssembly()));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}