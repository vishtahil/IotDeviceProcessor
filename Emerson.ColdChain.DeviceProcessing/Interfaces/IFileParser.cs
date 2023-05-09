using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Emerson.ColdChain.DeviceProcessing.Interfaces
{
    public interface IFileParser
    {
        T Parse<T>(Byte[] fileBytes);
        string GetFileStream<T>(List<T> data);
    }
}
