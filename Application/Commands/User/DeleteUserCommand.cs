using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.User
{
    public class DeleteUserCommand: IRequest<int>
    {

        public int Id { get; }
        public DeleteUserCommand(int id)
        {
            Id = id;
        }

        }
    }
