using KSTDotNetCore.NLayer.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace KSTDotNetCore.NLayer.DataAccess.Db;

internal class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Connectionstrings.sqlConnectionStringBuilder.ConnectionString);
    }
    public DbSet<BlogModel> Blogs { get; set; }

}
