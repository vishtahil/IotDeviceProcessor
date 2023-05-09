using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerson.ColdChain.DeviceProcessing.Models
{
    public class DeviceInformation1
    {
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }
        public Tracker[] Trackers { get; set; }
    }

    public class Tracker
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string ShipmentStartDtm { get; set; }
        public Sensor[] Sensors { get; set; }
    }

    public class Sensor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Crumb[] Crumbs { get; set; }
    }

    public class Crumb
    {
        public string CreatedDtm { get; set; }
        public float Value { get; set; }
    }

}
