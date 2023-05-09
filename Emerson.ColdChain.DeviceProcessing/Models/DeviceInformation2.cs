using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerson.ColdChain.DeviceProcessing.Models
{

    public class DeviceInformation2
    {
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public Device[] Devices { get; set; }
    }

    public class Device
    {
        public int DeviceID { get; set; }
        public string Name { get; set; }
        public string StartDateTime { get; set; }
        public Sensordata[] SensorData { get; set; }
    }

    public class Sensordata
    {
        public string SensorType { get; set; }
        public string DateTime { get; set; }
        public float Value { get; set; }
    }

}
