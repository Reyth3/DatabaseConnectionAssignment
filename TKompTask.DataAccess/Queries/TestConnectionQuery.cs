using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKompTask.DataAccess.Queries
{
    public record TestConnectionQuery : IRequest<bool>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public TestConnectionQuery(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
