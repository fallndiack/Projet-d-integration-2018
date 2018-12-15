using Lamb_Ji_DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lamb_Ji_ViewModel
{
    public class LutteurViewModel
    {
        public LutteurViewModel()
        {

            //imageUrl = "~/Images/Image-Lutteur/default.png";
        }

        public int? LutteurID { get; set; }
        public Nullable<int> LutteurClubID { get; set; }
        [Required(ErrorMessage = "Le nom est requis!")]
        [DisplayName("Prénom Nom")]
        public string LutteurName { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Le champ e-mail doit être complété")]
        [EmailAddress(ErrorMessage = "Veuillez donner un e-mail valide svp!!!")]
        public string LutteurEmail { get; set; }
        [Required(ErrorMessage = "Le poids est requis!")]
        [Range(60, 250)]
        [DisplayName("Poids")]
        public int LutteurPoids { get; set; }
        //[Required(ErrorMessage = "La date est requise!..") ]
        [Display(Name = "Date de Naissance")]
        //[DataType(DataType.Date, ErrorMessage = "Invalid Date Format")]
        //[DisplayFormat(DataFormatString = "dd-mm-yyyy")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        //[Range(typeof(DateTime), "1975-01-01", "2002-12-31",
        //ErrorMessage = "La date {0} doit etre compris entre {1} et {2}")]
        public System.DateTime LutteurDateNaissance { get; set; }
        [DisplayName("Adresse")]
        public string LutteurAddresse { get; set; }
        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        [DisplayName("Description")]
        public string LutteurDescription { get; set; }
        [DisplayName("Telephone")]
        public string LutteurTelephone { get; set; }
        [DisplayName("Image")]
        public string imageUrl { get; set; }

        [DisplayName("Club")]
        public string ClubName { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }


        public virtual ICollection<Affiche> Affiches { get; set; }
        public virtual ICollection<Affiche> Affiches1 { get; set; }
        public virtual Club Club { get; set; }
        public virtual ICollection<Licence> Licences { get; set; }
    }
}
