using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Octapull.Domain.Common;
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

            //modelBuilder.Ignore<Meeting>();

            base.OnModelCreating(modelBuilder);
        }

        //public override int SaveChanges()
        //{
        //    var entries = ChangeTracker.Entries();

        //    foreach (var entry in entries)
        //    {
        //        if (entry.State == EntityState.Added)
        //        {
        //            ((ICreatedByEntity)entry.Entity).CreatedOn = DateTime.UtcNow;
        //        }
        //        else if (entry.State == EntityState.Modified)
        //        {
        //            ((IModifiedByEntity)entry.Entity).ModifiedOn = DateTime.UtcNow;
        //        }
        //        else if (entry.State == EntityState.Deleted)
        //        {
        //            ((IDeletedByEntity)entry.Entity).DeletedOn = DateTime.UtcNow;
        //        }
        //    }

        //    return base.SaveChanges();
        //}

    }
}
