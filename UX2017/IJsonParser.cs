using System.Collections.Generic;
using Newtonsoft.Json;
using UX2017.Models;

namespace UX2017
{
    public interface IJsonParser
    {
        IEnumerable<T> Parse<T>(string json);
    }

    public class JsonParser : IJsonParser
    {
        public IEnumerable<T> Parse<T>(string json)
        {
            var output = JsonConvert.DeserializeObject<Output<T>>(json);
            if (output.Status.Code == 200)
                return output.Results;

            // TODO: error handling
            return null;
        }
    }
}