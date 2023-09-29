using BodyTemp.Repositories.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyTemp.Repositories
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseSqlServer("name=ConnectionStrings:DefaultConnection");
                });

            return services;
        }
    }
}
