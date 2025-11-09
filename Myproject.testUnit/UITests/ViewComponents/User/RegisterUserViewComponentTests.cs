using Application.Services.Commands.User;
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
    public class RegisterUserViewComponentTests
    {
        [Fact]
        public void Invoke_ShouldReturnDefaultView()
        {
            // Arrange
            var userSrvMock = new Mock<IUserCommandService>();
            var mediatorMock = new Mock<IMediator>();
            var component = new RegisterUserViewComponent(userSrvMock.Object, mediatorMock.Object);

            // Act
            var result = component.Invoke();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewViewComponentResult>();

            var viewResult = result as ViewViewComponentResult;
            viewResult!.ViewName.Should().BeNull();
            // چون در کد View() بدون اسم صدا زده شده، ViewName = null یعنی ویوی پیش‌فرض
        }
    }
}
