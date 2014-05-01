using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.helpers
{
    public class RdSHelper
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
        public static RdSHelper Current
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
            internal static readonly RdSHelper Helper1 = new RdSHelper();
        }
        // endregion
        
        // region Constructeur
        public RdSHelper()
        {
               
        }

        // endregion
        // region Fonctions publiques utiles
        public List<RESPONSABLE_DE_SECTEUR> GetList()
		{
            using (_db =  new BDD_SIO7Entities())
		    {
                return _db.RESPONSABLE_DE_SECTEUR.ToList();
            }
        }

        public void Insert(RESPONSABLE_DE_SECTEUR RESPONSABLE_DE_SECTEUR)
		{
            using (_db = new BDD_SIO7Entities())
    		{
                _db.AddToRESPONSABLE_DE_SECTEUR(RESPONSABLE_DE_SECTEUR);
                _db.SaveChanges();
            }
        }

        public void Update(RESPONSABLE_DE_SECTEUR RESPONSABLE_DE_SECTEUR)
		{
            using (_db = new BDD_SIO7Entities())
            {
                _db.RESPONSABLE_DE_SECTEUR.Attach(RESPONSABLE_DE_SECTEUR);
                _db.ObjectStateManager.ChangeObjectState(RESPONSABLE_DE_SECTEUR, System.Data.EntityState.Modified);
                _db.SaveChanges();
            }
        }

        public void Delete(RESPONSABLE_DE_SECTEUR RESPONSABLE_DE_SECTEUR)
		{
            using (_db = new BDD_SIO7Entities())
		    {
                _db.DeleteObject(RESPONSABLE_DE_SECTEUR);
                _db.SaveChanges();
            }
        }
        /*
        public void DeleteCascade(RESPONSABLE_DE_SECTEUR RESPONSABLE_DE_SECTEUR)
        {
            using (_db = new BDD_SIO7Entities())
	        {
		        _db.RESPONSABLE_DE_SECTEUR.Attach(RESPONSABLE_DE_SECTEUR);

		        //cascade
                List<RESPONSABLE_DE_SECTEURCertifies> pc = RESPONSABLE_DE_SECTEUR.RESPONSABLE_DE_SECTEURCertifie.ToList();
		        foreach(RESPONSABLE_DE_SECTEURCertifie pp in pc)
		        {
			        _db.DeleteObject(pp)
		        }
                
		        _db.DeleteObject(RESPONSABLE_DE_SECTEUR);
		        _db.SaveChanges();
	        }
        }
        */

        public RESPONSABLE_DE_SECTEUR getItemFromId(int id)
        {
            using (_db = new BDD_SIO7Entities())
	        {
		        return 	(from rds in _db.RESPONSABLE_DE_SECTEUR
				        where rds.matricule_col_res == id
				        select rds).FirstOrDefault(); 
	        }
        }

        public bool isRdS(int id)
        {
            using (_db = new BDD_SIO7Entities())
            {
                var v = (from rds in _db.RESPONSABLE_DE_SECTEUR
                        where rds.matricule_col_res == id
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
