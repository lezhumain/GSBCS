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

        public RapTrans(int mat, string prat, string date)
        {
            this.numero = mat;
            this.praticien = prat;
            this.date = date;
        }

        public override string ToString()
        {
            return "<RapTrans>n°" + this.numero + " prat:" + this.praticien + " date:" + this.date;
        }

        public string ToCsvRow()
        {
            return this.numero + ";" + this.praticien + ";" + this.date + "\n";
        }

        public override string getClass()
        {
            return "WpfApplication.ViewModel.Trans.RapTrans";
        }
    }
}
