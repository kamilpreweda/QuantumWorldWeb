using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using QuantumWorld.Infrastructure.Commands.Users;

namespace QuantumWorld.TestsEndToEnd.Controllers
{
    public class AccountControllerTests : ControllerTestsBase
    {
        [Fact]
        public async Task given_valid_current_and_new_password_it_should_be_changed()
        {
            var request = new ChangeUserPassword
            {
                CurrentPassword = "secret",
                NewPassword = "secret2"
            };
            var payload = GetPayload(request);
            var response = await Client.PutAsync("account/password", payload);
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}