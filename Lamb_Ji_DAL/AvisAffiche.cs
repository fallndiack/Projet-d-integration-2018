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
    
    public partial class AvisAffiche
    {
        public int AvisAfficheID { get; set; }
        public string Auteur { get; set; }
        public string Message { get; set; }
        public double note { get; set; }
        public System.DateTime DateAvis { get; set; }
        public int AfficheID { get; set; }
    
        public virtual Affiche Affiche { get; set; }
    }
}
