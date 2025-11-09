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
    public class DeActiveUserCommandHandler: IRequestHandler<DeActiveUserCommand, int>
    {

        private readonly IUserCommandRepository _repo;
        public DeActiveUserCommandHandler(IUserCommandRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(DeActiveUserCommand request, CancellationToken cancellationToken)
        {
            return await _repo.DeActiveUserAsync(request.Id);
        }
    }
}
