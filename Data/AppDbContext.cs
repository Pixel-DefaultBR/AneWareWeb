using AnewareApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AnewareApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<UserModel> Users { get; set; }
        public DbSet<ReportModel> Reports { get; set; }
    }
}
