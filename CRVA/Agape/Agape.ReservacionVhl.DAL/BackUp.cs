using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agape.ReservacionVhl.DAL
{
    public class BackUp
    {
        DBCommon db = new DBCommon();
        public string GetDatabase()
        {
            return db.Database.Connection.Database;
        }
        public string GetServer()
        {
            return db.Database.Connection.DataSource;
        }
    }
}
