//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lamb_Ji_DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Affiche
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Affiche()
        {
            this.AvisAffiches = new HashSet<AvisAffiche>();
        }
    
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
        public string imageUrl { get; set; }
        public double Note { get; set; }

        public virtual Lutteur Lutteur { get; set; }
        public virtual Lutteur Lutteur1 { get; set; }
        public virtual Combat Combat { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AvisAffiche> AvisAffiches { get; set; }
    }
}
