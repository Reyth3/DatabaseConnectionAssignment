using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TKompTask.API.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ApiAuthAttribute : Attribute
    {
    }
}
