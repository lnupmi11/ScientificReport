using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScientificReport.Models;

namespace ScientificReport.Data
{
  public class ApplicationDbContext : IdentityDbContext
  {
    public ApplicationDbContext(DbContextOptions options)
      : base(options)
    {
    }

    public DbSet<Report> Reports { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<ScientificWork> ScientificWork { get; set; }
    public DbSet<UserProfile> UserProfile { get; set; }
  }
}