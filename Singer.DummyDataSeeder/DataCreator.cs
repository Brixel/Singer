using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Singer.Controllers;
using Singer.DTOs;
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
        private const string ClientId = "dataGenerator.client";
        private const string ClientSecret = "A12F8B27-EFD7-47CD-9AE0-97D9CC7C451C";


        private CareUsers _careUsers;
        private LegalGuardians _legalGuardians;
        private Admins _admins = new Admins();

        private Events _events;
        private List<EventLocationDTO> _eventLocations;

        private string _token;

        public DataCreator(string apiUrl)
        {
            _apiUrl = apiUrl;
        }

        public async Task CreateAdminsAsync()
        {
            if (_admins == default)
                _admins = new Admins();

            await CreateAsync<AdminController, AdminUserDTO, CreateAdminUserDTO>(_admins);
        }

        public async Task CreateLegalGuardiansAsync()
        {
            if (_legalGuardians == default)
                _legalGuardians = new LegalGuardians();

            await CreateAsync<CareUserController, LegalGuardianUserDTO, CreateLegalGuardianUserDTO>(_legalGuardians);
        }

        public async Task CreateCareUsersAsync()
        {
            if (_careUsers == default)
                _careUsers= new CareUsers(_legalGuardians);

            await CreateAsync<CareUserController, CareUserDTO, CreateCareUserDTO>(_careUsers);
        }

        public async Task CreateEventsAsync()
        {
            if (_eventLocations == default)
                _eventLocations = await $"{_apiUrl}/{typeof(EventLocationController).GetRoute()}".GetJsonAsync<List<EventLocationDTO>>();
            if (_events == default)
                _events = new Events(_eventLocations);

            await CreateAsync<EventController, EventDTO, CreateEventDTO>(_events);
        }

        public async Task LinkCareUsersAndLegalGuardiansAsync()
        {
            await _careUsers.Data
                .Cast<CareUser>()
                .Select(x => new
                {
                    CareUser = x.Dto,
                    LegalGuardians = x.LegalGuardians.Select(l => l.Dto),
                })
                .ForEachAsync(x => LinkCareUserAndLegalGuardianAsync(x.CareUser, x.LegalGuardians));
        }

        private async Task LinkCareUserAndLegalGuardianAsync(CareUserDTO careUser, IEnumerable<LegalGuardianUserDTO> legalGuardians)
        {
            _ = await $"{_apiUrl}/{typeof(CareUserController).GetRoute()}/{careUser.Id}"
                .PutJsonAsync(new UpdateCareUserDTO
                {
                    AgeGroup = careUser.AgeGroup,
                    BirthDay = careUser.BirthDay,
                    CaseNumber = careUser.CaseNumber,
                    Email = careUser.Email,
                    FirstName = careUser.FirstName,
                    HasTrajectory = careUser.HasTrajectory,
                    IsExtern = careUser.IsExtern,
                    LastName = careUser.LastName,
                    LegalGuardianUsersToAdd = legalGuardians.Select(x => x.Id).ToList(),
                    NormalDaycareLocationId = careUser.NormalDaycareLocation.Id,
                    VacationDaycareLocationId = careUser.VacationDaycareLocation.Id,
                });
        }

        private async Task CreateAsync<TController, TDto, TCreateDto>(IDataContainer<TDto, TCreateDto> storer)
        {
            var url = $"{_apiUrl}/{typeof(TController).GetRoute()}";

            foreach (var dataItem in storer.Data)
                dataItem.Dto = await PostAsync<TDto, TCreateDto>(url, dataItem.CreateDto);
        }

        private async Task<TDto> PostAsync<TDto, TCreateDto>(string url, TCreateDto payload)
        {
            EnsureToken();

            var response = await url
                    .WithOAuthBearerToken(_token)
                    .PostJsonAsync(payload);

            using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);

            return await reader.DeserializeJsonAsync<TDto>();
        }

        private void EnsureToken(bool resetToken = false)
        {
            if (_token != default && !resetToken)
                return;

            _token = GetToken();
        }

        [SecurityCritical]
        public string GetToken()
        {
            var url = $"{_apiUrl}/connect/token";

            Console.Write("Username: ");
            var userName = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            string token = default;
            while (token != default)
            {
                var cts = new CancellationTokenSource();

                try
                {
                    var loginTask = url.PostAsync(new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        { "username", userName },
                        { "password", password },
                        { "grant_type", "password" },
                        { "client_id", ClientId },
                        { "client_secret", ClientSecret },
                    }));

                    Console.Write("Waiting for authentication response from server");
                    _ = WriteDotsAsync(cts.Token);

                    loginTask.Wait();
                    cts.Cancel();

                    // TODO debug why this throws error
                    var response = loginTask.Result;
                }
                catch (Exception e)
                {
                    cts.Cancel();
                    Console.WriteLine();
                    Console.WriteLine($"Error: {e.SerializeJson()}");
                }
            }

            return token;
        }

        private async Task WriteDotsAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(1000);
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.Write(".");
                await Task.Delay(1000);
            }
        }
    }
}
