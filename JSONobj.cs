using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONSerializer
{
    public class JSONobj
    {
        Dictionary<string, object> _data = new Dictionary<string, object>();

        public JSONobj() { }

        public void PushField(string field, object obj)
        {
            _data.Add(field, obj);
        }
    }
}
