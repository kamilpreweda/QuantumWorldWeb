using AutoMapper;
using Moq;
using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;
using QuantumWorld.Infrastructure.Services;
using MongoDB.Bson;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

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
            var configurationMock = new Mock<IConfiguration>();
            var httpContextMock = new Mock<IHttpContextAccessor>();
            var jwtServiceMock = new Mock<IJwtService>();
            var resourceServiceMock = new Mock<IResourceService>();
            var buildingServiceMock = new Mock<IBuildingService>();
            var researchServiceMock = new Mock<IResearchService>();
            var shipServiceMock = new Mock<IShipService>();
            var battle = new Battle();

            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object, encrypterMock.Object, configurationMock.Object, httpContextMock.Object, jwtServiceMock.Object, resourceServiceMock.Object, buildingServiceMock.Object, researchServiceMock.Object, shipServiceMock.Object);
            await userService.RegisterAsync("secret", "user");

            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }
    }
}