using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite;
using School.Application.Mapping;
using School.Infrastructure.Data;
using System;


namespace School.Tests
{
    public  class BasePruebas
    {
        protected IServiceProvider CreateContext(string nameDB)
        {
            var services = new ServiceCollection();
            services.AddDbContext<SchoolDbContext>(options => options.UseInMemoryDatabase(databaseName: nameDB),
                ServiceLifetime.Scoped,
                ServiceLifetime.Scoped);

            return services.BuildServiceProvider();
        }

        protected IMapper ConfigurarAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            return config.CreateMapper();
        }

    }
}
