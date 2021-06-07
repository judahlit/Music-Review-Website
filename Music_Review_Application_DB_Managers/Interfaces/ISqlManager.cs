using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_DB_Managers.Interfaces
{
    public interface ISqlManager
    {
        string GetSqlString(string value);
    }
}
