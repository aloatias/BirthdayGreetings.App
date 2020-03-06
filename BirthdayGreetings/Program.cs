using BirthdayGreetings.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace BirthdayGreetings
{
    class Program
    {
        private static IConfiguration _configuration;

        static void Main(string[] args)
        {
            BuildConfiguration();
            InjectDependencies();
        }

        private static void BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", false, true);

            _configuration = builder.Build();
        }

        private static void InjectDependencies()
        {
            new ServiceCollection()
                .AddDbContext<BirthdayGreetingContext>(options => options.UseSqlServer(_configuration.GetConnectionString("BirthDayGreetingsConnection")));
        }
    }
}
