using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication.ViewModel.Trans
{
    public class PratTrans : BaseTrans
    {
        public int matricule { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }

        public PratTrans(int mat, string nom, string prenom)
        {
            this.matricule = mat;
            this.nom = nom;
            this.prenom = prenom;
        }

        public override string ToString()
        {
            return "<PratTrans>" + this.prenom + " " + this.nom;
        }

        public string ToCsvRow()
        {
            return this.matricule + ";" + this.nom + ";" + this.prenom + "\n";
        }

        public override string getClass()
        {
            return "WpfApplication.ViewModel.Trans.PratTrans";
        }
    }
}
