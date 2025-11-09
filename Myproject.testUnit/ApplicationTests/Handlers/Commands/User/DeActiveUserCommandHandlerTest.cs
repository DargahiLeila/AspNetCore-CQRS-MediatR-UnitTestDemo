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
    public class DeActiveUserCommandHandlerTest
    {
        [Fact]
        public async Task Handle_ShouldReturnDeactivatedUserId()
        {
            var repo = new Mock<IUserCommandRepository>();
            repo.Setup(r => r.DeActiveUserAsync(8)).ReturnsAsync(8);

            var handler = new DeActiveUserCommandHandler(repo.Object);
            var cmd = new DeActiveUserCommand(8);

            var result = await handler.Handle(cmd, CancellationToken.None);

            result.Should().Be(8);
        }
    }
}
