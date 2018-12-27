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
   public class ClubViewModel
    {
        public int? ClubID { get; set; }
        [Required(ErrorMessage = "Le nom du club est requis!")]
        [DisplayName("Nom du Club")]
        public string ClubName { get; set; }
        [DisplayName("Adresse")]
        public string ClubAdresse { get; set; }
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Veuillez donner un e-mail valide svp!!!")]
        public string ClubEmail { get; set; }
        [DisplayName("Date de création")]
        public DateTime ClubDateCreation { get; set; }

        public string imageUrl { get; set; }

        public HttpPostedFileBase ImageUpload { get; set; }

    }
}
