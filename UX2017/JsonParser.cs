using System.Collections.Generic;
using Newtonsoft.Json;
using UX2017.Models;

namespace UX2017
{
    public class JsonParser
    {
        public IEnumerable<T> Parse<T>(string json)
        {
            var output = JsonConvert.DeserializeObject<Output<T>>(json);
            if (output.Status.Code == 200)
                return output.Results;
            return null;
        }
    }
}