using NUnit.Framework;
using Moq;
using FCG.Application.Services;
using FCG.Application.Requests;
using FCG.Domain.Entities;
using FCG.Domain.Exceptions;
using FCG.Domain.Interfaces.Repository;

namespace FCG.Application.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _repoMock;
        private UserService _service;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IUserRepository>();
            _service = new UserService(_repoMock.Object);
        }

        [Test]
        public async Task CreateUserAsync_ValidData_CreatesUser()
        {
            // DADO: um usuário com dados válidos
            var request = new CreateUserRequest
            {
                Name = "João",
                Email = "joao@mail.com",
                Password = "Senha@123"
            };

            _repoMock.Setup(r => r.GetUserByEmailAsync(request.Email)).ReturnsAsync((User)null);
            _repoMock.Setup(r => r.CreateUserAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            // QUANDO: criamos o usuário
            var result = await _service.CreateUserAsync(request);

            // ENTÃO: o usuário deve ser criado com os dados fornecidos
            Assert.IsNotNull(result);
            Assert.AreEqual(request.Email, result.Email);
        }

        [Test]
        public void CreateUserAsync_InvalidEmail_ThrowsValidationException()
        {
            // DADO: um usuário com e-mail inválido
            var request = new CreateUserRequest
            {
                Name = "João",
                Email = "email-invalido",
                Password = "Senha@123"
            };

            // QUANDO: tentamos criar o usuário
            // ENTÃO: deve lançar uma exceção de validação
            var ex = Assert.ThrowsAsync<ValidationUserException>(async () =>
                await _service.CreateUserAsync(request)
            );

            Assert.AreEqual("E-mail inválido", ex.Message);
        }

        [Test]
        public void CreateUserAsync_WeakPassword_ThrowsValidationException()
        {
            // DADO: um usuário com senha fraca
            var request = new CreateUserRequest
            {
                Name = "João",
                Email = "joao@mail.com",
                Password = "123"
            };

            // QUANDO: tentamos criar o usuário
            // ENTÃO: deve lançar uma exceção de validação
            var ex = Assert.ThrowsAsync<ValidationUserException>(async () =>
                await _service.CreateUserAsync(request)
            );

            Assert.IsTrue(ex.Message.Contains("Senha deve ter no mínimo"));
        }

        [Test]
        public void CreateUserAsync_EmailAlreadyExists_ThrowsConflictException()
        {
            // DADO: um e-mail já cadastrado no sistema
            var request = new CreateUserRequest
            {
                Name = "João",
                Email = "joao@mail.com",
                Password = "Senha@123"
            };

            _repoMock.Setup(r => r.GetUserByEmailAsync(request.Email)).ReturnsAsync(new User());

            // QUANDO: tentamos criar um novo usuário com o mesmo e-mail
            // ENTÃO: deve lançar uma exceção de conflito
            var ex = Assert.ThrowsAsync<ConflictException>(async () =>
                await _service.CreateUserAsync(request)
            );

            Assert.AreEqual("E-mail já cadastrado!", ex.Message);
        }
    }
}
