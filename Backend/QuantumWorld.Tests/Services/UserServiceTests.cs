using AutoMapper;
using Moq;
using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;
using QuantumWorld.Infrastructure.Services;
using MongoDB.Bson;

namespace QuantumWorld.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task register_should_invoke_add_on_repository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<IEncrypter>();
            var battle = new Battle();

            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object, encrypterMock.Object);
            await userService.RegisterAsync(Guid.NewGuid(), "user@email.com", "secret", "user");

            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }
    }
}