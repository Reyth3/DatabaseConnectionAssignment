using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKompTask.Shared
{
    public class ColumnInfoDto
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public int OrdinalPosition { get; set; }
        public object ColumnDefault { get; set; }
        public bool IsNullable { get; set; }
        public string DataType { get; set; }
        public int Precision { get; set; }
    }
}
