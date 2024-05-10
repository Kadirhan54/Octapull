using Microsoft.AspNetCore.Http;

namespace Octapull.Application.Dtos.Meeting
{
    public class CreateMeetingDto
    {
        public string Name { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Description { get; set; }
        public IEnumerable<IFormFile?> Document { get; set; }
    }
}
