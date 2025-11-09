using DomainModel.DTO.UserModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.User
{
    public class AddUserCommand : IRequest<int>
    {

        public UserAddModel Model { get; }
        public AddUserCommand(UserAddModel model)
        {
            Model = model;
        }
    }
}
