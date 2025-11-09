using Application.Services.Queries.User;
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
    public class UserSearchBoxViewComponentTests
    {
        [Fact]
        public void Invoke_WhenCalled_ShouldReturnViewWithSearchModel()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var queryServiceMock = new Mock<IUserQueryService>();

            var searchModel = new UserSearchModel
            {
                Name = "Ali",
                PageSize = 5,
                PageIndex = 1
            };

            var component = new UserSearchBoxViewComponent(queryServiceMock.Object, mediatorMock.Object);

            // Act
            var result = component.Invoke(searchModel);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewViewComponentResult>();

            var viewResult = result as ViewViewComponentResult;
            viewResult!.ViewName.Should().BeNull(); // چون View() بدون اسم صدا زده شده
            viewResult.ViewData.Model.Should().BeEquivalentTo(searchModel);
        }
    }
}
