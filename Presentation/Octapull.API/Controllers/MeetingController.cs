using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Octapull.Application.Abstractions;
using Octapull.Application.Dtos;
using Octapull.Application.Dtos.Meeting;
using Octapull.Domain.Entities;
using Octapull.Persistence.Contexts.Application;
using System.Text.Json;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Octapull.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        public readonly IMeetingService _meetingService;
        public MeetingController(ApplicationDbContext applicationDbContext, IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [HttpPost]
        public IActionResult CreateMeeting(CreateMeetingDto createMeetingDto)
        {
            var meeting = _meetingService.CreateMeeting(createMeetingDto);

            return Ok(meeting);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMeetingAsync(UpdateMeetingDto updateMeetingDto, CancellationToken cancellationToken)
        {
            await _meetingService.UpdateMeetingAsync(updateMeetingDto, cancellationToken);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeetingAsync(Guid id, CancellationToken cancellationToken)
        {
            await _meetingService.DeleteMeetingAsync(id, cancellationToken);

            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMeetings(CancellationToken cancellationToken)
        {
            var meetings = await _meetingService.GetAllMeetingsAsync(cancellationToken);

            // ??
            var json = JsonSerializer.Serialize(meetings);

            return Ok(json);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeetingByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var meeting = await _meetingService.GetMeetingByIdAsync(id, cancellationToken);

            return Ok(meeting);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeetingsByUserIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var meetings = await _meetingService.GetMeetingsByUserIdAsync(id, cancellationToken);

            return Ok(meetings);
        }

            
    }
}
