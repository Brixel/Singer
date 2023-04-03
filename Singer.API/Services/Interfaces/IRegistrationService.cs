using System;
using System.Threading.Tasks;

using Singer.DTOs;
using Singer.Models;

namespace Singer.Services.Interfaces;

public interface IRegistrationService : IDatabaseService<Registration, RegistrationDTO, CreateRegistrationDTO, UpdateRegistrationDTO>
{
    Task<SearchResults<RegistrationOverviewDTO>> AdvancedSearch(RegistrationSearchDTO dto);
    Task<RegistrationStatus> AcceptRegistration(Guid registrationId, Guid executedByUserId);
    Task<RegistrationStatus> RejectRegistration(Guid registrationId, Guid executedByUserId);
}
