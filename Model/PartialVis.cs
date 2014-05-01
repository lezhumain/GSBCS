using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class VISITEUR : EntityObject
    {
        public override string ToString()
        {
            return "<VISITEUR> " + this.COLLABORATEUR.prenom_col + " " + this.COLLABORATEUR.nom_col;//this.matricule_col_vis.ToString();
        }
    }
}
