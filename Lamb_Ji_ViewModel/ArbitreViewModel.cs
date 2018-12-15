using Lamb_Ji_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lamb_Ji_ViewModel
{
    public class ArbitreViewModel
    {
        public int ArbitreID { get; set; }
        public string ArbitreName { get; set; }
        public string ArbitreEmail { get; set; }
        public Nullable<System.DateTime> ArbitreDateNaissance { get; set; }
        public string imageUrl { get; set; }

        public HttpPostedFileBase ImageUpload { get; set; }


        public virtual ICollection<CombatViewModel> Combats { get; set; }
    }
}
