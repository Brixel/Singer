using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Flurl.Http;
using Singer.Controllers;
using Singer.DTOs.Users;
using Singer.DummyDataSeeder.Data;
using Singer.DummyDataSeeder.Data.Bases;
using Singer.DummyDataSeeder.Extensions;
using WSharp.Extensions;

namespace Singer.DummyDataSeeder
{
    internal class DataCreator
    {
        private readonly string _apiUrl;
        private readonly string _token;

        private readonly IDataContainer<CareUserDTO, CreateCareUserDTO> _careUsers = new CareUsers();
        private readonly IDataContainer<LegalGuardianUserDTO, CreateLegalGuardianUserDTO> _legalGuardians = new LegalGuardians();

        public DataCreator(string apiUrl, string token)
        {
            _apiUrl = apiUrl;
            _token = token;
        }

        public async Task CreateCareUsersAsync() 
            => await CreateAsync<CareUserController, CareUserDTO, CreateCareUserDTO>(_careUsers);

        public async Task CreateLegalGuardiansAsync()
            => await CreateAsync<CareUserController, LegalGuardianUserDTO, CreateLegalGuardianUserDTO>(_legalGuardians);

        private async Task CreateAsync<TController, TDto, TCreateDto>(IDataContainer<TDto, TCreateDto> storer)
        {
            var url = $"{_apiUrl}/{typeof(TController).GetRoute()}";

            foreach (var createDto in storer.Data)
            {
                var response = await url
                    .WithOAuthBearerToken(_token)
                    .PostJsonAsync(createDto.CreateDto);

                using var stream = await response.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(stream);

                createDto.Dto = await reader.DeserializeJsonAsync<TDto>();
            }
        }
    }
}