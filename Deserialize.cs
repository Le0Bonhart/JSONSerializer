using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JSONSerializer
{
    public static partial class Serializer
    {
        public static string DeserealizerFormat(string json)
        {
            StringBuilder sb = new StringBuilder();
            bool InString = false;
            foreach (char c in json)
            {
                if (c == '\"')
                {
                    InString = !InString;
                    sb.Append(c);
                }
                else if (InString) sb.Append(c);
                else if (!InString && !Char.IsWhiteSpace(c))
                {
                    char c2 = c == ',' ? ';' : c;
                    sb.Append(c2);
                }
            }
            return sb.ToString();
        }

        public static jsonObj DeserializeObj(string json)
        {
            jsonObj obj = new jsonObj();
            if (!json.Contains('\"')) return obj;
            json = DeserealizerFormat(json);
            int LPointer = json.IndexOf('\"', 0);
            int MPointer = json.IndexOf(':', LPointer);
            int RPointer = json.Contains(';') ? json.IndexOf(';', MPointer + 1) : json.IndexOf('}');
            while (true)
            {
                obj._data.Add((string)Deserialize(json.Substring(LPointer, MPointer - LPointer)), Deserialize(json.Substring(MPointer + 1, RPointer - MPointer - 1)));
                if (RPointer == json.IndexOf('}')) break;
                LPointer = json.IndexOf('\"', RPointer);
                MPointer = json.IndexOf(":", LPointer);
                RPointer = RPointer == json.LastIndexOf(';') ? json.IndexOf('}') : json.IndexOf(';', MPointer);
            }
            return obj;
        }

        public static object Deserialize(string json)
        {
            if (json[0] == '{') return DeserializeObj(json);
            if (json[0] == '\"') return json.Substring(1, json.Length - 2);
            if (json[0] == 'n') return null;
            if (json[0] == 't') return true;
            if (json[0] == 'f') return false;
            return double.Parse(json, System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
