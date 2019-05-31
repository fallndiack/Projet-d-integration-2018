using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamb_Ji_ViewModel
{
    public class SingleActuViewModel
    {
        public int actuID { get; set; }
        public string actuNom { get; set; }
        public string actuTitre { get; set; }
        public string actuTexte { get; set; }
        public string actuImgUrl { get; set; }     
        public Nullable<System.DateTime> actuDate { get; set; }
    }
}
