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
    public class UserQueryRepositoryTests
    {
        private Write_Context CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<Write_Context>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new Write_Context(options);
        }

        [Fact]
        public async Task GetAsync_WhenUserExists_ShouldReturnUser()
        {
            var ctx = CreateContext("GetAsyncDb");
            ctx.TblUsers.Add(new TblUser { Id = 1, Name = "Ali", IsDeleted = false });
            await ctx.SaveChangesAsync();

            var repo = new UserQueryRepository(ctx);

            var result = await repo.GetAsync(1);

            result.Should().NotBeNull();
            result!.Name.Should().Be("Ali");
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnOnlyNotDeletedUsers()
        {
            var ctx = CreateContext("GetAllAsyncDb");
            ctx.TblUsers.AddRange(
                new TblUser { Id = 1, Name = "Ali", IsDeleted = false },
                new TblUser { Id = 2, Name = "Sara", IsDeleted = true }
            );
            await ctx.SaveChangesAsync();

            var repo = new UserQueryRepository(ctx);

            var result = await repo.GetAllAsync();

            result.Should().HaveCount(1);
            result[0].Name.Should().Be("Ali");
        }

        [Fact]
        public async Task GetByIdAsync_WhenUserExists_ShouldReturnUserGetModel()
        {
            var ctx = CreateContext("GetByIdAsyncDb");
            ctx.TblUsers.Add(new TblUser { Id = 5, Name = "Reza", IsDeleted = false });
            await ctx.SaveChangesAsync();

            var repo = new UserQueryRepository(ctx);

            var result = await repo.GetByIdAsync(5);

            result.Should().NotBeNull();
            result!.Name.Should().Be("Reza");
        }

        [Fact]
        public void Search_WhenNameFilterApplied_ShouldReturnFilteredUsers()
        {
            var ctx = CreateContext("SearchDb");
            ctx.TblUsers.AddRange(
                new TblUser { Id = 1, Name = "Ali", IsDeleted = false },
                new TblUser { Id = 2, Name = "Alireza", IsDeleted = false },
                new TblUser { Id = 3, Name = "Sara", IsDeleted = false }
            );
            ctx.SaveChanges();

            var repo = new UserQueryRepository(ctx);

            var sm = new UserSearchModel { Name = "Ali", PageIndex = 0, PageSize = 10 };
            var users = repo.Search(sm, out int count);

            count.Should().Be(2);
            users.Should().OnlyContain(u => u.Name.StartsWith("Ali"));
        }

        [Fact]
        public async Task SearchAsync_WhenPagingApplied_ShouldReturnPagedUsers()
        {
            var ctx = CreateContext("SearchAsyncDb");
            for (int i = 1; i <= 15; i++)
            {
                ctx.TblUsers.Add(new TblUser { Id = i, Name = "User" + i, IsDeleted = false });
            }
            await ctx.SaveChangesAsync();

            var repo = new UserQueryRepository(ctx);

            var sm = new UserSearchModel { PageIndex = 1, PageSize = 5 };
            var (users, count) = await repo.SearchAsync(sm);

            count.Should().Be(15);
            users.Should().HaveCount(5);
            users.First().Name.Should().StartWith("User");
        }
    }
}
