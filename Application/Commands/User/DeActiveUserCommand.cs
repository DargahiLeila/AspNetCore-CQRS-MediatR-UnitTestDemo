using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.User
{
    public class DeActiveUserCommand: IRequest<int>
    {

        public int Id { get; }
        public DeActiveUserCommand(int id)
        {
            Id = id;
        }
    }
}
