using System.Net;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.DTO;

namespace QuantumWorld.TestsEndToEnd.Controllers
{
    public class UsersControllerTests : ControllerTestsBase
    {
        [Fact]
        public async Task given_valid_email_user_should_exist()
        {
            var email = "email@email";
            var user = await GetUserAsync(email);
            user.Email.Should().BeEquivalentTo(email);
        }

        [Fact]
        public async Task given_invalid_email_user_should_not_exist()
        {
            var email = "email1000@email";
            var response = await Client.GetAsync($"users/{email}");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task given_unique_email_user_should_be_created()
        {
            var request = new CreateUser
            {
                Email = "test@email.com",
                Username = "test",
                Password = "secret",
                Resources = new(500),
                Buildings = new()
            };
            var payload = GetPayload(request);
            var response = await Client.PostAsync("users", payload);
            response.Headers.Location.ToString().Should().BeEquivalentTo($"users/{request.Email}");

            var user = await GetUserAsync(request.Email);
            user.Email.Should().BeEquivalentTo(request.Email);
        }

        private async Task<UserDto> GetUserAsync(string email)
        {
            var response = await Client.GetAsync($"users/{email}");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UserDto>(responseString);
        }
    }
}