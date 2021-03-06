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
    
    public partial class Lutteur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lutteur()
        {
            this.Licences = new HashSet<Licence>();
            this.Affiches = new HashSet<Affiche>();
            this.Affiches1 = new HashSet<Affiche>();
        }
    
        public int LutteurID { get; set; }
        public Nullable<int> LutteurClubID { get; set; }
        public string LutteurName { get; set; }
        public string LutteurEmail { get; set; }
        public int LutteurPoids { get; set; }
        public System.DateTime LutteurDateNaissance { get; set; }
        public string LutteurAddresse { get; set; }
        public string LutteurDescription { get; set; }
        public string LutteurTelephone { get; set; }
        public string imageUrl { get; set; }
    
        public virtual Club Club { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Licence> Licences { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Affiche> Affiches { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Affiche> Affiches1 { get; set; }
    }
}
