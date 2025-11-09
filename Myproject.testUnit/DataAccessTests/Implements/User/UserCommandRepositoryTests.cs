using DataAccess.Implements.User;
using DomainModel.DTO.UserModel;
using DomainModel.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myproject.testUnit.DataAccessTests.Implements.User
{
    public class UserCommandRepositoryTests
    {
        private Write_Context CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<Write_Context>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new Write_Context(options);
        }

        [Fact]
        public async Task AddAsync_ShouldInsertUserAndReturnId()
        {
            var ctx = CreateContext("AddAsyncDb");
            var repo = new UserCommandRepository(ctx);

            var id = await repo.AddAsync(new UserAddModel { Name = "Ali", IsDeleted = false });

            id.Should().BeGreaterThan(0);
            var inserted = await ctx.TblUsers.FindAsync(id);
            inserted.Should().NotBeNull();
            inserted!.Name.Should().Be("Ali");
        }

        [Fact]
        public async Task UpdateAsync_WhenUserExists_ShouldUpdateAndReturnId()
        {
            var ctx = CreateContext("UpdateAsyncDb");
            var user = new TblUser { Name = "OldName", IsDeleted = false };
            ctx.TblUsers.Add(user);
            await ctx.SaveChangesAsync();

            var repo = new UserCommandRepository(ctx);
            var result = await repo.UpdateAsync(new UserUpdateModel { Id = user.Id, Name = "NewName" });

            result.Should().Be(user.Id);
            (await ctx.TblUsers.FindAsync(user.Id))!.Name.Should().Be("NewName");
        }

        [Fact]
        public async Task UpdateAsync_WhenUserNotExists_ShouldReturnZero()
        {
            var ctx = CreateContext("UpdateAsyncNotFoundDb");
            var repo = new UserCommandRepository(ctx);

            var result = await repo.UpdateAsync(new UserUpdateModel { Id = 99, Name = "X" });

            result.Should().Be(0);
        }

        [Fact]
        public async Task ActiveUserAsync_ShouldSetIsDeletedFalse()
        {
            var ctx = CreateContext("ActiveUserDb");
            var user = new TblUser { Name = "Test", IsDeleted = true };
            ctx.TblUsers.Add(user);
            await ctx.SaveChangesAsync();

            var repo = new UserCommandRepository(ctx);
            var result = await repo.ActiveUserAsync(user.Id);

            result.Should().Be(user.Id);
            (await ctx.TblUsers.FindAsync(user.Id))!.IsDeleted.Should().BeFalse();
        }

        [Fact]
        public async Task DeActiveUserAsync_ShouldSetIsDeletedTrue()
        {
            var ctx = CreateContext("DeActiveUserDb");
            var user = new TblUser { Name = "Test", IsDeleted = false };
            ctx.TblUsers.Add(user);
            await ctx.SaveChangesAsync();

            var repo = new UserCommandRepository(ctx);
            var result = await repo.DeActiveUserAsync(user.Id);

            result.Should().Be(user.Id);
            (await ctx.TblUsers.FindAsync(user.Id))!.IsDeleted.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_ShouldSetIsDeletedTrue()
        {
            var ctx = CreateContext("DeleteUserDb");
            var user = new TblUser { Name = "Test", IsDeleted = false };
            ctx.TblUsers.Add(user);
            await ctx.SaveChangesAsync();

            var repo = new UserCommandRepository(ctx);
            var result = await repo.DeleteAsync(user.Id);

            result.Should().Be(user.Id);
            (await ctx.TblUsers.FindAsync(user.Id))!.IsDeleted.Should().BeTrue();
        }

        [Fact]
        public async Task ExistNameAsync_WhenNameExists_ShouldReturnTrue()
        {
            var ctx = CreateContext("ExistNameDb");
            ctx.TblUsers.Add(new TblUser { Name = "Ali", IsDeleted = false });
            await ctx.SaveChangesAsync();

            var repo = new UserCommandRepository(ctx);
            var exists = await repo.ExistNameAsync("Ali");

            exists.Should().BeTrue();
        }

        [Fact]
        public async Task ExistNameAsync_WithUserId_ShouldReturnFalseIfSameUser()
        {
            var ctx = CreateContext("ExistNameWithIdDb");
            var user = new TblUser { Name = "Ali", IsDeleted = false };
            ctx.TblUsers.Add(user);
            await ctx.SaveChangesAsync();

            var repo = new UserCommandRepository(ctx);
            var exists = await repo.ExistNameAsync("Ali", user.Id);

            exists.Should().BeFalse();
        }
    }
}
