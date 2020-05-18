using GL_sensors_v0_4.Models;
using Microsoft.EntityFrameworkCore;
//using Pomelo.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GL_sensors_v0_4.Data
{
    public class SnakeyDataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            optionsBuilder.UseMySql(configuration["ConnectionStrings:DefaultConnection"]);
            //optionsBuilder.UseMySql(configuration["ConnectionStrings:DefaultConnection"]);
            //optionsBuilder.
        }

        //public SnakeyDataContext(DbContextOptions<SnakeyDataContext> options)
        //: base(options)
        //{
        //}
        //public SnakeyDataContext() { }
        public DbSet<Measurement> Measurements { get; set; }
    }
}
