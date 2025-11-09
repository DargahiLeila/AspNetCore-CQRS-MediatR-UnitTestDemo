using DomainModel.DTO.UserModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.User
{
    public class UpdateUserCommand: IRequest<int>
    {

        public UserUpdateModel Model { get; }
        public UpdateUserCommand(UserUpdateModel model)
        {
            Model = model;
        }
    }
}
