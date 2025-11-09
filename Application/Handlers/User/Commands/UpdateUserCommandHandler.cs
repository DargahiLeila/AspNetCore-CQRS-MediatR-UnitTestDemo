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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {

        private readonly IUserCommandRepository _repo;

        public UpdateUserCommandHandler(IUserCommandRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _repo.ExistNameAsync(request.Model.Name, request.Model.Id))
                throw new ArgumentException("نام کاربر تکراری است");

            return await _repo.UpdateAsync(request.Model);
        }
    
}
}
