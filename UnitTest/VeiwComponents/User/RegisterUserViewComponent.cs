using Application.Services;
using Application.Services.Commands.User;
using Application.Services.Queries.User;
using DomainModel.DTO.UserModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UnitTest.VeiwComponents.User
{
    [ViewComponent(Name = "RegisterUser")]
    public class RegisterUserViewComponent:ViewComponent
    {
        //private readonly IUserService UserSrv;
        private readonly IUserCommandService UserSrv;
        private readonly IMediator _mediator;

        public RegisterUserViewComponent(IUserCommandService UserSrv, IMediator _mediator)
        {
            this.UserSrv = UserSrv;
            this._mediator = _mediator;
        }

        public IViewComponentResult Invoke()
        {
            return  View();
        }
    }
}
