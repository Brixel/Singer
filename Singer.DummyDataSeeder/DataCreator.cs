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


        private readonly IDataContainer<CareUserDTO, CreateCareUserDTO> _careUsers = new CareUsers();
        private readonly IDataContainer<LegalGuardianUserDTO, CreateLegalGuardianUserDTO> _legalGuardians = new LegalGuardians();

        private string _token;

        public DataCreator(string apiUrl)
        {
            _apiUrl = apiUrl;
        }

        public async Task CreateCareUsersAsync() 
            => await CreateAsync<CareUserController, CareUserDTO, CreateCareUserDTO>(_careUsers);

        public async Task CreateLegalGuardiansAsync()
            => await CreateAsync<CareUserController, LegalGuardianUserDTO, CreateLegalGuardianUserDTO>(_legalGuardians);

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

        private async void EnsureToken(bool resetToken = false)
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
