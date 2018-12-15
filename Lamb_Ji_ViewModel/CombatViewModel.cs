using Lamb_Ji_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamb_Ji_ViewModel
{
    public class CombatViewModel
    {
        public int CombatID { get; set; }
        public string Combat_Description { get; set; }
        public int TypeLutteID { get; set; }
        public int CategorieID { get; set; }
        public int StadeID { get; set; }
        public string Combat_Etat { get; set; }

        public string CategorieName { get; set; }
        public string TypeLutteName { get; set; }
        public string StadeName { get; set; }



        public virtual ICollection<Affiche> Affiches { get; set; }
        public virtual Categorie Categorie { get; set; }
        public virtual Stade Stade { get; set; }
        public virtual TypeLutte TypeLutte { get; set; }
        public virtual ICollection<Arbitre> Arbitres { get; set; }
    }
}
