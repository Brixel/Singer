using Singer.DTOs;
using Singer.DTOs.Users;

namespace Singer.Services.Interfaces;

public interface IDateValidator
{
    void Validate(EventDTO dto);
    void Validate(CreateEventDTO dto);
    void Validate(UpdateEventDTO dto);

    void Validate(CreateCareUserDTO dto);
    void Validate(UpdateCareUserDTO dto);
}
