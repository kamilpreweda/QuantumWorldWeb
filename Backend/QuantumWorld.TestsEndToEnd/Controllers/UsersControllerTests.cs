using System.Net;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using MongoDB.Bson;
using Newtonsoft.Json;
using QuantumWorld.Core.Domain;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.DTO;

namespace QuantumWorld.TestsEndToEnd.Controllers
{
    public class UsersControllerTests : ControllerTestsBase
    {
        [Fact]
        public async Task given_valid_username_user_should_exist()
        {
            var username = "string";
            var user = await GetUserAsync(username);
            user.Username.Should().BeEquivalentTo(username);
        }

        [Fact]
        public async Task given_invalid_username_user_should_not_exist()
        {
            var username = "wrongUsername";
            var response = await Client.GetAsync($"users/{username}");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task given_unique_username_user_should_be_created()
        {
            var request = new CreateUser
            {
                Password = "string",
                Username = "string",

            };
            var payload = GetPayload(request);
            var response = await Client.PostAsync("users/register", payload);
            response.Headers.Location.ToString().Should().BeEquivalentTo($"users/{request.Username}");

            var user = await GetUserAsync(request.Username);
            user.Username.Should().BeEquivalentTo(request.Username);
        }

        private async Task<UserDto> GetUserAsync(string username)
        {
            var response = await Client.GetAsync($"users/{username}");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UserDto>(responseString, new Newtonsoft.Json.JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            });
        }
    }
}