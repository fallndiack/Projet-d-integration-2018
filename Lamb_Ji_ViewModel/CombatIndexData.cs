using Lamb_Ji_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamb_Ji_ViewModel
{
   public class CombatIndexData
    {
        public IEnumerable<Combat> combats { get; set; }
        public IEnumerable<Arbitre> arbitres { get; set; }
    }
}
