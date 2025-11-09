using Application.Queries.User;
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
using UnitTest.Models.User;
using UnitTest.VeiwComponents.User;

namespace Myproject.testUnit.UITests.ViewComponents.User
{
    public class UserListViewComponentTests
    {
        [Fact]
        public async Task InvokeAsync_WhenCalled_ShouldReturnViewWithUserListAndPaging()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var queryServiceMock = new Mock<IUserQueryService>();

            var searchModel = new UserSearchModel { PageSize = 0 };

            var fakeUsers = new List<UserListItem>
    {
        new UserListItem { Id = 1, Name = "Ali" },
        new UserListItem { Id = 2, Name = "Sara" }
    };

            mediatorMock.Setup(m => m.Send(It.IsAny<SearchUsersQuery>(), default))
                        .ReturnsAsync((fakeUsers, 2)); // تاپل

            var component = new UserListViewComponent(queryServiceMock.Object, mediatorMock.Object);

            // Act
            var result = await component.InvokeAsync(searchModel);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewViewComponentResult>();

            var viewResult = result as ViewViewComponentResult;
            var model = viewResult!.ViewData.Model as UserListAndSearchModel;

            model.Should().NotBeNull();
            model!.UserListItems.Should().HaveCount(2);
            model.UserListItems[0].Name.Should().Be("Ali");
            model.UserListItems[1].Name.Should().Be("Sara");

            searchModel.PageSize.Should().Be(10);
            searchModel.RecordCount.Should().Be(2);
            searchModel.PageCount.Should().Be(1);
        }
    }
}
