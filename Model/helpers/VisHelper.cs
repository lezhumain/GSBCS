using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.helpers
{
    public class VisHelper
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
        public static VisHelper Current
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
            internal static readonly VisHelper Helper1 = new VisHelper();
        }
        // endregion
        
        // region Constructeur
        public VisHelper()
        {
               
        }

        // endregion
        // region Fonctions publiques utiles
        public List<VISITEUR> GetList()
		{
            using (_db =  new BDD_SIO7Entities())
		    {
                return _db.VISITEUR.ToList();
            }
        }
        /*
        public VISITEUR GetOneByUsername(string username)
        {
            using (_db = new BDD_SIO7Entities())
            {
                string nom, prenom;
                string[] uname = username.Split('.');

                if (uname.Length != 2)
                    throw new Exception("Invalid username");
        
                prenom = uname[0];
                nom = uname[1];

                return (from c in _db.VISITEUR
                                .Include("COLLABORATEUR")
                        where c.nom_col == nom &&
                              c.prenom_col == prenom
                        select c).FirstOrDefault();
            }
        }

        public VISITEUR GetOneByUsername1(string username)
        {
            using (_db = new BDD_SIO7Entities())
            {
                string nom, prenom;
                string[] uname = username.Split('.');

                if (uname.Length != 2)
                    throw new Exception("Invalid username");

                prenom = uname[0];
                nom = uname[1];

                return (from c in _db.VISITEUR
                        join od in _db.VISITEUR on c.matricule_col equals od.matricule_col_vis
                        where c.nom_col == nom &&
                              c.prenom_col == prenom
                        select c).FirstOrDefault();
            }
        }
        */
        public void Insert(VISITEUR collaborateur)
		{
            using (_db = new BDD_SIO7Entities())
    		{
                _db.AddToVISITEUR(collaborateur);
                _db.SaveChanges();
            }
        }

        public void Update(VISITEUR collaborateur)
		{
            using (_db = new BDD_SIO7Entities())
            {
                _db.VISITEUR.Attach(collaborateur);
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

        public void Delete(VISITEUR collaborateur)
		{
            using (_db = new BDD_SIO7Entities())
		    {
                _db.DeleteObject(collaborateur);
                _db.SaveChanges();
            }
        }
        /*
        public void DeleteCascade(VISITEUR collaborateur)
        {
            using (_db = new BDD_SIO7Entities())
	        {
		        _db.VISITEUR.Attach(collaborateur);

		        //cascade
                List<VISITEURCertifies> pc = collaborateur.VISITEURCertifie.ToList();
		        foreach(VISITEURCertifie pp in pc)
		        {
			        _db.DeleteObject(pp)
		        }
                
		        _db.DeleteObject(collaborateur);
		        _db.SaveChanges();
	        }
        }
        */

        public List<VISITEUR> GetListForChart()
        {
            using (_db = new BDD_SIO7Entities())
            {
                return (from c in _db.VISITEUR
                            .Include("COLLABORATEUR")
                            .Include("COLLABORATEUR.RAPPORT_DE_VISITE")
                        orderby c.COLLABORATEUR.RAPPORT_DE_VISITE.Count()
                        select c).Take(5).ToList<VISITEUR>();
            }
        }

        public List<VISITEUR> getListByRegion(string codeReg)
        {
            using (_db = new BDD_SIO7Entities())
	        {
		        return 	(from vis in _db.VISITEUR
                            .Include("COLLABORATEUR")
                            .Include("REGION")
                            .Include("COLLABORATEUR.RAPPORT_DE_VISITE")
				        where vis.code_region == codeReg
				        select vis).ToList<VISITEUR>(); 
	        }
        }

        public List<VISITEUR> getListByRegion(REGION reg)
        {
            return getListByRegion(reg.code_region);
        }
        
        // endregion
      }
}
