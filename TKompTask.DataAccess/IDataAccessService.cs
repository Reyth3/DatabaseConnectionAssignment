using System.Collections.Generic;
using TKompTask.DataAccess.Models;

namespace TKompTask.DataAccess
{
    public interface IDataAccessService
    {
        bool TryOpenConnection(string username, string password);
        List<ColumnInfo> GetColumnInfoForInts();
    }
}