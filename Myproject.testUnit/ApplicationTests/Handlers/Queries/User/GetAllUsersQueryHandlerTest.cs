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
    public class GetAllUsersQueryHandlerTest
    {

        [Fact]
        public async Task Handle_ShouldReturnAllUsers()
        {
            var repo = new Mock<IUserQueryRepository>();
            repo.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<UserListItem> { new UserListItem { Id = 1, Name = "Ali" } });

            var handler = new GetAllUsersQueryHandler(repo.Object);
            var query = new GetAllUsersQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().HaveCount(1);
            result[0].Name.Should().Be("Ali");
        }
    }
}
