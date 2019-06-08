using Lamb_Ji_DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamb_Ji_ViewModel
{
    public class AfficheViewModel
    {
        public int AfficheID { get; set; }
        [Required(ErrorMessage = "Le nom est requis!")]
        [DisplayName("Nom")]
        public string AfficheNom { get; set; }

        [DisplayName("Description")]
        public int CombatID { get; set; }
        public int Lutteur_A { get; set; }
        public int Lutteru_B { get; set; }
        public Nullable<System.DateTime> DateCombat { get; set; }
        public string Vaincqueur { get; set; }
        [DisplayName("Image")]
        public string imageUrl { get; set; }
        public double Note { get; set; }

        public virtual Lutteur Lutteur { get; set; }
        public virtual Lutteur Lutteur1 { get; set; }
        public virtual Combat Combat { get; set; }
        public virtual ICollection<AvisAffiche> AvisAffiches { get; set; }
    }
}
