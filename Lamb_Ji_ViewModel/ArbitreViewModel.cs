using Lamb_Ji_DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lamb_Ji_ViewModel
{
    public class ArbitreViewModel
    {
        public int? ArbitreID { get; set; }
        [Required(ErrorMessage = "Le nom est requis!")]
        [DisplayName("Prénom Nom")]
        public string ArbitreName { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Le champ e-mail doit être complété")]
        [EmailAddress(ErrorMessage = "Veuillez donner un e-mail valide svp!!!")]
        public string ArbitreEmail { get; set; }
        [Display(Name = "Date de Naissance")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "La date de naissance est requise!")]

        public Nullable<System.DateTime> ArbitreDateNaissance { get; set; }
        public string imageUrl { get; set; }

        public HttpPostedFileBase ImageUpload { get; set; }


        public virtual ICollection<Combat> Combats { get; set; }
    }
}
