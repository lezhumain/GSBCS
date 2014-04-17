using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.helpers
{
    public class ColHelper
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
        public static ColHelper Current
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
            internal static readonly ColHelper Helper1 = new ColHelper();
        }
        // endregion
        
        // region Constructeur
        public ColHelper()
        {
               
        }

        // endregion
        // region Fonctions publiques utiles
        public List<COLLABORATEUR> GetList()
		{
            using (_db =  new BDD_SIO7Entities())
		    {
                return _db.COLLABORATEUR.ToList();
            }
        }

        public COLLABORATEUR GetOneByUsername(string username)
        {
            using (_db = new BDD_SIO7Entities())
            {
                string nom, prenom;
                string[] uname = username.Split('.');

                if (uname.Length != 2)
                    throw new Exception("Invalid username");
        
                prenom = uname[0];
                nom = uname[1];

                return (from c in _db.COLLABORATEUR
                            where c.nom_col == nom &&
                                  c.prenom_col == prenom
                            select c).FirstOrDefault();
            }
        }

        public void Insert(COLLABORATEUR collaborateur)
		{
            using (_db = new BDD_SIO7Entities())
    		{
                _db.AddToCOLLABORATEUR(collaborateur);
                _db.SaveChanges();
            }
        }

        public void Update(COLLABORATEUR collaborateur)
		{
            using (_db = new BDD_SIO7Entities())
            {
                _db.COLLABORATEUR.Attach(collaborateur);
                _db.ObjectStateManager.ChangeObjectState(collaborateur, System.Data.EntityState.Modified);
                _db.SaveChanges();
            }
            /*
            using (_db = new BDD_SIO7Entities())
			{
                _db.Refresh(System.Data.Objects.RefreshMode.StoreWins, collaborateur);
                _db.SaveChanges();
            }
            */
        }

        public void Delete(COLLABORATEUR collaborateur)
		{
            using (_db = new BDD_SIO7Entities())
		    {
                _db.DeleteObject(collaborateur);
                _db.SaveChanges();
            }
        }
        /*
        public void DeleteCascade(COLLABORATEUR collaborateur)
        {
            using (_db = new BDD_SIO7Entities())
	        {
		        _db.COLLABORATEUR.Attach(collaborateur);

		        //cascade
                List<COLLABORATEURCertifies> pc = collaborateur.COLLABORATEURCertifie.ToList();
		        foreach(COLLABORATEURCertifie pp in pc)
		        {
			        _db.DeleteObject(pp)
		        }
                
		        _db.DeleteObject(collaborateur);
		        _db.SaveChanges();
	        }
        }
        
        public getItemFromId(int id)
        {
	        using(_db = new BDDContext())
	        {
		        return 	(from profils in _db.Profil
				        where profils.id == id
				        select profils).FirstOnDefault(); 
	        }
        }
        */


        // endregion
      }
}
