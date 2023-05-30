using System;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONSerializer
{
    public static partial class Serializer
    {
        public static string Serialize(IEnumerable array)
        {
            string serialized = "[";
            foreach (dynamic item in array)
                serialized = $"{serialized}{Serialize(item)}, ";
            if (serialized.Contains(',')) serialized = serialized.Remove(serialized.Length-2);
            serialized += "]";
            return serialized;
        }

        public static string Serialize(string str) { return $"\"{str}\""; }

        public static string Serialize(ValueType value)
        {
            if (!value.GetType().IsPrimitive && !(value.GetType() == typeof(Decimal))) return Serialize((object)value);
            return Convert.ToString(value, CultureInfo.InvariantCulture); ;
        }
    }
}
