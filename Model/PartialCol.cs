using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class COLLABORATEUR : EntityObject
    {
        public override string ToString()
        {
            return "<COLLABORATEUR>" + this.prenom_col + " " + this.nom_col;
        }
    }
}
