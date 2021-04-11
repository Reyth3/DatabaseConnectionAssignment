using System;
using System.Data;
using System.Data.SqlClient;

namespace TKompTask.DataAccess
{
    public class DataAccessService : IDataAccessService, IDisposable
    {
        private readonly IDbConnection _con;

        public DataAccessService(IDbConnection con)
        {
            _con = con;
        }

        public bool TryOpenConnection(string username, string password)
        {
            if (_con == null || _con.ConnectionString == null)
                return false;
            _con.ConnectionString = 
                _con.ConnectionString.Replace("{u}", username)
                .Replace("{p}", password);
            _con.Open();
            return _con.State == ConnectionState.Open;
        }

        public void TryCloseConnection()
        {
            if (_con.State != ConnectionState.Closed)
                _con.Close();
        }

        public void Dispose()
        {
            _con.Dispose();
        }
    }
}
