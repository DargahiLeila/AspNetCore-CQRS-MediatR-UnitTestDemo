using Application.Queries.User;
using Application.Services;
using Application.Services.Queries.User;
using DomainModel.DTO.UserModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UnitTest.VeiwComponents.User
{
    [ViewComponent(Name = "UserSearchBox")]
    public class UserSearchBoxViewComponent: ViewComponent
    {
        //private readonly IUserService UserSrv;
        private readonly IUserQueryService _queryService;
        private readonly IMediator _mediator;
        public UserSearchBoxViewComponent(IUserQueryService queryService, IMediator _mediator)
        {
            this._queryService = queryService;
            this._mediator = _mediator;
        }
        
        public IViewComponentResult Invoke(UserSearchModel sm)
        {
            // اگر اینجا یه موقه دراپ دان داشتیم باید متدشو با send صدا میکنم.
           // var usr = await _mediator.Send(new GetDrpTblNameQuery();
            return View(sm);
        }
    }
}
