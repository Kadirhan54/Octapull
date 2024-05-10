using AutoMapper;
using Octapull.Application.Dtos.Account.Request;
using Octapull.Application.Dtos.Account.User;
using Octapull.Application.Dtos.Meeting;
using Octapull.Domain.Entities;
using Octapull.Domain.Identity;

namespace Octapull.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequestDto, CreateUserRequestDto>();
            //CreateMap<ApplicationUser, CreateUserRequestDto>();
            CreateMap<CreateMeetingDto, Meeting>()
                .ForMember(dest => dest.MeetingDocuments, opt => opt.Ignore());
        }
    }
}
