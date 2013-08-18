using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screen.Vc.Model
{
    //
    // NOTE: We are currently focused only on US customers and hence our models are very US specific.
    // TODO: Upgrade to Non us models at a later stage when we grow.
    //
    public class Address
    {
        public int BlockNumber { get; set; }
        public int Street { get; set; }
        public string Apt { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode5 { get; set; }
        public int? ZipCodeSub4 { get; set; }
    }
}
