using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Octapull.Application.Abstractions;
using Octapull.Application.Abstractions.Storage;
using Octapull.Application.Dtos.Meeting;
using Octapull.Application.Interfaces;
using Octapull.Domain.Entities;
using Octapull.Domain.Identity;
using Octapull.Persistence.Contexts.Application;

namespace Octapull.Persistence.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IIdentityService _identityService;
        private readonly IBlobService _blobService;
        private readonly IMapper _mapper;

        public MeetingService(ApplicationDbContext applicationDbContext, IIdentityService identityService, IBlobService blobService, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _identityService = identityService;
            _blobService = blobService;
            _mapper = mapper;
        }

        public async Task<bool> CreateMeetingAsync(CreateMeetingDto createMeetingDto, string createdByUserName, CancellationToken cancellationToken)
        {
            var applicationUser = await _identityService.GetUserByUserNameAsync(createdByUserName);

            //Meeting meeting = new()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = createMeetingDto.Name,
            //    StartDate = createMeetingDto.StartDate,
            //    EndDate = createMeetingDto.EndDate,
            //    Description = createMeetingDto.Description,
            //    DocumentsId = new List<string>(),
            //    CreatedByUserId = applicationUser?.Id,
            //    ApplicationUser = applicationUser,
            //    ApplicationUserId = applicationUser.Id,
            //    CreatedOn = DateTimeOffset.UtcNow,
            //};

            Meeting meeting = _mapper.Map<Meeting>(createMeetingDto);

            meeting.Id = Guid.NewGuid();
            meeting.MeetingDocuments = new List<MeetingDocument>();
            meeting.CreatedByUserId = applicationUser?.Id;
            meeting.ApplicationUser = applicationUser;
            meeting.ApplicationUserId = applicationUser.Id;

            if (createMeetingDto.Document != null)
            {
                foreach (var file in createMeetingDto.Document)
                {
                    var fileId = await _blobService.UploadAsync(file.OpenReadStream(), "documents", file.ContentType, cancellationToken);

                    var document = new Document
                    {
                        Id = fileId,
                        CreatedByUserId = applicationUser?.Id,
                    };

                    _applicationDbContext.Add(document);

                    var meetingDocument = new MeetingDocument
                    {
                        MeetingId = meeting.Id,
                        DocumentId = document.Id,
                    };

                    _applicationDbContext.Add(meetingDocument);

                    meeting.MeetingDocuments.Add(meetingDocument);
                }
            }

            try
            {
                _applicationDbContext.Add(meeting);
                _applicationDbContext.SaveChanges();

                return true;
                //return Task.FromResult(true);
            }
            catch (Exception)
            {
                return false;
                //return Task.FromResult(false);
            }
        }

        public async Task<bool> UpdateMeetingAsync(UpdateMeetingDto updateMeetingDto, Guid meetingId, string createdByUserName ,CancellationToken cancellationToken)
        {
            var meeting = await _applicationDbContext.Meetings.Where(x => x.Id == meetingId).SingleOrDefaultAsync(cancellationToken);

            if (meeting == null)
            {
                return false;
            }

            var applicationUser = await _identityService.GetUserByUserNameAsync(createdByUserName);

            if (meeting == null)
            {
                return false;
            }

            meeting.Name = updateMeetingDto.Name;
            meeting.StartDate = updateMeetingDto.StartDate;
            meeting.EndDate = updateMeetingDto.EndDate;
            meeting.Description = updateMeetingDto.Description;
            meeting.MeetingDocuments = new List<MeetingDocument>();

            foreach (var file in updateMeetingDto.Documents)
            {
                var fileId = await _blobService.UploadAsync(file.OpenReadStream(), "documents", file.ContentType, cancellationToken);

                var document = new Document
                {
                    Id = fileId,
                    CreatedByUserId = applicationUser?.Id,
                };

                _applicationDbContext.Add(document);

                var meetingDocument = new MeetingDocument
                {
                    MeetingId = meeting.Id,
                    DocumentId = document.Id,
                };

                _applicationDbContext.Add(meetingDocument);

                meeting.MeetingDocuments.Add(meetingDocument);
            }

            try
            {
                _applicationDbContext.Update(meeting);
                _applicationDbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteMeetingAsync(Guid meetingId, CancellationToken cancellationToken)
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
