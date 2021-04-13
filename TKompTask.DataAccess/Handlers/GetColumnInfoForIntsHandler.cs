using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TKompTask.DataAccess.Models;
using TKompTask.DataAccess.Queries;

namespace TKompTask.DataAccess.Handlers
{
    public record GetColumnInfoForIntsHandler : IRequestHandler<GetColumnInfoForIntsQuery, List<ColumnInfo>>
    {
        private readonly IDataAccessService _dataAccess;
        public GetColumnInfoForIntsHandler(IDataAccessService dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<List<ColumnInfo>> Handle(GetColumnInfoForIntsQuery request, CancellationToken cancellationToken)
        {
            return Task.Run<List<ColumnInfo>>(() => {
                    return Task.Run(() => _dataAccess.GetColumnInfoForInts());
            });
        }
    }
}
