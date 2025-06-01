using FCG.Domain.Interfaces.Repository;
using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCG.Application.Services;
using FCG.Domain.Entities;
using FCG.Domain.Interfaces.Repository;
using FCG.Application.Requests;

namespace FCG.Application.Tests {

    [TestFixture]
    public class AdminUserServiceTests {

        private AdminService _service;
        private Mock<IUserRepository> _repoMock;

        [SetUp]
        public void Setup() {
            _repoMock = new Mock<IUserRepository>();
            _service = new AdminService(_repoMock.Object);
        }

        [Test]
        public async Task PromoteUser_UpdatesRoleToAdmin() {
            // DADO: um usuário comum
            var user = new User {
                Id = 1,
                Name = "noel",
                Email = "noel@mail.com",
                Password = "Senha@123",
                IsAdmin = false
            };
            _repoMock.Setup(r => r.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(user);
            _repoMock.Setup(r => r.UpdateUserAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            // QUANDO: promovemos o usuário
            await _service.PromoteUserAsync(user.Id);
            // ENTÃO: ele deve ser administrador
            Assert.IsTrue(user.IsAdmin);
        }

        [Test]
        public async Task DeleteUser_RemovesUser() {
            // DADO: um usuário comum
            var user = new User {
                Id = 2,
                Name = "potter",
                Email = "potter@mail.com",
                Password = "Senha@123",
                IsBanned = false
            };
            _repoMock.Setup(r => r.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(user);
            _repoMock.Setup(r => r.UpdateUserAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            // QUANDO: banimos o usuário
            await _service.BanUserAsync(user.Id);
            // ENTÃO: ele deve estar marcado como banido
            Assert.IsTrue(user.IsBanned);
        }
    }
}
