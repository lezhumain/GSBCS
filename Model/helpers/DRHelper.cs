using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.helpers
{
    public class DRHelper
    {
        // region Fields
        private BDD_SIO7Entities _db;
        // endregion

        // region Thread-safe, lazy Singleton
        /// <summary>
        /// This is a thread-safe, lazy singleton. See
        /// http://www.yoda.arachsys.com/csharp/singleton.html
        /// for more details about its implementation.
        /// </summary>
        public static DRHelper Current
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
            internal static readonly DRHelper Helper1 = new DRHelper();
        }
        // endregion
        
        // region Constructeur
        public DRHelper()
        {
               
        }

        // endregion
        // region Fonctions publiques utiles
        public List<DIRECTEUR_REGIONAL> GetList()
		{
            using (_db =  new BDD_SIO7Entities())
		    {
                return _db.DIRECTEUR_REGIONAL.ToList();
            }
        }

        public void Insert(DIRECTEUR_REGIONAL DIRECTEUR_REGIONAL)
		{
            using (_db = new BDD_SIO7Entities())
    		{
                _db.AddToDIRECTEUR_REGIONAL(DIRECTEUR_REGIONAL);
                _db.SaveChanges();
            }
        }

        public void Update(DIRECTEUR_REGIONAL DIRECTEUR_REGIONAL)
		{
            using (_db = new BDD_SIO7Entities())
            {
                _db.DIRECTEUR_REGIONAL.Attach(DIRECTEUR_REGIONAL);
                _db.ObjectStateManager.ChangeObjectState(DIRECTEUR_REGIONAL, System.Data.EntityState.Modified);
                _db.SaveChanges();
            }
            /*
            using (_db = new BDD_SIO7Entities())
			{
                _db.Refresh(System.Data.Objects.RefreshMode.StoreWins, DIRECTEUR_REGIONAL);
                _db.SaveChanges();
            }
            */
        }

        public void Delete(DIRECTEUR_REGIONAL DIRECTEUR_REGIONAL)
		{
            using (_db = new BDD_SIO7Entities())
		    {
                _db.DeleteObject(DIRECTEUR_REGIONAL);
                _db.SaveChanges();
            }
        }
        /*
        public void DeleteCascade(DIRECTEUR_REGIONAL DIRECTEUR_REGIONAL)
        {
            using (_db = new BDD_SIO7Entities())
	        {
		        _db.DIRECTEUR_REGIONAL.Attach(DIRECTEUR_REGIONAL);

		        //cascade
                List<DIRECTEUR_REGIONALCertifies> pc = DIRECTEUR_REGIONAL.DIRECTEUR_REGIONALCertifie.ToList();
		        foreach(DIRECTEUR_REGIONALCertifie pp in pc)
		        {
			        _db.DeleteObject(pp)
		        }
                
		        _db.DeleteObject(DIRECTEUR_REGIONAL);
		        _db.SaveChanges();
	        }
        }
        */

        public DIRECTEUR_REGIONAL getItemFromId(int id)
        {
            using (_db = new BDD_SIO7Entities())
	        {
		        return 	(from profils in _db.DIRECTEUR_REGIONAL
				        where profils.matricule_col_dir == id
				        select profils).FirstOrDefault(); 
	        }
        }

        public bool isDR(int id)
        {
            using (_db = new BDD_SIO7Entities())
            {
                var v = (from rds in _db.DIRECTEUR_REGIONAL
                         where rds.matricule_col_dir == id
                         select rds).FirstOrDefault();

                if (v == null)
                    return false;
                else
                    return true;
            }
        }
        


        // endregion
      }
}
