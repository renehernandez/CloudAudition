using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudAuditionApi.Controllers;
using CloudAuditionApi.DatabaseService;
using CloudAuditionApi.Models;
using Moq;
using NUnit.Framework;

namespace CloudAuditionApi.UnitTests
{
    public class GetMessagesTest 
    {   
        [Test]
        public async Task ReturnsExpectedCount() 
        {
            // Arrange
            var mockService = new Mock<IMessageDbService>();
            mockService.Setup(service => service.GetAllAsync())
                .ReturnsAsync(TestHelper.GetMessages());

            var controller = new MessagesController(mockService.Object);

            var result = await controller.GetMessagesAsync();

            Assert.That(result.Value.Count(), Is.EqualTo(2));
        }
    }

    public class GetMessageTest 
    {   
        [Test]
        public async Task ReturnsMessageObjMatchingId() 
        {
            // Arrange
            var mockService = new Mock<IMessageDbService>();
            mockService.Setup(service => service.FindAsync(It.IsAny<long>()))
                .ReturnsAsync((long id) => new Message() { Id = id, Content = "Hello World"});

            var controller = new MessagesController(mockService.Object);

            var result = await controller.GetMessageAsync(3);

            Assert.That(result.Value.Id, Is.EqualTo(3));
        }
    }

    class TestHelper 
    {
        public static List<Message> GetMessages() {
            return new List<Message>(){ 
                    new Message() { Id = 1, Content = "Hello"},
                    new Message() { Id = 2, Content = "World"}
                };
        }
    }
}