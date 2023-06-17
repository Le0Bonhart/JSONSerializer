using System.Collections;
using System.Text;

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

        public static JsonObj DeserializeObj(string json)
        {
            JsonObj obj = new JsonObj();
            if (!json.Contains('\"')) return obj;
            json = DeserealizerFormat(json);
            int LPointer = json.IndexOf('\"', 0);
            int MPointer = json.IndexOf(':', LPointer);
            int RPointer = json.Contains(';') ? json.IndexOf(';', MPointer + 1) : json.IndexOf('}');
            while (true)
            {
                obj.Data.Add((string)Deserialize(json.Substring(LPointer, MPointer - LPointer)), Deserialize(json.Substring(MPointer + 1, RPointer - MPointer - 1)));
                if (RPointer == json.LastIndexOf('}')) break;
                LPointer = json.IndexOf('\"', RPointer);
                MPointer = json.IndexOf(":", LPointer);
                if (json[MPointer + 1] == '{') RPointer = Closer(json, MPointer + 1) + 1;
                else if (json[MPointer + 1] == '[') RPointer = Closer(json, MPointer + 1) + 1;
                else RPointer = RPointer == json.LastIndexOf(';') ? json.LastIndexOf('}') : json.IndexOf(';', MPointer);
            }
            return obj;
        }

        public static ArrayList DeserializeArray(string json)
        {
            ArrayList list = new ArrayList();
            json = DeserealizerFormat(json);
            int LPointer = 0;
            int RPointer = json.IndexOf(';');
            while (true)
            {
                list.Add(Deserialize(json.Substring(LPointer + 1, RPointer - LPointer - 1)));
                if (RPointer == json.LastIndexOf(']')) break;
                LPointer = json.IndexOf(";", RPointer);
                if (json[LPointer + 1] == '[') RPointer = Closer(json, LPointer + 1);
                else RPointer = RPointer == json.LastIndexOf(';') ? json.IndexOf(']') : json.IndexOf(';', LPointer + 1);
            }
            return list;
        }

        public static dynamic? Deserialize(string json)
        {
            return json[0] switch
            {
                ('{') => DeserializeObj(json),
                ('\"') => json.Substring(1, json.Length - 2),
                ('[') => DeserializeArray(json),
                ('n') => null,
                ('t') => true,
                ('f') => false,
                _ => double.Parse(json, System.Globalization.CultureInfo.InvariantCulture),
            };
        }

        static int Closer(string str, int ind)
        {
            char bracket = str[ind];
            char want = bracket == '{' ? '}' : ']';
            int counter = 1;

            for (int i = ind + 1; ;i++)
            {
                if (str[i] == bracket) counter++;
                else if (str[i] == want) counter--;
                if (counter == 0) return i;
            }
        }
    }
}
