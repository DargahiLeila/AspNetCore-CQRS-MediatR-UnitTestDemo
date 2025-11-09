using Application.Queries.User;
using DataAccess.Services.Queries;
using DomainModel.DTO.UserModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.User.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserGetModel>
    {

        private readonly IUserQueryRepository _repo;

        public GetUserByIdQueryHandler(IUserQueryRepository repo)
        {
            _repo = repo;
        }

        public async Task<UserGetModel?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetByIdAsync(request.Id);
        }
    }
}
