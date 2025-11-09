using Application.Commands.User;
using Application.Handlers.User.Commands;
using DataAccess.Services.Commands.User;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myproject.testUnit.ApplicationTests.Handlers.Commands.User
{
    public class DeleteUserCommandHandlerTest
    {

        [Fact]
        public async Task Handle_ShouldReturnDeletedUserId()
        {
            var repo = new Mock<IUserCommandRepository>();
            repo.Setup(r => r.DeleteAsync(5)).ReturnsAsync(5);

            var handler = new DeleteUserCommandHandler(repo.Object);
            var cmd = new DeleteUserCommand(5);

            var result = await handler.Handle(cmd, CancellationToken.None);

            result.Should().Be(5);
        }
    }
}
