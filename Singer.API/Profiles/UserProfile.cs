using System;
using AutoMapper;
using Singer.DTOs;
using Singer.Models;

namespace Singer.Profiles
{
   public class UserProfile : Profile
   {
      public UserProfile()
      {
         CreateMap<CareUser, CareUserDTO>().ForMember(
            x => x.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id))
         );
         CreateMap<CareUserDTO, CareUser>().ForMember(
            x => x.Id, opt => opt.MapFrom(src => src.Id.ToString())
         );
         CreateMap<CreateCareUserDTO, CareUser>();
      }
   }
}
