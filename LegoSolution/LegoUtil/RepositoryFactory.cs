using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegoBL.Interfaces;


namespace LegoUtil
{
    public static class RepositoryFactory
    {
        public static ILegoRepository GeefRepository(string databaseType, string connectionsString)
        {
            switch (databaseType)
            {
                case "SQL": return new LegoRepository(connectionsString);
                default: return null;
            }
        }
    }
}
