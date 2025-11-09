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
    public class GetUserByIdQueryHandlerTest
    {
        [Fact]
        public async Task Handle_ShouldReturnUser()
        {
            var repo = new Mock<IUserQueryRepository>();
            repo.Setup(r => r.GetByIdAsync(5))
                .ReturnsAsync(new UserGetModel { Id = 5, Name = "Sara" });

            var handler = new GetUserByIdQueryHandler(repo.Object);
            var query = new GetUserByIdQuery(5);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result!.Name.Should().Be("Sara");
        }
    }
}
