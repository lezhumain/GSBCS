using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication.ViewModel.Trans
{
    public class ColTrans : BaseTrans
    {
        public int matricule { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }

        public ColTrans(int mat, string nom, string prenom)
        {
            this.matricule = mat;
            this.nom = nom;
            this.prenom = prenom;
        }

        public ColTrans(ColTrans c)
        {
            this.matricule = c.matricule;
            this.nom = c.nom;
            this.prenom = c.prenom;
        }

        public override string ToString()
        {
            return "<ColTrans>" + this.prenom + " " + this.nom;
        }

        public string ToCsvRow()
        {
            return this.matricule + ";" + this.nom + ";" + this.prenom + "\n";
        }

        public override string getClass()
        {
            return "WpfApplication.ViewModel.Trans.ColTrans";
        }
    }
}
