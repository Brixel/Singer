using AutoMapper;
using Singer.DTOs;
using Singer.Models;

namespace Singer.Profiles
{
   public class EventRegistrationProfile : Profile
   {
      public EventRegistrationProfile()
      {
         CreateMap<CreateEventRegistrationDTO, EventRegistration>();
      }
   }
}
