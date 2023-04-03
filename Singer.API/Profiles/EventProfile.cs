using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Singer.DTOs;
using Singer.Models;

namespace Singer.Profiles;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<SingerLocation, SingerLocationDTO>();
        CreateMap<SingerLocation, DaycareLocationDTO>();
        CreateMap<CreateSingerLocationDTO, SingerLocation>();
        CreateMap<UpdateSingerLocationDTO, SingerLocation>();

        CreateMap<EventSlot, EventSlotDTO>()
           .ForMember(x => x.CurrentRegistrants, opt => opt.MapFrom(src =>
              src.Registrations.Count(x => x.Status == RegistrationStatus.Accepted)));

        CreateMap<Event, EventDTO>()
           .ForMember(x => x.StartDateTime, opt => opt.MapFrom(src => src.EventSlots.Min(x => x.StartDateTime)))
           .ForMember(x => x.EndDateTime, opt => opt.MapFrom(src => src.EventSlots.Max(x => x.EndDateTime)))
           .ForMember(x => x.AllowedAgeGroups, opt => opt.MapFrom(src =>
              ToAgeGroupList(src.AllowedAgeGroups)))
           .ForMember(x => x.EventSlots, options => options.MapFrom(src => src.EventSlots));

        CreateMap<CreateEventDTO, Event>()
           .ForMember(x => x.AllowedAgeGroups, opt => opt.MapFrom(src =>
               src.AllowedAgeGroups.Sum(x => Convert.ToInt32(x))));
        CreateMap<UpdateEventDTO, Event>()
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
