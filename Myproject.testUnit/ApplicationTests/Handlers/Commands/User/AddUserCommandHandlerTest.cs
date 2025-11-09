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
    public class AddUserCommandHandlerTest
    {
        [Fact]
        public async Task Handle_WhenNameExists_ShouldThrowArgumentException()
        {
            var repo = new Mock<IUserCommandRepository>();
            repo.Setup(r => r.ExistNameAsync("Ali")).ReturnsAsync(true);

            var handler = new AddUserCommandHandler(repo.Object);
            var cmd = new AddUserCommand(new UserAddModel { Name = "Ali", IsDeleted = false });

            Func<Task> act = async () => await handler.Handle(cmd, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("نام کاربر تکراری است");
        }

        [Fact]
        public async Task Handle_WhenValid_ShouldReturnNewUserId()
        {
            var repo = new Mock<IUserCommandRepository>();
            repo.Setup(r => r.ExistNameAsync("Sara")).ReturnsAsync(false);
            repo.Setup(r => r.AddAsync(It.IsAny<UserAddModel>())).ReturnsAsync(10);

            var handler = new AddUserCommandHandler(repo.Object);
            var cmd = new AddUserCommand(new UserAddModel { Name = "Sara", IsDeleted = false });

            var result = await handler.Handle(cmd, CancellationToken.None);

            result.Should().Be(10);
        }
    }
}
