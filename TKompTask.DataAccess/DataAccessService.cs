using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using TKompTask.DataAccess.Models;

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
            else if (_con.State == ConnectionState.Open)
                return true;
            _con.ConnectionString = 
                _con.ConnectionString.Replace("{u}", username)
                .Replace("{p}", password);
            _con.Open();
            return _con.State == ConnectionState.Open;
        }

        public List<ColumnInfo> GetColumnInfoForInts()
        {
            using var cmd = _con.CreateCommand();
            cmd.CommandText = @"SELECT TABLE_NAME, COLUMN_NAME, ORDINAL_POSITION, COLUMN_DEFAULT, IS_NULLABLE, DATA_TYPE, NUMERIC_PRECISION
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE DATA_TYPE = 'int'
                ORDER BY ORDINAL_POSITION";
            var reader = cmd.ExecuteReader();
            var list = new List<ColumnInfo>();
            while (reader.Read())
                list.Add(new ColumnInfo()
                {
                    TableName = reader.GetString(0),
                    ColumnName = reader.GetString(1),
                    OrdinalPosition = reader.GetInt32(2),
                    ColumnDefault = reader.GetValue(3),
                    IsNullable = reader.GetString(4) == "YES",
                    DataType = reader.GetString(5),
                    Precision = reader.GetByte(6)
                });
            return list;
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

        /* SELECT TABLE_NAME, COLUMN_NAME, ORDINAL_POSITION, COLUMN_DEFAULT, IS_NULLABLE, DATA_TYPE, NUMERIC_PRECISION
FROM INFORMATION_SCHEMA.COLUMNS
WHERE DATA_TYPE = 'int'
ORDER BY ORDINAL_POSITION

*/
    }
}
