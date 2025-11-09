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
    public class SearchUsersQueryHandler : IRequestHandler<SearchUsersQuery, (List<UserListItem> Users, int RecordCount)>
    {

        private readonly IUserQueryRepository _repo;

        public SearchUsersQueryHandler(IUserQueryRepository repo)
        {
            _repo = repo;
        }

        public async Task<(List<UserListItem> Users, int RecordCount)> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
        {
            return await _repo.SearchAsync(request.SearchModel);
        }
    }
}
