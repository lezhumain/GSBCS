using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class RESPONSABLE_DE_SECTEUR : EntityObject
    {
        public override string ToString()
        {
            return "<RESPONSABLE_DE_SECTEUR>" + this.COLLABORATEUR.prenom_col + " " + this.COLLABORATEUR.nom_col;
        }
    }
}
