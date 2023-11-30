using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volxyseat.Api.Controllers;
using Volxyseat.Domain.Core.Data;
using Volxyseat.Domain.Models.SubscriptionModel;
using Volxyseat.Domain.ViewModel;
using Volxyseat.Infrastructure.Profiles;
using Xunit;

namespace SubscriptionControllerTests.Controllers
{
    public class SubscriptionControllerTests
    {
        private readonly Mock<ISubscriptionRepository> _subscriptionMock;
        private readonly SubscriptionController _controller;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IMapper> _mapper;

        public SubscriptionControllerTests()
        {
            _subscriptionMock = new Mock<ISubscriptionRepository>();
            _uow = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();
            _controller = new SubscriptionController(_subscriptionMock.Object, _uow.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetById_WhenCalled_ReturnOkResult()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var result = await _controller.GetById(id);

            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetById_WhenCalledWithValidId_OkResult()
        {
            // Arrange
            var id = Guid.Parse("dbd2616a-8ece-48b8-9c8a-59383cc0cbbe");
            var subscription = new Subscription
            {
                Id = id,
            };

            _subscriptionMock.Setup(repo => repo.GetById(id))
                             .ReturnsAsync(subscription);

            // Act
            var result = await _controller.GetById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<Subscription>(okResult.Value);
            Assert.Equal(subscription.Id, model.Id);

        }

        [Fact]
        public async Task GetById_WhenCalledWithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _subscriptionMock.Setup(repo => repo.GetById(id)).ReturnsAsync((Subscription)null);

            // Act
            var result = await _controller.GetById(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        //COM AUTOMAPPER
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfiles>();
            });

            // Act & Assert
            config.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_Subscription_To_SubscriptionDto()
        {
            // Arrange
            var subscription = new Subscription { Id = Guid.NewGuid() };
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>()));

            // Act
            var subscriptionDto = mapper.Map<SubscriptionViewModel>(subscription);

            // Assert
            Assert.Equal(subscription.Id, subscriptionDto.Id);
        }

    }
}
