using DAO.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Orchid
{
    public class OrchidDAO : GenericDAO<BussinessObjects.Models.Orchid>, IOrchidDAO
    {
        private static OrchidDAO instance = null;

        public static OrchidDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrchidDAO();
                }
                return instance;
            }
        }


    }
}
