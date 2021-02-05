using ClientsManager.WebApi.Controllers;
using ClientsManager.WebApi.Data;
using ClientsManager.WebApi.Data.Contracts;
using ClientsManager.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ClientsManager.WebApi.Tests.MSTest
{
    [TestClass]
    public class UnitTestController
    {
        [TestMethod]
        public void AddClientTest()
        {
            //Arrange
            var newCliente = new Cliente()
            {
                Nome = "Joana",
                CPF = "529.982.247-25",
                DataNascimento = "01/05/2001",
                ClientesEnderecos = null
            };

            var mockRepo = new Mock<IClientRepository>();
            mockRepo.Setup(x => x.SaveChanges()).Returns(true);
            var controller = new ClienteController(mockRepo.Object);

            //Act
            ActionResult actionResult = controller.AddCliente(newCliente);

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        }

        [TestMethod]
        public void UpdateClienteTest()
        {
            //Arrange
            var newCliente = new Cliente()
            {
                Id = 3,
                Nome = "Joana",
                CPF = "529.982.247-25",
                DataNascimento = "01/05/2001",
                ClientesEnderecos = null
            };

            var mockRepo = new Mock<IClientRepository>();
            mockRepo.Setup(x => x.SaveChanges()).Returns(true);
            var controller = new ClienteController(mockRepo.Object);

            //Act
            ActionResult actionResult = controller.UpdateCliente(newCliente);

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetClientTest()
        {
            //Arrange
            var mockRepo = new Mock<IClientRepository>();
            mockRepo.Setup(x => x.GetObjectById<Cliente>(1)).Returns(new Cliente { Id = 1 });
            var controller = new ClienteController(mockRepo.Object);

            //Act
            ActionResult<Cliente> actionResult = controller.ClienteById("1");
            //var contentResult = actionResult.Result;

            //Assert
            Assert.AreEqual(1, actionResult.Value.Id);
        }
    }
}
