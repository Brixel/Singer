using AutoMapper;
using Singer.DTOs.Users;
using Singer.Models.Users;

namespace Singer.Profiles
{
   public class UserProfile : Profile
   {
      public UserProfile()
      {
         CreateMap<User, CareUserDTO>();
         CreateMap<CareUser, CareUserDTO>().ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id));
         CreateMap<CareUserDTO, CareUser>().ForMember(
            x => x.Id, opt => opt.MapFrom(src => src.Id.ToString())
         );
         CreateMap<CreateCareUserDTO, CareUser>();
         CreateMap<LegalGuardianUserDTO, LegalGuardianUser>();
         CreateMap<CreateLegalGuardianUserDTO, LegalGuardianUser>();
         CreateMap<LegalGuardianUser, LegalGuardianUserDTO>();
      }
   }
}
