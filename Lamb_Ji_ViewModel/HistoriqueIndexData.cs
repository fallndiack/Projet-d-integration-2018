using Lamb_Ji_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamb_Ji_ViewModel
{
   public class HistoriqueIndexData
    {
        public IEnumerable<Lutteur> lutteurs { get; set; }
        public IEnumerable<Affiche> affiches { get; set; }
    }
}
