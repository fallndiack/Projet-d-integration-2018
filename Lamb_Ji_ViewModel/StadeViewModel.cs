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
   public class StadeViewModel
    {
        public int? StadeID { get; set; }
        [Required(ErrorMessage = "Le nom du stade est requis!")]
        [DisplayName("Nom du Stade")]
        public string StadeName { get; set; }
        [DisplayName("Adresse")]
        public string StadeAdresse { get; set; }
        public string imageUrl { get; set; }

        public HttpPostedFileBase ImageUpload { get; set; }

    }
}
