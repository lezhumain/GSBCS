using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication.ViewModel.Trans
{
    public class RapTrans : BaseTrans
    {
        public int numero { get; set; }
        public string praticien { get; set; }
        public string date { get; set; }
        public string visiteur { get; set; }

        public RapTrans(int mat, string prat, string date, string visiteur)
        {
            this.numero = mat;
            this.praticien = prat;
            this.date = date;
            this.visiteur = visiteur;
        }

        public override string ToString()
        {
            return "<RapTrans>n°" + this.numero + " prat:" + this.praticien + " date:" + this.date;
        }

        /// <summary>
        /// Retourne une representation de l'instance sous forme
        /// d'une string, ligne d'un fichier CSV.
        /// Format: "col1;col2;col2\n"
        /// </summary>
        /// <returns>String, separee par des ';' et terminee par '\n'</returns>
        public string ToCsvRow()
        {
            return this.numero + ";" + this.praticien + ";" + this.date + ";" + this.visiteur + "\n";
        }

        public override string getClass()
        {
            return "WpfApplication.ViewModel.Trans.RapTrans";
        }
    }
}
