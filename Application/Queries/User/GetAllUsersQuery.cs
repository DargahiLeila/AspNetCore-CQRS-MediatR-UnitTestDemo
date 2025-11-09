using DomainModel.DTO.UserModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.User
{
    public class GetAllUsersQuery : IRequest<List<UserListItem>>
    {

    }
}
