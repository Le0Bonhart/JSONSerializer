using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONSerializer
{
    public class jsonObj
    {
        public Dictionary<string, object> _data = new Dictionary<string, object>();
        public Dictionary<string, object> Data { get { return _data; } }
        public jsonObj() { }
    }
}
