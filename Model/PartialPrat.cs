using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class PRATICIEN : EntityObject
    {
        public override string ToString()
        {
            return "<PRATICIEN>" + this.prenom_praticien + " " + this.nom_praticien;
        }
    }
}
