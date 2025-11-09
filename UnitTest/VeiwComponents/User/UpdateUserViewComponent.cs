using Application.Queries.User;
using Application.Services;
using Application.Services.Queries.User;
using DomainModel.DTO.UserModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace UnitTest.VeiwComponents.User
{
    [ViewComponent(Name = "UpdateUser")]
    public class UpdateUserViewComponent:ViewComponent
    {
        //private readonly IUserService _srv;
        private readonly IUserQueryService _queryService;
        private readonly IMediator _mediator;
        public UpdateUserViewComponent(IUserQueryService queryService, IMediator _mediator)
        {
            this._queryService = queryService;
            this._mediator = _mediator;

        }
        public async Task< IViewComponentResult> InvokeAsync( int UserId)
        {
            //var usr = await _queryService.GetUserAsync(UserId);
            var usr = await _mediator.Send(new GetUserByIdQuery(UserId));
            return View(usr);
        }
    }
}
