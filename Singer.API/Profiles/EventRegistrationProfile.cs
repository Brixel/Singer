using AutoMapper;

using Singer.DTOs;
using Singer.Models;

namespace Singer.Profiles;

public class EventRegistrationProfile : Profile
{
    public EventRegistrationProfile()
    {
        CreateMap<CreateRegistrationDTO, Registration>();
        CreateMap<Registration, RegistrationDTO>()
           .ForMember(x => x.EventDescription, opts => opts.MapFrom(x => x.EventSlot.Event));

        CreateMap<Registration, RegistrationOverviewDTO>()
           .ForMember(x => x.CareUserFirstName, opts => opts.MapFrom(x => x.CareUser.User.FirstName))
           .ForMember(x => x.CareUserLastName, opts => opts.MapFrom(x => x.CareUser.User.LastName))
           .ForMember(x => x.EventTitle, opts => opts.MapFrom(x => x.EventSlot.Event.Title))
           .ForMember(x => x.RegistrationStatus, opts => opts.MapFrom(x => x.Status))
           .ForMember(x => x.RegistrationType, opts => opts.MapFrom(x => x.EventRegistrationType));
    }
}
