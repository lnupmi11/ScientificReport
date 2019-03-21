using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScientificReport.Models;

namespace ScientificReport.Data
{
	public class ApplicationDbContext : IdentityDbContext<UserProfile, IdentityRole<int>, int>
	{
		public ApplicationDbContext(DbContextOptions options)
			: base(options)
		{
		}

		public DbSet<Report> Reports { get; set; }
		public DbSet<UserProfile> UserProfile { get; set; }
	}
}
