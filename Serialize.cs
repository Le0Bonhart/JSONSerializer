using System;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace JSONSerializer
{
    public static partial class Serializer
    {
        public static string Serialize(object obj)
        {
            StringBuilder serialized = new("{");
            serialized.Append($" \"class\" : \"{obj.GetType()}\", ");
            FieldInfo[] fields = obj.GetType().GetFields();
            foreach (FieldInfo field in fields)
            {
                dynamic val = field.GetValue(obj);
                if (val is not null)
                    serialized.Append($"\"{field.Name}\" : {Serialize(val)}, ");
                else serialized.Append($"\"{field.Name}\" : null, ");
            }
            serialized = serialized.Remove(serialized.Length - 2, 2);
            serialized.Append("}");

            return serialized.ToString();
        }
        public static string Serialize(IEnumerable array)
        {
            StringBuilder serialized = new("[");
            foreach (dynamic item in array)
                if (item !=  null)
                    serialized.Append($"{Serialize(item)}, ");
                else
                    serialized.Append("null, ");
            if (serialized.ToString().Contains(',')) serialized = serialized.Remove(serialized.Length-2, 2);
            serialized.Append("]");
            return serialized.ToString();
        }

        public static string Serialize(Dictionary<string, object> dict)
        {
            StringBuilder serialized = new("{");
            foreach (string key in dict.Keys)
            {
                dynamic item = dict[key];
                if (item != null)
                    serialized.Append($"\"{key}\" : {Serialize(item)}, ");
                else serialized.Append($"\"{key}\" : null, ");
            }

            serialized = serialized.Remove(serialized.Length - 2, 2);

            serialized.Append("}");
            return serialized.ToString();
        }

        public static string Serialize(string str) { return $"\"{str}\""; }

        public static string Serialize(bool b)
        {
            return b ? "true" : "false";
        }

        public static string Serialize(ValueType value)
        {
            if (!value.GetType().IsPrimitive && !(value.GetType() == typeof(Decimal))) return Serialize((object)value);
            return Convert.ToString(value, CultureInfo.InvariantCulture); ;
        }
    }
}
