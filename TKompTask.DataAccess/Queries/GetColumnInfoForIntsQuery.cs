using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKompTask.DataAccess.Models;

namespace TKompTask.DataAccess.Queries
{
    public record GetColumnInfoForIntsQuery : IRequest<List<ColumnInfo>>
    {
    }
}
