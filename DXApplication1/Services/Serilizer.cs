using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication1.Services
{
    public class Serilizer<T>
    {
        public static T JsonSerilizeData(string data)
        {
            TextReader sr = new StringReader(data);
            var ser = JsonSerializer.Create();
            JsonReader reader = new JsonTextReader(sr);

            T mm = ser.Deserialize<T>(reader);
            return mm;
        }
        public static T DeserilizeData(T data)
        {
            TextWriter textWriter = new StringWriter();
            var ser = JsonSerializer.Create();
            JsonWriter writer = new JsonTextWriter(textWriter);
            return data;
        }
    }
}
