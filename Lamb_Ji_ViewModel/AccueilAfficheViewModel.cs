using Lamb_Ji_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamb_Ji_ViewModel
{
    public class AccueilAfficheViewModel
    {
        public AccueilAfficheViewModel()
        {
            ListeAffiches = new List<AfficheAvecAvisDto>();
        }
        public List<AfficheAvecAvisDto> ListeAffiches { get; set; }
    }
}
