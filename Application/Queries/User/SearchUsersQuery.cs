using DomainModel.DTO.UserModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.User
{
    public class SearchUsersQuery : IRequest<(List<UserListItem> Users, int RecordCount)>
    {

        public UserSearchModel SearchModel { get; }

        public SearchUsersQuery(UserSearchModel searchModel)
        {
            SearchModel = searchModel;
        }
    }
}
