using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenetikAlgoritma.Model
{
    internal class Kromozom
    {
        public int KromozomNo { get; set; }
        public List<Hucre> Hucreler { get; set; }
        public List<Urun> Urunler { get; set; }
        public double Fitness { get; set; }
        public double RassalDeger { get; set; }
    }
}
