using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Octapull.Application.Abstractions;
using Octapull.Application.Dtos.Meeting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Octapull.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        public readonly IMeetingService _meetingService;
        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMeetingAsync([FromForm] CreateMeetingDto createMeetingDto)
        {

            var meeting = await _meetingService.CreateMeetingAsync(createMeetingDto, User.Identity.Name);
            //var meeting = _meetingService.CreateMeeting(createMeetingDto, User.Identity.Claims[0]);

            return Ok(meeting);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeetingAsync(Guid id, [FromForm] UpdateMeetingDto updateMeetingDto, CancellationToken cancellationToken)
        {
            var result = await _meetingService.UpdateMeetingAsync(updateMeetingDto, id, User.Identity.Name, cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeetingAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _meetingService.DeleteMeetingAsync(id, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMeetings(CancellationToken cancellationToken)
        {
            var meetings = await _meetingService.GetAllMeetingsAsync(cancellationToken);

            return Ok(meetings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeetingByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var meeting = await _meetingService.GetMeetingByIdAsync(id, cancellationToken);

            return Ok(meeting);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetMeetingsByUserIdAsync(Guid id, CancellationToken cancellationToken)
        //{
        //    var meetings = await _meetingService.GetMeetingsByUserIdAsync(id, cancellationToken);

        //    return Ok(meetings);
        //}


    }
}
