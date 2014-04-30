using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.helpers
{
    public class RapHelper
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
        public static RapHelper Current
        {
            get{ return Nested.rapHelper; }
        }
        
        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }
            internal static readonly RapHelper rapHelper = new RapHelper();
        }
        // endregion
        
        // region Constructeur
        public RapHelper()
        {
               
        }
        // endregion
        // region Fonctions publiques utiles
        public List<RAPPORT_DE_VISITE> GetList()
		{
            using (_db =  new BDD_SIO7Entities())
		    {
                return _db.RAPPORT_DE_VISITE.ToList();
            }
        }
        /*
        public string getRedacName()
        {
            using (_db = new BDD_SIO7Entities())
            {
                return _db.RAPPORT_DE_VISITE.ToList();
            }
        }
        */
        /*
        public List<RAPPORT_DE_VISITE> GetViewList()
        {
        }
         */

        public void Insert(RAPPORT_DE_VISITE rapport)
		{
            using (_db = new BDD_SIO7Entities())
    		{
                _db.AddToRAPPORT_DE_VISITE(rapport);
                _db.SaveChanges();
            }
        }

        public void Update(RAPPORT_DE_VISITE rapport)
		{
            using (_db = new BDD_SIO7Entities())
            {
                _db.RAPPORT_DE_VISITE.Attach(rapport);
                _db.ObjectStateManager.ChangeObjectState(rapport, System.Data.EntityState.Modified);
                _db.SaveChanges();
            }
            /*
            using (_db = new BDD_SIO7Entities())
			{
                _db.Refresh(System.Data.Objects.RefreshMode.StoreWins, rapport);
                _db.SaveChanges();
            }
            */
        }

        public void Delete(RAPPORT_DE_VISITE rapport)
		{
            using (_db = new BDD_SIO7Entities())
		    {
                _db.DeleteObject(rapport);
                _db.SaveChanges();
            }
        }
        /*
        public void DeleteCascade(COLLABORATEUR rapport)
        {
            using (_db = new BDD_SIO7Entities())
	        {
		        _db.COLLABORATEUR.Attach(rapport);

		        //cascade
                List<COLLABORATEURCertifies> pc = rapport.COLLABORATEURCertifie.ToList();
		        foreach(COLLABORATEURCertifie pp in pc)
		        {
			        _db.DeleteObject(pp)
		        }
                
		        _db.DeleteObject(rapport);
		        _db.SaveChanges();
	        }
        }
        */
        public List<RAPPORT_DE_VISITE> getListByRegion(string codeReg)
        {
            using (_db = new BDD_SIO7Entities())
	        {
		        return 	(from rap in _db.RAPPORT_DE_VISITE
                            .Include("COLLABORATEUR")
                            .Include("PRATICIEN")
                            .Include("COLLABORATEUR.VISITEUR")
                            .Include("MOTIF")
				        where rap.COLLABORATEUR.VISITEUR.code_region == codeReg
				        select rap).ToList<RAPPORT_DE_VISITE>(); 
	        }
        }

        public List<RAPPORT_DE_VISITE> getListByRegion(REGION reg)
        {
            return getListByRegion(reg.code_region);
        }


        // endregion
      }
}
