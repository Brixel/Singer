using System.Threading.Tasks;
using Singer.DTOs;
using Singer.Models;

namespace Singer.Services.Interfaces
{
   public interface IEventLocationService : IDatabaseService<SingerLocation, SingerLocationDTO, CreateSingerLocationDTO, UpdateSingerLocationDTO>
   {
      Task<SearchResults<SingerLocationDTO>> AdvancedSearch(SingerLocationSearchDTO dto);
   }
}
