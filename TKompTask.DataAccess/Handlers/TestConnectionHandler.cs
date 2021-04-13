using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TKompTask.DataAccess.Queries;

namespace TKompTask.DataAccess.Handlers
{
    public record TestConnectionHandler : IRequestHandler<TestConnectionQuery, bool>
    {
        private readonly IDataAccessService _dataAccess;
        public TestConnectionHandler(IDataAccessService dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<bool> Handle(TestConnectionQuery request, CancellationToken cancellationToken)
        {
            return Task.Run(() => _dataAccess.TryOpenConnection(request.Username, request.Password));
        }
    }
}
