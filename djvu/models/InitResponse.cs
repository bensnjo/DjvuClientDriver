using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace djvu.models
{
    internal class InitResponse
    {
        public string resultCd { get; set; }
        public string resultMsg { get; set; }
        public object resultDt { get; set; }
        public object data { get; set; }
    }
}
