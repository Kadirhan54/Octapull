namespace Octapull.Domain.Entities
{
    public class MeetingDocument : EntityBase<Guid>
    {
        public Guid MeetingId { get; set; }
        public Meeting Meeting { get; set; }

        public Guid DocumentId { get; set; }
        public Document Document { get; set; }
    }
}
