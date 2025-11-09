using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.User
{
    public class ActiveUserCommand: IRequest<int>
    {

        public int Id { get; }
        public ActiveUserCommand(int id)
        {
            Id = id;
        }
    }
}
