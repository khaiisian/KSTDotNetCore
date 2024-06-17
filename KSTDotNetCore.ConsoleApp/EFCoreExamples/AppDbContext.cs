using KSTDotNetCore.ConsoleApp.Dtos;
using KSTDotNetCore.ConsoleApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTDotNetCore.ConsoleApp.EFCoreExamples
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(Connectionstrings.sqlConnectionStringBuilder.ConnectionString);
        //}

        public DbSet<BlogDto> Blogs { get; set; }
    }
}
