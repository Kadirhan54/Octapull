using Octapull.Application.Dtos.Meeting;
using Octapull.Domain.Entities;

namespace Octapull.Application.Abstractions
{
    public interface IMeetingService
    {
        Task<bool> CreateMeeting(CreateMeetingDto meeting);
        Task<bool> UpdateMeetingAsync(UpdateMeetingDto meeting, CancellationToken cancellationToken);
        Task<bool> DeleteMeetingAsync(Guid meetingId, CancellationToken cancellationToken);
        Task<Meeting> GetMeetingByIdAsync(Guid meetingId, CancellationToken cancellationToken);
        Task<IEnumerable<Meeting>> GetMeetingsByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<IEnumerable<Meeting>> GetAllMeetingsAsync(CancellationToken cancellationToken);
    }
}
