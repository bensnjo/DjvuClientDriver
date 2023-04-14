using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace djvu.models
{
    internal class Configs
    {
       public string initiolization_api = "/initializer/selectInitInfo";
        public string initiolization_port = "8088";
        public string initiolization_endponint;

        public Configs()
        {
            initiolization_endponint = initiolization_port + initiolization_api;
        }
    }
}
