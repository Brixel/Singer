using System.Linq;
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
         // Generic DTO to User mapping
         CreateMap<IUpdateUserDTO, User>();
         CreateMap<ICreateUserDTO, User>();
         CreateMap<User, UserDTO>();

         // CareUsers
         CreateMap<CareUser, CareUserDTO>()
            .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(x => x.LegalGuardianUsers, opt => opt.MapFrom(src => src.LegalGuardianCareUsers.Select(y => y.LegalGuardian)));
         CreateMap<CareUser, LinkedCareUserDTO>()
            .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.User.LastName));
         CreateMap<CreateCareUserDTO, CareUser>()
            .ForMember(m => m.User, o => o.MapFrom(src => src));
         CreateMap<UpdateCareUserDTO, CareUser>()
            .ForMember(m => m.User, o => o.MapFrom(src => src));

         //LegalGuardianUsers
         CreateMap<CreateLegalGuardianUserDTO, LegalGuardianUser>()
            .ForMember(m => m.User, o => o.MapFrom(src => src));

         CreateMap<UpdateLegalGuardianUserDTO, LegalGuardianUser>()
            .ForMember(m => m.User, o => o.MapFrom(src => src));

         CreateMap<LegalGuardianUser, LegalGuardianUserDTO>()
            .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(x => x.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(x => x.CareUsers, opt => opt.MapFrom(src => src.LegalGuardianCareUsers.Select(y => y.CareUser)));
         CreateMap<LegalGuardianUser, LinkedLegalGuardianDTO>()
            .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(x => x.Email, opt => opt.MapFrom(src => src.User.Email));

         // AdminUser
         CreateMap<AdminUser, AdminUserDTO>()
            .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(x => x.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(x => x.UserId, opt => opt.MapFrom(src => src.UserId));
         CreateMap<CreateAdminUserDTO, AdminUser>()
            .ForMember(m => m.User, o => o.MapFrom(src => src));
         CreateMap<UpdateAdminUserDTO, AdminUser>()
            .ForMember(m => m.User, o => o.MapFrom(src => src));
      }
   }
}
