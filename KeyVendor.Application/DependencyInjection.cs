using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RentalCar.Application.Common.Behaviours;

namespace KeyVendor.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(UnhandledExceptionBehaviour<,>));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(ValidationBehaviour<,>));
        return serviceCollection;
    }
}