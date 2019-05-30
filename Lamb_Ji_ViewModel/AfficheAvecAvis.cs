using Lamb_Ji_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamb_Ji_ViewModel
{
    public class AfficheAvecAvis
    {
        public AfficheAvecAvis()
        {
            Avis = new List<AvisAffiche>();
        }
        public int AfficheID { get; set; }
        public string AfficheNom { get; set; }
        public int CombatID { get; set; }
        public int Lutteur_A { get; set; }
        public int Lutteru_B { get; set; }
        public Nullable<System.DateTime> DateCombat { get; set; }
        public string Vaincqueur { get; set; }
        public string imageUrl { get; set; }

        public virtual List<AvisAffiche> Avis { get; set; }
        public virtual Lutteur Lutteur { get; set; }
        public virtual Lutteur Lutteur1 { get; set; }
        public virtual Combat Combat { get; set; }
    }
}
