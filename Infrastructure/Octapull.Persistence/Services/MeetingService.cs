using Microsoft.EntityFrameworkCore;
using Octapull.Application.Abstractions;
using Octapull.Application.Dtos.Meeting;
using Octapull.Domain.Entities;
using Octapull.Persistence.Contexts.Application;

namespace Octapull.Persistence.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public MeetingService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task<bool> CreateMeeting(CreateMeetingDto createMeetingDto)
        {
            Meeting meeting = new()
            {
                Id = Guid.NewGuid(),
                Name = createMeetingDto.Name,
                StartDate = createMeetingDto.StartDate,
                EndDate = createMeetingDto.EndDate,
                Description = createMeetingDto.Description,
                Document = createMeetingDto.Document
            };

            try
            {
                _applicationDbContext.Add(meeting);
                _applicationDbContext.SaveChanges();
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }

        }

        public async Task<bool> UpdateMeetingAsync(UpdateMeetingDto updateMeetingDto,CancellationToken cancellationToken)
        {
            var meeting = await _applicationDbContext.Meetings.Where(x => x.Id == updateMeetingDto.MeetingId).SingleOrDefaultAsync(cancellationToken);

            if (meeting == null)
            {
                return false;
            }

            meeting.Name = updateMeetingDto.Name;
            meeting.StartDate = updateMeetingDto.StartDate;
            meeting.EndDate = updateMeetingDto.EndDate;
            meeting.Description = updateMeetingDto.Description;
            meeting.Document = updateMeetingDto.Document;

            try
            {
                _applicationDbContext.Update(meeting);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteMeetingAsync(Guid meetingId,CancellationToken cancellationToken)
        {
            var meeting = await _applicationDbContext.Meetings.Where(x => x.Id == meetingId).SingleOrDefaultAsync(cancellationToken);

            if (meeting == null)
            {
                return false;
            }

            try
            {
                _applicationDbContext.Remove(meeting);
                _applicationDbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Meeting>> GetAllMeetingsAsync(CancellationToken cancellationToken)
        {
            var meetings = await _applicationDbContext.Meetings.ToListAsync(cancellationToken);

            //if (meetings == null)
            //{
            //    return false;
            //}

            return meetings;    
        }

        public async Task<Meeting> GetMeetingByIdAsync(Guid meetingId, CancellationToken cancellationToken)
        {
            var meeting = await _applicationDbContext.Meetings.Where(x => x.Id == meetingId).SingleOrDefaultAsync();

            return meeting;
        }

        public async Task<IEnumerable<Meeting>> GetMeetingsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


    }
}
