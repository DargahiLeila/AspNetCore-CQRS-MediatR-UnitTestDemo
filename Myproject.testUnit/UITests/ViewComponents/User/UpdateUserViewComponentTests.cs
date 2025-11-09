using Application.Services.Queries.User;
using Application.Queries.User;
using DomainModel.DTO.UserModel;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.VeiwComponents.User;

namespace Myproject.testUnit.UITests.ViewComponents.User
{
    public class UpdateUserViewComponentTests
    {
        [Fact]
        public async Task InvokeAsync_WhenCalled_ShouldReturnViewWithUser()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var queryServiceMock = new Mock<IUserQueryService>();

            var fakeUser = new UserGetModel { Id = 5, Name = "Sara" };

            mediatorMock.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default))
                        .ReturnsAsync(fakeUser);

            var component = new UpdateUserViewComponent(queryServiceMock.Object, mediatorMock.Object);

            // Act
            var result = await component.InvokeAsync(5);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewViewComponentResult>();

            var viewResult = result as ViewViewComponentResult;
            viewResult!.ViewName.Should().BeNull(); // چون View() بدون اسم صدا زده شده
            viewResult.ViewData.Model.Should().BeEquivalentTo(fakeUser);

            // همچنین می‌تونی مطمئن بشی که Mediator دقیقاً یک بار صدا زده شده
            mediatorMock.Verify(m => m.Send(It.Is<GetUserByIdQuery>(q => q.Id == 5), default), Times.Once);
        }
    }
}
