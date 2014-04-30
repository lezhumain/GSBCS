using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.helpers
{
    public class RegHelper
    {
        // region Fields
        private BDD_SIO7Entities _db;
        // endregion

        # region Thread-safe, lazy Singleton
        /// <summary>
        /// This is a thread-safe, lazy singleton. See
        /// http://www.yoda.arachsys.com/csharp/singleton.html
        /// for more details about its implementation.
        /// </summary>
        public static RegHelper Current
        {
            get{ return Nested.Helper1; }
        }
        
        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }
            internal static readonly RegHelper Helper1 = new RegHelper();
        }
        # endregion
        
        # region Constructeur
        public RegHelper()
        {
               
        }
        # endregion
        
        # region Fonctions publiques utiles
        public List<REGION> GetList()
		{
            using (_db =  new BDD_SIO7Entities())
		    {
                return _db.REGION.ToList();
            }
        }

        public REGION GetOneByName(string name)
        {
            using (_db = new BDD_SIO7Entities())
            {
                return (from c in _db.REGION
                            where c.nom_region == name
                            select c).FirstOrDefault();
            }
        }
        # endregion
      }
}
