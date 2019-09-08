using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Singer.DTOs;
using Singer.Models;
using Singer.Services;

namespace Singer.Profiles
{
   public class EventProfile : Profile
   {
      public EventProfile()
      {
         CreateMap<EventLocation, EventLocationDTO>();
         CreateMap<CreateEventLocationDTO, EventLocation>();

         CreateMap<Event, EventDTO>()
            .ForMember(x => x.AllowedAgeGroups, opt => opt.MapFrom(src =>
               ToAgeGroupList(src.AllowedAgeGroups)));

         CreateMap<CreateEventDTO, Event>()
            .ForMember(x => x.AllowedAgeGroups, opt => opt.MapFrom(src =>
                src.AllowedAgeGroups.Sum(x => Convert.ToInt32(x))));

         CreateMap<Event, EventDescriptionDTO>()
            .ForMember(x => x.AgeGroups, opt => opt.MapFrom(src =>
               ToAgeGroupList(src.AllowedAgeGroups)));
      }

      public static List<AgeGroup> ToAgeGroupList(AgeGroup bitmap)
      {
         var list = new List<AgeGroup>();
         foreach (AgeGroup group in Enum.GetValues(typeof(AgeGroup)))
         {
            if (bitmap.HasFlag(group))
            {
               list.Add(group);
            }
         }
         return list;
      }
   }
}
