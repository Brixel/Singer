using AutoMapper;
using Singer.DTOs;
using Singer.DTOs.Users;
using Singer.Models.Users;

namespace Singer.Profiles
{
   public class UserProfile : Profile
   {
      public UserProfile()
      {
         // CareUsers
         CreateMap<CareUser, CareUserDTO>()
            .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.User.LastName));
         CreateMap<CareUserDTO, CareUser>()
            .ForMember(x => x.User, opt => opt.MapFrom(src => new User { FirstName = src.FirstName, LastName = src.LastName }));
         CreateMap<CreateCareUserDTO, CareUser>()
            .ForMember(x => x.User, opt => opt.MapFrom(src => new User { FirstName = src.FirstName, LastName = src.LastName }));

         //LegalGuardianUsers
         CreateMap<LegalGuardianUserDTO, LegalGuardianUser>()
            .ForMember(x => x.User, opt => opt.MapFrom(src => new User { FirstName = src.FirstName, LastName = src.LastName, Email = src.Email }));
         CreateMap<CreateLegalGuardianUserDTO, LegalGuardianUser>()
            .ForMember(x => x.User, opt => opt.MapFrom(src => new User { FirstName = src.FirstName, LastName = src.LastName, Email = src.Email }));
         CreateMap<LegalGuardianUser, LegalGuardianUserDTO>()
            .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(x => x.Email, opt => opt.MapFrom(src => src.User.Email));

         // AdminUser
         CreateMap<AdminUser, AdminUserDTO>()
            .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(x => x.Email, opt => opt.MapFrom(src => src.User.Email));
         CreateMap<AdminUserDTO, AdminUser>()
            .ForMember(x => x.User, opt => opt.MapFrom(src =>
               new User { FirstName = src.FirstName, LastName = src.LastName , Email = src.Email, UserName = src.UserName}));
         CreateMap<CreateAdminUserDTO, AdminUser>()
            .ForMember(x => x.User, opt => opt.MapFrom(src =>
               new User { FirstName = src.FirstName, LastName = src.LastName, Email = src.Email, UserName = src.Email}));
      }
   }
}
