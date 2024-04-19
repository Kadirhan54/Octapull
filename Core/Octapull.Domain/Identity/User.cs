using Microsoft.AspNetCore.Identity;
using Octapull.Domain.Common;
using Octapull.Domain.Entities;
using Octapull.Domain.Enums;

namespace Octapull.Domain.Identity
{
    public class User : IdentityUser<Guid>, IEntityBase<Guid>, ICreatedByEntity, IModifiedByEntity
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
        public Gender Gender { get; set; }

        public UserSetting UserSetting { get; set; }
        public ICollection<Meeting> Meetings { get; set; }

        public Guid? CreatedByUserId { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public string? ModifiedByUserId { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}
