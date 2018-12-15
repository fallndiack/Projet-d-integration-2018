using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamb_Ji_ViewModel
{
   public class ClubViewModel
    {
        public int ClubID { get; set; }
        public string ClubName { get; set; }
        public string ClubAdresse { get; set; }
        public string ClubEmail { get; set; }
        public DateTime ClubDateCreation { get; set; }

        public string imageUrl { get; set; }
    }
}
