using AutoMapper;
using Singer.DTOs;
using Singer.Models;

namespace Singer.Profiles
{
   public class EventRegistrationProfile : Profile
   {
      public EventRegistrationProfile()
      {
         CreateMap<CreateRegistrationDTO, Registration>();
         CreateMap<Registration, RegistrationDTO>()
            .ForMember(x => x.EventDescription, opts => opts.MapFrom(x => x.EventSlot.Event));
      }
   }
}
