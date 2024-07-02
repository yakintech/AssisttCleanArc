using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistt.Application.Commands
{
    public class ProductCommands
    {
        public class CreateProduct : IRequest<int>
        {
            public string Name { get; set; }
        }
   
    }
}
