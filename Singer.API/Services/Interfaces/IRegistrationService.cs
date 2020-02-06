using System.Threading.Tasks;
using Singer.DTOs;
using Singer.Models;

namespace Singer.Services.Interfaces
{
   public interface IRegistrationService : IDatabaseService<Registration, RegistrationDTO, CreateRegistrationDTO, UpdateRegistrationDTO>
   {
      Task<SearchResults<RegistrationDTO>> AdvancedSearch(RegistrationSearchDTO dto);
   }
}
