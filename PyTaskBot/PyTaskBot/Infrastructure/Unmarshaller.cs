using System;
using Newtonsoft.Json;

namespace PyTaskBot.Infrastructure
{
    public static class Unmarshaller<T>
    {
        public static T Unmarshal(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new ArgumentException("The json string is null or empty.");
            }

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}