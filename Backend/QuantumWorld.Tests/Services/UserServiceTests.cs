using AutoMapper;
using Moq;
using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task register_should_invoke_add_on_repository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();

            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object);
            await userService.RegisterAsync("user@email.com", "user", "secret");

            userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
        }
    }
}