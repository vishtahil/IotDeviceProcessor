using Emerson.ColdChain.DeviceProcessing.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerson.ColdChain.DeviceProcessing.Business
{
    public class JsonFileParser:IFileParser
    {
        public T Parse<T>(Byte[] fileBytes)
        {
            using (var reader = new StreamReader(new MemoryStream(fileBytes)))
            {
                var json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        /// <summary>
        /// get string 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetFileStream<T>(List<T> data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }
    }
}
