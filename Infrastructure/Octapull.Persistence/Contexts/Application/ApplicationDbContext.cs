using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Octapull.Domain.Common;
using Octapull.Domain.Entities;
using Octapull.Domain.Identity;
using System.Reflection;

namespace Octapull.Persistence.Contexts.Application
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, Guid>
    {

        public DbSet<Meeting> Meetings{ get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //modelBuilder.Entity<MeetingDocument>().HasNoKey();  

            // ???
            //modelBuilder.Ignore<Meeting>();
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((ICreatedByEntity)entry.Entity).CreatedOn = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    ((IModifiedByEntity)entry.Entity).ModifiedOn = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    ((IDeletedByEntity)entry.Entity).DeletedOn = DateTime.UtcNow;
                }
            }

            //foreach (var data in datas)
            //{
            //    switch (data.State)
            //    {
            //        case EntityState.Added:
            //            data.Entity.CreatedDate = DateTime.UtcNow;
            //            data.Entity.Status = Status.Inserted;
            //            break;
            //        case EntityState.Modified:
            //            data.Entity.LastModifiedDate = DateTime.UtcNow;
            //            data.Entity.Status = Status.Updated;
            //            break;
            //        case EntityState.Deleted:
            //            data.Entity.CreatedDate = DateTime.UtcNow;
            //            data.Entity.Status = Status.Deleted;
            //            break;
            //        default:
            //            break;
            //    }
            //}

            return base.SaveChanges();
        }

    }
}
