using Microsoft.AspNetCore.Http;
using System.Reflection.Metadata;

namespace Octapull.Application.Dtos.Meeting
{
    public class UpdateMeetingDto
    {
        public string Name { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Description { get; set; }
        public IEnumerable<IFormFile>? Documents { get; set; }
    }
}
