using AutoMapper;
using Singer.DTOs;
using Singer.Models;

namespace Singer.Profiles
{
   public class EventProfile : Profile
   {
      public EventProfile()
      {
         CreateMap<EventLocation, EventLocationDTO>();
         CreateMap<CreateEventLocationDTO, EventLocation>();

         CreateMap<Event, EventDTO>();
         CreateMap<CreateEventDTO, Event>();
      }
   }
}
