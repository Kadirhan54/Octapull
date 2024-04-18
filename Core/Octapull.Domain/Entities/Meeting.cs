namespace Octapull.Domain.Entities
{
    public class Meeting : EntityBase<Guid>
    {
        public string Name { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Description { get; set; }
        
        // TODO : Use IFormFile 
        public string Document { get; set; }

    }
}
