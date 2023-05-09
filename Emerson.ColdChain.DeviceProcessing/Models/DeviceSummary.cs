using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerson.ColdChain.DeviceProcessing.Models
{
    public class DeviceSummary
    {
        public int CompanyId { get; set; } // Foo1: PartnerId, Foo2: CompanyId
        public string CompanyName { get; set; } // Foo1: PartnerName, Foo2: Company
        public int? DeviceId { get; set; } // Foo1: Id, Foo2: DeviceID
        public string DeviceName { get; set; } // Foo1: Model, Foo2: Name
        public DateTime? FirstReadingDtm { get; set; } // Foo1: Trackers.Sensors.Crumbs, Foo2: Devices.SensorData
        public DateTime? LastReadingDtm { get; set; }
        public int? TemperatureCount { get; set; }
        public double? AverageTemperature { get; set; }
        public int? HumidityCount { get; set; }
       
        public double? AverageHumdity
        {
            get; set;
        }

       
    }

    
}
