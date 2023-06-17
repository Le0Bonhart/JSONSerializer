namespace JSONSerializer
{
    public class JsonObj
    {
        Dictionary<string, object> _data = new Dictionary<string, object>();
        public Dictionary<string, object> Data { get { return _data; } }
        public JsonObj() { }
    }
}
