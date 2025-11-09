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
    public class ActiveUserCommandHandlerTest
    {
        [Fact]
        public async Task Handle_ShouldReturnActivatedUserId()
        {
            var repo = new Mock<IUserCommandRepository>();
            repo.Setup(r => r.ActiveUserAsync(7)).ReturnsAsync(7);

            var handler = new ActiveUserCommandHandler(repo.Object);
            var cmd = new ActiveUserCommand(7);

            var result = await handler.Handle(cmd, CancellationToken.None);

            result.Should().Be(7);
        }
    }
}
