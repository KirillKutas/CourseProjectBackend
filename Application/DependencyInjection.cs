using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Application.Common.Behaviors;
//using Application.Common.Sieve;
//using Application.Common.Sieve.CustomFilters;
//using Application.Common.Sieve.CustomSortings;
//using Sieve.Models;
//using Sieve.Services;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            //ConfigureSieve(services);

            return services;
        }

        //private static void ConfigureSieve(IServiceCollection services)
        //{
        //    services.AddScoped<ISieveProcessor, RcoSieveProcessor>();
        //    services.AddScoped<ISieveCustomFilterMethods, RcoFilterMethods>();
        //    services.AddScoped<ISieveCustomSortMethods, RcoSortMethods>();

        //    services.Configure<SieveOptions>(x =>
        //    {
        //        //x.CaseSensitive = false; // should property names be case-sensitive? Defaults to false
        //        x.ThrowExceptions = true; // should Sieve throw exceptions instead of silently failing? Defaults to false
        //        //x.DefaultPageSize = 0; // optional number to fallback to when no page argument is given. Set <=0 to disable paging if no pageSize is specified (default).
        //        //x.MaxPageSize = 0; // maximum allowed page size. Set <=0 to make infinite (default)
        //    });
        //}
    }
}
