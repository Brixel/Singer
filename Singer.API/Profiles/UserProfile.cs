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
         CreateMap<User, CareUserDTO>().ForMember(
            x => x.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id))
         );
         CreateMap<CareUserDTO, User>().ForMember(
            x => x.Id, opt => opt.MapFrom(src => src.Id.ToString())
         );
         CreateMap<CreateCareUserDTO, User>();
      }
   }
}
