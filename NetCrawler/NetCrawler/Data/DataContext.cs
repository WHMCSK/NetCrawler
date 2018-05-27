using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NetCrawler.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.EntityFrameworkCore.Extensions;
using NetCrawler.Common;

namespace NetCrawler.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Movie> Movies { set; get; }

        public DbSet<CrawlerConfigurations> CrawlerConfigurations { get; set; }

        public DbSet<GirlsPics> GirlsPics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(@"Server=localhost;database=girls;uid=root;pwd=Vosung123;SslMode=None;");
        }
    }
}
