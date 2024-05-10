using Octapull.Application.Dtos.Meeting;
using Octapull.Domain.Entities;
using System.Security.Claims;

namespace Octapull.Application.Abstractions
{
    public interface IMeetingService
    {
        Task<bool> CreateMeetingAsync(CreateMeetingDto meeting, string createdByUserName, CancellationToken cancellationToken = default);
        Task<bool> UpdateMeetingAsync(UpdateMeetingDto meeting, Guid id, string createdByUserName, CancellationToken cancellationToken = default);
        Task<bool> DeleteMeetingAsync(Guid meetingId, CancellationToken cancellationToken = default);
        Task<Meeting> GetMeetingByIdAsync(Guid meetingId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Meeting>> GetMeetingsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Meeting>> GetAllMeetingsAsync(CancellationToken cancellationToken = default);
    }
}
