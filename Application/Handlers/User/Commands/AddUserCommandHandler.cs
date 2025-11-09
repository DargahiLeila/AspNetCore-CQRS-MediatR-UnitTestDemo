using Application.Commands.User;
using DataAccess.Services.Commands.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.User.Commands
{
    public class AddUserCommandHandler: IRequestHandler<AddUserCommand, int>
    {

        private readonly IUserCommandRepository _repo;

        public AddUserCommandHandler(IUserCommandRepository repo)
        {
            _repo = repo;
        }

       

        public async Task<int> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            // اعتبارسنجی ساده
            if (await _repo.ExistNameAsync(request.Model.Name))
                throw new ArgumentException("نام کاربر تکراری است");

            // ذخیره در دیتابیس
            return await _repo.AddAsync(request.Model);
        }
    }
}
