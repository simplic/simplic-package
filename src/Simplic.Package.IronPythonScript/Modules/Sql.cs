using Dapper;
using IronPython.Runtime;
using Simplic.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.IronPythonScript
{
    public static partial class PythonApiModule
    {
        [PythonType("Sql")]
        public static class Sql
        {
            private static ISqlService sqlService;

            static Sql()
            {
                sqlService = CommonServiceLocator.ServiceLocator.Current.GetInstance<ISqlService>();
            }
            
            public static IList<object> execute(string query, IList<object> parameter)
            {
                return sqlService.OpenConnection(connection =>
                {
                    return connection.Query(query, parameter).ToList();
                });
            }
        }
    }
}
