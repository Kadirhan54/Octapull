using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Octapull.Domain.Entities;
using Octapull.Domain.Identity;
using System.Reflection;

namespace Octapull.Persistence.Contexts.Application
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {

        public DbSet<Meeting> Meetings{ get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

    }
}
