using Application.Commands.User;
using Application.Handlers.User.Commands;
using DataAccess.Services.Commands.User;
using DomainModel.DTO.UserModel;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myproject.testUnit.ApplicationTests.Handlers.Commands.User
{
    public class UpdateUserCommandHandlerTest
    {
        [Fact]
        public async Task Handle_WhenNameExists_ShouldThrowArgumentException()
        {
            var repo = new Mock<IUserCommandRepository>();
            repo.Setup(r => r.ExistNameAsync("Ali", 1)).ReturnsAsync(true);

            var handler = new UpdateUserCommandHandler(repo.Object);
            var cmd = new UpdateUserCommand(new UserUpdateModel { Id = 1, Name = "Ali" });

            Func<Task> act = async () => await handler.Handle(cmd, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("نام کاربر تکراری است");
        }

        [Fact]
        public async Task Handle_WhenValid_ShouldReturnUpdatedId()
        {
            var repo = new Mock<IUserCommandRepository>();
            repo.Setup(r => r.ExistNameAsync("Sara", 1)).ReturnsAsync(false);
            repo.Setup(r => r.UpdateAsync(It.IsAny<UserUpdateModel>())).ReturnsAsync(1);

            var handler = new UpdateUserCommandHandler(repo.Object);
            var cmd = new UpdateUserCommand(new UserUpdateModel { Id = 1, Name = "Sara" });

            var result = await handler.Handle(cmd, CancellationToken.None);

            result.Should().Be(1);
        }
    }
}
