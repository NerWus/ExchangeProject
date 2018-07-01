using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication3
{
    class FileRow
    {
        public string TICKER { get; set; }
        public string DTYYYYMMDD { get; set; }
        public double OPEN { get; set; }
        public double HIGH { get; set; }
        public double LOW { get; set; }
        public double CLOSE { get; set; }
        public int VOL { get; set; }
    }
}
