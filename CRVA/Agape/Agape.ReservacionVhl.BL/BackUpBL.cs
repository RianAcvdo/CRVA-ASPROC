using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agape.ReservacionVhl.DAL;

namespace Agape.ReservacionVhl.BL
{
    public  class BackUpBL
    {
        public BackUp backUp = new BackUp();

        public string GetDataBase()
        {
            return backUp.GetDatabase();
        }
        public string GetServer()
        {
            return backUp.GetServer();
        }
    }
}
