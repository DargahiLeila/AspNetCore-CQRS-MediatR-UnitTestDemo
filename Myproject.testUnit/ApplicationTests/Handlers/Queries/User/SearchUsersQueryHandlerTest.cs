using Application.Handlers.User.Queries;
using Application.Queries.User;
using DataAccess.Services.Queries;
using DomainModel.DTO.UserModel;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myproject.testUnit.ApplicationTests.Handlers.Queries.User
{
    public class SearchUsersQueryHandlerTest
    {
        [Fact]
        public async Task Handle_ShouldReturnUsersAndCount()
        {
            var repo = new Mock<IUserQueryRepository>();
            repo.Setup(r => r.SearchAsync(It.IsAny<UserSearchModel>()))
                .ReturnsAsync((new List<UserListItem> { new UserListItem { Id = 1, Name = "Ali" } }, 1));

            var handler = new SearchUsersQueryHandler(repo.Object);
            var query = new SearchUsersQuery(new UserSearchModel { Name = "Ali" });

            var (users, count) = await handler.Handle(query, CancellationToken.None);

            users.Should().HaveCount(1);
            count.Should().Be(1);
        }
    }
}
