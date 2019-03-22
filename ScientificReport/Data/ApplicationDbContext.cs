using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScientificReport.Models;

namespace ScientificReport.Data
{
	public class ApplicationDbContext : IdentityDbContext<UserProfile>
	{
		public ApplicationDbContext(DbContextOptions options)
			: base(options)
		{
		}

		public DbSet<Report> Reports { get; set; }
	}
}
