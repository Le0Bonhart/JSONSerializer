using System;
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
            string serialized = "{";
            serialized = $"{serialized} \"class\" : \"{obj.GetType()}\", ";
            FieldInfo[] fields = obj.GetType().GetFields();
             foreach (FieldInfo field in fields)
            {
                dynamic val = field.GetValue(obj);
                serialized += $"\"{field.Name}\" : {Serialize(val)}, ";
            }
            serialized = serialized.Remove(serialized.Length - 2);
            serialized += "}";

            return serialized;
        }
    }
}