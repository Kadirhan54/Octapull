using Octapull.Domain.Identity;

namespace Octapull.Domain.Entities
{
    public class Meeting : EntityBase<Guid>
    {
        public string Name { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Description { get; set; }
        
        public ICollection<MeetingDocument> MeetingDocuments { get; set; }

        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
