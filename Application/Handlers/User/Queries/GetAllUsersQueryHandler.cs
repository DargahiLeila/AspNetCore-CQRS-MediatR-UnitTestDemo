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
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserListItem>>
    {

        private readonly IUserQueryRepository _repo;

        public GetAllUsersQueryHandler(IUserQueryRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<UserListItem>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync();
        }
    }
}
