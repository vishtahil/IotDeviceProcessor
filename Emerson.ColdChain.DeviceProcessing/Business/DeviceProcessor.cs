using Emerson.ColdChain.DeviceProcessing.Interfaces;
using Emerson.ColdChain.DeviceProcessing.Models;

namespace Emerson.ColdChain.DeviceProcessing.Business
{
    public class DeviceProcessor:IDeviceProcessor
    {
        private readonly IFileParser _fileParser;
        public DeviceProcessor(IFileParser parser)
        {
            _fileParser = parser;
        }

        /// <summary>
        /// Aggregate Device Data
        /// </summary>
        /// <param name="deviceData1"></param>
        /// <param name="deviceData2"></param>
        /// <returns></returns>
        public string AggregateDeviceFiles(Byte[] file1Bytes, Byte[] file2Bytes)
        {
            var deviceData1 = _fileParser.Parse<DeviceInformation1>(file1Bytes);
            var deviceData2 = _fileParser.Parse<DeviceInformation2>(file2Bytes);
            var deviceSummaryList = AggregateDeviceData( deviceData1,  deviceData2);
            var deviceString = _fileParser.GetFileStream(deviceSummaryList);
            return deviceString;
        }


        /// <summary>
        /// Aggregate Device Data
        /// </summary>
        /// <param name="deviceData1"></param>
        /// <param name="deviceData2"></param>
        /// <returns></returns>
        public List<DeviceSummary> AggregateDeviceData(DeviceInformation1 deviceData1, DeviceInformation2 deviceData2)
        {
            //standardise  Device 1 data 
            var deviceMergedSummary = new List<DeviceSummary>();

            deviceMergedSummary.AddRange(StandardizeDevice1(deviceData1));

            //standardize Device 2 data
            deviceMergedSummary.AddRange(StandardizeDevice2(deviceData2));

            var deviceSummaryList = SummarizeData(deviceMergedSummary);

            return deviceSummaryList;
        }


        /// <summary>
        /// summarize data
        /// </summary>
        /// <param name="deviceMergedSummary"></param>
        /// <returns></returns>
        public List<DeviceSummary> SummarizeData(List<DeviceSummary> deviceMergedSummary)
        {
            //aggregate the data
            var summaryData = deviceMergedSummary.GroupBy(d => new { d.CompanyId, d.DeviceId })
                                .Select(x => new DeviceSummary()
                                {
                                    CompanyId = x.Key.CompanyId,
                                    DeviceId = x.Key.DeviceId,
                                    CompanyName = x.First().CompanyName,
                                    DeviceName = x.First().DeviceName,
                                    FirstReadingDtm = x.Min(t => t.FirstReadingDtm),
                                    LastReadingDtm = x.Max(t => t.LastReadingDtm),
                                    TemperatureCount = x.Sum(t => t.TemperatureCount),
                                    HumidityCount = x.Sum(t => t.HumidityCount),
                                    AverageHumdity = x.Where(x => x.AverageHumdity.HasValue).Average(x => x.AverageHumdity),
                                    AverageTemperature = x.Where(x => x.AverageTemperature.HasValue).Average(x => x.AverageTemperature)

                                }).ToList();
            return summaryData;
        }

        /// <summary>
        /// Standardise Device 2 
        /// </summary>
        /// <param name="device2Data"></param>
        /// <returns></returns>
        public List<DeviceSummary> StandardizeDevice2(DeviceInformation2 device2Data)
        {
            List<DeviceSummary> deviceMergedSummary = new List<DeviceSummary>();
            //standardise  Device 2 data
            foreach (var device in device2Data.Devices)
            {
                var deviceSummary = new DeviceSummary();
                deviceSummary.CompanyName = device2Data.Company;
                deviceSummary.CompanyId = device2Data.CompanyId;
                deviceSummary.DeviceId = device.DeviceID;
                deviceSummary.DeviceName = device.Name;

                deviceSummary.FirstReadingDtm = Convert.ToDateTime(device?.SensorData?.Min(t => t.DateTime));
                deviceSummary.LastReadingDtm = Convert.ToDateTime(device?.SensorData?.Max(t => t.DateTime));

                var temperatureData = device?.SensorData?.Where(x => x.SensorType == "TEMP").ToList();
                deviceSummary.TemperatureCount = temperatureData?.Count()??0;
                deviceSummary.AverageTemperature =(deviceSummary.TemperatureCount>0)? temperatureData?.Average(x => x.Value) ?? 0: 0 ;

                var humidityData = device?.SensorData?.Where(x => x.SensorType == "HUM").ToList();
                deviceSummary.HumidityCount = humidityData?.Count() ?? 0;
                deviceSummary.AverageHumdity = (deviceSummary.HumidityCount > 0) ? humidityData?.Average(x => x.Value) ?? 0:0;

                deviceMergedSummary.Add(deviceSummary);
            }
            return deviceMergedSummary;
        }

        /// <summary>
        /// Standardize Device 1
        /// </summary>
        /// <param name="device1Data"></param>
        /// <returns></returns>
        public List<DeviceSummary> StandardizeDevice1(DeviceInformation1 device1Data)
        {
            List<DeviceSummary> deviceMergedSummary = new List<DeviceSummary>();
            foreach (var device in device1Data.Trackers)
            {
                var deviceSummary = new DeviceSummary();
                deviceSummary.CompanyName = device1Data.PartnerName;
                deviceSummary.CompanyId = device1Data.PartnerId;
                deviceSummary.DeviceId = device.Id;
                deviceSummary.DeviceName = device.Model;

                var selectedCrumbs = device?.Sensors?.SelectMany(c => c.Crumbs);
                deviceSummary.FirstReadingDtm = Convert.ToDateTime(selectedCrumbs?.Min(t => t.CreatedDtm));
                deviceSummary.LastReadingDtm = Convert.ToDateTime(selectedCrumbs?.Max(t => t.CreatedDtm));

                var temperatureData = device?.Sensors?.Where(x => x.Name == "Temperature").FirstOrDefault()?.Crumbs;
                deviceSummary.TemperatureCount = temperatureData?.Count()??0;
                deviceSummary.AverageTemperature = temperatureData?.Average(t => t.Value) ?? 0;

                var humidityData = device?.Sensors?.Where(x => x.Name == "Humidty").FirstOrDefault()?.Crumbs;
                deviceSummary.HumidityCount = humidityData?.Count() ??0;
                deviceSummary.AverageHumdity = humidityData?.Average(t => t.Value)??0;

                deviceMergedSummary.Add(deviceSummary);
            }
            return deviceMergedSummary;
        }
    }
}