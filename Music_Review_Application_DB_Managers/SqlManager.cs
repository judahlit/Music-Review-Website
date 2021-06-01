using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_DB_Managers
{
    public class SqlManager
    {
        #region Constants and Fields

        public const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MRA_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        #endregion

        public string GetSqlString(string value)
        {
            if (value != null)
            {
                return value.Replace("'", "''");
            }

            return value;
        }
    }
}
