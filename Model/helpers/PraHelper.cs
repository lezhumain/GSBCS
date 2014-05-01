using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.helpers
{
    public class PraHelper
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
        public static PraHelper Current
        {
            get{ return Nested.praHelper; }
        }
        
        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }
            internal static readonly PraHelper praHelper = new PraHelper();
        }
        // endregion
        
        // region Constructeur
        public PraHelper()
        {
        }
        // endregion

        // region Fonctions publiques utiles
        public List<PRATICIEN> GetList()
		{
            using (_db =  new BDD_SIO7Entities())
		    {
                return _db.PRATICIEN.ToList();
            }
        }

        public void Insert(PRATICIEN praticien)
		{
            using (_db = new BDD_SIO7Entities())
    		{
                _db.AddToPRATICIEN(praticien);
                _db.SaveChanges();
            }
        }

        public void Update(PRATICIEN praticien)
		{
            using (_db = new BDD_SIO7Entities())
            {
                _db.PRATICIEN.Attach(praticien);
                _db.ObjectStateManager.ChangeObjectState(praticien, System.Data.EntityState.Modified);
                _db.SaveChanges();
            }
            /*
            using (_db = new BDD_SIO7Entities())
			{
                _db.Refresh(System.Data.Objects.RefreshMode.StoreWins, praticien);
                _db.SaveChanges();
            }
            */
        }

        public void Delete(PRATICIEN praticien)
		{
            using (_db = new BDD_SIO7Entities())
		    {
                _db.DeleteObject(praticien);
                _db.SaveChanges();
            }
        }
        /*
        public void DeleteCascade(PRATICIEN praticien)
        {
            using (_db = new BDD_SIO7Entities())
	        {
		        _db.PRATICIEN.Attach(praticien);

		        //cascade
                List<PRATICIENCertifies> pc = praticien.PRATICIENCertifie.ToList();
		        foreach(PRATICIENCertifie pp in pc)
		        {
			        _db.DeleteObject(pp)
		        }
                
		        _db.DeleteObject(praticien);
		        _db.SaveChanges();
	        }
        }
        */
        public PRATICIEN getById(int id)
        {
	        using(_db = new BDD_SIO7Entities())
	        {
		        return 	(from prat in _db.PRATICIEN
                        .Include("RAPPORT_DE_VISITE")
				        where prat.matricule_praticien == id
				        select prat).FirstOrDefault();
 
	        }
        }


        // endregion
      }
}
