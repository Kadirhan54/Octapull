using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Octapull.Application.Dtos;
using Octapull.Domain.Entities;
using Octapull.Persistence.Contexts.Application;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Octapull.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public MeetingController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMeetings(CancellationToken cancellationToken)
        {
            var meetings = await _applicationDbContext.Meetings.ToListAsync(cancellationToken);

            return Ok(meetings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeetingByIdAsync(Guid id)
        {
            var meeting = await _applicationDbContext.Meetings.Where(x => x.Id == id).SingleOrDefaultAsync();

            if (meeting == null)
                return NotFound();

            return Ok(meeting);
        }

        [HttpPost]
        public IActionResult CreateMeeting(MeetingDto meetingDto)
        {

            Meeting meeting = new()
            {
                Id = Guid.NewGuid(),
                Name = meetingDto.Name,
                StartDate = meetingDto.StartDate,
                EndDate= meetingDto.EndDate,
                Description = meetingDto.Description,
                Document = meetingDto.Document
            };

            _applicationDbContext.Add(meeting);
            _applicationDbContext.SaveChanges();

            return Ok(meeting);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeetingAsync(Guid id, MeetingDto meetingDto)
        {
            var meeting = await _applicationDbContext.Meetings.Where(x=>x.Id == id).SingleOrDefaultAsync();

            if(meeting == null)
            {
                return NotFound();
            }

            meeting.Name = meetingDto.Name;
            meeting.StartDate = meetingDto.StartDate;
            meeting.EndDate = meetingDto.EndDate;
            meeting.Description = meetingDto.Description;
            meeting.Document = meetingDto.Document;

            _applicationDbContext.Update(meeting);
            _applicationDbContext.SaveChanges();

            return Ok(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeetingAsync(Guid id)
        {
            var meeting = await _applicationDbContext.Meetings.Where(x => x.Id == id).SingleOrDefaultAsync();

            if (meeting == null)
            {
                return NotFound();
            }

            _applicationDbContext.Remove(meeting);
            _applicationDbContext.SaveChanges();

            return Ok(id);
        }

    }
}
