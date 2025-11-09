using Application.Commands.User;
using DomainModel.DTO.UserModel;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model.Structures;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Controllers;
using UnitTest.DTOS;
using UnitTest.ViewModel.User;

namespace Myproject.testUnit.UITests.Controllers.User
{
    public class UserManageControllerTests
    {
        
    private readonly Mock<IMediator> _mediatorMock;
        private readonly UserManageController _controller;

        public UserManageControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new UserManageController(null!, null!, _mediatorMock.Object);
        }

        [Fact]
        public void Index_ShouldReturnViewWithModel()
        {
            var sm = new UserSearchModel { Name = "Ali" };

            var result = _controller.Index(sm) as ViewResult;

            result.Should().NotBeNull();
            result!.Model.Should().Be(sm);
        }

        [Fact]
        public void ListAction_ShouldReturnViewComponent()
        {
            var sm = new UserSearchModel();

            var result = _controller.ListAction(sm) as ViewComponentResult;

            result.Should().NotBeNull();
            result!.ViewComponentName.Should().Be("UserList");
            result.Arguments.Should().Be(sm);
        }

        [Fact]
        public void SearchBoxAction_ShouldReturnViewComponent()
        {
            //Arrange --  آماده کردن شرایط تست
            var sm = new UserSearchModel();
            //Act --- صدا کردن اون متدی که قراره تست بشه
            var result = _controller.SearchBoxAction(sm) as ViewComponentResult;
            //Assert --- بررسی نتیجه
            result.Should().NotBeNull();
            result!.ViewComponentName.Should().Be("UserSearchBox");
        }

        [Fact]
        public void Add_Get_ShouldReturnRegisterUserViewComponent()
        {
            var result = _controller.Add() as ViewComponentResult;

            result.Should().NotBeNull();
            result!.ViewComponentName.Should().Be("RegisterUser");
        }

        [Fact]
        public async Task Add_Post_WhenValid_ShouldReturnSuccessJson()
        {
            // Arrange
            var vm = new UserAddEditViewModel { Name = "Ali" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<AddUserCommand>(), default))
                         .ReturnsAsync(1);

            // Act
            var result = await _controller.Add(vm) as JsonResult;

            // Assert
            result.Should().NotBeNull();
            var data = result!.Value as OperationResult;
            data.Should().NotBeNull();
            data!.success.Should().BeTrue();
            data.id.Should().Be(1);
            data.message.Should().Be("کاربر با موفقیت ثبت شد");
        }

        [Fact]
        public async Task Add_Post_WhenDuplicateName_ShouldReturnErrorJson()
        {
            // Arrange
            var vm = new UserAddEditViewModel { Name = "Ali" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<AddUserCommand>(), default))
                         .ThrowsAsync(new ArgumentException("نام کاربر تکراری است"));

            // Act
            var result = await _controller.Add(vm) as JsonResult;

            // Assert
            result.Should().NotBeNull();
            var data = result!.Value as OperationResult;
            data.Should().NotBeNull();
            data!.success.Should().BeFalse();
            data.message.Should().Be("نام کاربر تکراری است");
        }


        [Fact]
        public async Task DeActiveUser_WhenValid_ShouldReturnSuccessJson()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeActiveUserCommand>(), default))
                         .ReturnsAsync(10);
            // Act
            var result = await _controller.DeActiveUser(10) as JsonResult;

            // Assert
            result.Should().NotBeNull();
            var data = result!.Value as OperationResult;
            data.Should().NotBeNull();
            data!.success.Should().BeTrue();
            data.id.Should().Be(10);
        }

        [Fact]
        public async Task DeActiveUser_WhenExceptionThrown_ShouldReturnFailureJson()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeActiveUserCommand>(), default))
                         .ThrowsAsync(new Exception("خطای تستی"));

            // Act
            var result = await _controller.DeActiveUser(10) as JsonResult;

            // Assert
            result.Should().NotBeNull();
            var data = result!.Value as OperationResult;
            data.Should().NotBeNull();
            data!.success.Should().BeFalse();
            data.message.Should().Be("خطا در غیرفعال کردن کاربر");
        }


        [Fact]
        public async Task ActiveUser_WhenValid_ShouldReturnSuccessJson()
        {
            //Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<ActiveUserCommand>(), default))
                         .ReturnsAsync(20);
            //Act
            var result = await _controller.ActiveUser(20) as JsonResult;

           // Assert
            result.Should().NotBeNull();
            var data = result!.Value as OperationResult;
            data.Should().NotBeNull();
            data!.success.Should().BeTrue();
            data.id.Should().Be(20);
            data.message.Should().Be("کاربر با موفقیت فعال شد");
        }

        [Fact]
        public async Task ActiveUser_WhenExceptionThrown_ShouldReturnFailureJson()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<ActiveUserCommand>(), default))
                         .ThrowsAsync(new Exception("خطای تستی"));

            // Act
            var result = await _controller.ActiveUser(20) as JsonResult;

            // Assert
            result.Should().NotBeNull();
            var data = result!.Value as OperationResult;
            data.Should().NotBeNull();
            data!.success.Should().BeFalse();
            data.message.Should().Be("خطا در فعال کردن کاربر");
        }


        [Fact]
        public void Update_Get_ShouldReturnUpdateUserViewComponent()
        {
            var result = _controller.Update(5) as ViewComponentResult;

            result.Should().NotBeNull();
            result!.ViewComponentName.Should().Be("UpdateUser");
            result.Arguments.Should().Be(5);
        }

        [Fact]
        public async Task Update_Post_WhenValid_ShouldReturnSuccessJson()
        {
            // Arrange
            var vm = new UserAddEditViewModel { Id = 5, Name = "Sara" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateUserCommand>(), default))
                         .ReturnsAsync(5);
            // Act
            var result = await _controller.Update(vm) as JsonResult;
           
            // Assert
            result.Should().NotBeNull();
            var data = result!.Value as OperationResult;
            data.Should().NotBeNull();
            data!.success.Should().BeTrue();
            data.id.Should().Be(5);
            data.message.Should().Be("کاربر با موفقیت ویرایش شد");
        }

        [Fact]
        public async Task Update_Post_WhenDuplicateName_ShouldReturnErrorJson()
        {
            // Arrange
            var vm = new UserAddEditViewModel { Id = 5, Name = "Sara" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateUserCommand>(), default))
                         .ThrowsAsync(new ArgumentException("نام کاربر تکراری است"));

            // Act
            var result = await _controller.Update(vm) as JsonResult;

            // Assert
            result.Should().NotBeNull();
            var data = result!.Value as OperationResult;
            data.Should().NotBeNull();
            data!.success.Should().BeFalse();
            data.message.Should().Be("نام کاربر تکراری است");
        }

    }
}
