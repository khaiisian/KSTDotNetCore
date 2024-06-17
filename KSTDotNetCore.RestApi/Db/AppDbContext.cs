using KSTDotNetCore.RestApi;
using KSTDotNetCore.RestApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTDotNetCore.ConsoleApp.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //if you use MySQL or Oracle or other change name behind Use.
        //    optionsBuilder.UseSqlServer(Connectionstrings.sqlConnectionStringBuilder.ConnectionString);
        //}


        public DbSet<BlogModel> Blogs { get; set; }
    }
}
