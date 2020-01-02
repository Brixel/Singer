using Singer.DTOs;
using Singer.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Singer.Services.Interfaces
{
   public interface IDateValidator
   {
      void Validate(EventDTO dto);
      void Validate(CreateEventDTO dto);
      void Validate(UpdateEventDTO dto);

      void Validate(CreateCareUserDTO dto);
      void Validate(UpdateCareUserDTO dto);
   }
}
