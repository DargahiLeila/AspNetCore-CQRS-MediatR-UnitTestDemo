using Application.Commands.User;
using Application.Services;
using Application.Services.Commands.User;
using Application.Services.Queries.User;
using DomainModel.DTO.UserModel;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using UnitTest.DTOS;
using UnitTest.ViewModel.User;

namespace UnitTest.Controllers
{
    public class UserManageController : Controller
    {
       
        private readonly IUserCommandService _commandService;
        private readonly IUserQueryService _queryService;
        private readonly IMediator _mediator;
        public UserManageController(IUserCommandService commandService, IUserQueryService queryService,IMediator mediator)
        {
            _commandService = commandService;
            _queryService = queryService;
            this._mediator = mediator;
        }



        public IActionResult Index(UserSearchModel sm)
        {
            return View(sm);
        }
     

        [HttpGet]
        public IActionResult ListAction(UserSearchModel sm)
        {
            return  ViewComponent("UserList", sm);
        }
      

        [HttpGet]
        public IActionResult SearchBoxAction(UserSearchModel sm)
        {
            return  ViewComponent("UserSearchBox", sm);
        }
       
        [HttpGet]
        public IActionResult Add()
        {
            return  ViewComponent("RegisterUser");
        }





        [HttpPost]
        public async Task<JsonResult> Add(UserAddEditViewModel usr)
        {
            try
            {
                var model = new UserAddModel
                {
                    Name = usr.Name,
                    IsDeleted = false
                };


                var insertedUserId = await _mediator.Send(new AddUserCommand(model));

                return Json(new OperationResult
                {
                    success = true,
                    message = "کاربر با موفقیت ثبت شد",
                    id = insertedUserId
                });
            }
            catch (ArgumentException ex)
            {
                return Json(new OperationResult { success = false, message = ex.Message });
            }
            catch (Exception)
            {
                return Json(new OperationResult { success = false, message = "خطا در ثبت کاربر" });
            }

        }

        

        [HttpPost]
        public async Task<JsonResult> DeActiveUser(int id)
        {
            try
            {
                
                var deactivatedUserId = await _mediator.Send(new DeActiveUserCommand(id));

                return Json(new OperationResult
                {
                    success = true,
                    message = "کاربر با موفقیت غیرفعال شد",
                    id = deactivatedUserId
                });
            }
            catch (Exception)
            {
                return Json(new OperationResult
                {
                    success = false,
                    message = "خطا در غیرفعال کردن کاربر"
                });
            }
        }



        [HttpPost]
        public async Task<JsonResult> ActiveUser(int id)
        {
            try
            {
                
                var activatedUserId = await _mediator.Send(new ActiveUserCommand(id));

                return Json(new OperationResult
                {
                    success = true,
                    message = "کاربر با موفقیت فعال شد",
                    id = activatedUserId
                });
            }
            catch (Exception)
            {
                return Json(new OperationResult
                {
                    success = false,
                    message = "خطا در فعال کردن کاربر"
                });
            }
        }

        [HttpGet]
        public IActionResult Update(int UserID)
        {

            return ViewComponent("UpdateUser", UserID);
        }




        [HttpPost]
        public async Task<JsonResult> Update(UserAddEditViewModel usr)
        {
            try
            {
                var model = new UserUpdateModel
                {
                    Id = usr.Id,
                    Name = usr.Name,
                };

                var updatedUserId = await _mediator.Send(new UpdateUserCommand(model));

                return Json(new OperationResult
                {
                    success = true,
                    message = "کاربر با موفقیت ویرایش شد",
                    id = updatedUserId
                });
            }
            catch (ArgumentException ex)
            {
                return Json(new OperationResult { success = false, message = ex.Message });
            }
            catch (Exception)
            {
                return Json(new OperationResult { success = false, message = "خطا در ویرایش کاربر" });
            }
        }





    }
}
