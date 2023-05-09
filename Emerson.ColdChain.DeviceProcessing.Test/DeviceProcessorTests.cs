using Emerson.ColdChain.DeviceProcessing.Business;
using Emerson.ColdChain.DeviceProcessing.Common;
using Emerson.ColdChain.DeviceProcessing.Interfaces;
using Emerson.ColdChain.DeviceProcessing.Models;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;

namespace Emerson.ColdChain.DeviceProcessing.Test
{
    public class DeviceProcessorTests
    {
        private readonly IDeviceProcessor _deviceProcessor;
        private readonly IFileParser _fileParser;
        public DeviceProcessorTests()
        {
            _fileParser= new  JsonFileParser();
            _deviceProcessor = new DeviceProcessor(_fileParser);
           
        }

        [Fact]
        public void AggregateDeviceFiles_Test_TestCase_With_Missing_Humidity_Temperature_Data()
        {
            // Read data from file 1
            var device1Data = Utilities.ReadJsonFile<DeviceInformation1>("Files/TestCaseFiles/TestCase2/DeviceDataFoo1.json");
          
            // Read data from file 2  
            var device2Data = Utilities.ReadJsonFile<DeviceInformation2>("Files/TestCaseFiles/TestCase2/DeviceDataFoo2.json");

            var mergeSummary = _deviceProcessor.AggregateDeviceData(device1Data, device2Data);

            Assert.NotNull(mergeSummary);
            Assert.True(mergeSummary.Count == 2);

            AssertResults(mergeSummary[0], 
                companyId: 1, companyName: "Foo1",
                deviceId: 1, deviceName: "ABC-100",
                firstReadingDtm:Convert.ToDateTime("2020-08-17T10:35:00"), 
                lastReadingDtm: Convert.ToDateTime("2020-08-17T10:45:00"), 
                temperatureCount: 3, averageTemperature: 2,
                humudityCount: 0, averageHumidity: 0);

            AssertResults(mergeSummary[1], 
                 companyId: 2,companyName: "Foo2", 
                 deviceId: 1, deviceName: "XYZ-100",
                 firstReadingDtm: Convert.ToDateTime("2020-08-18T10:35:00"),
                 lastReadingDtm: Convert.ToDateTime("2020-08-18T10:45:00"),
                 temperatureCount: 0,averageTemperature: 0,
                 humudityCount: 3, averageHumidity: 2);

        }

        private void AssertResults(DeviceSummary summary,  int companyId, string companyName, 
                int? deviceId,string deviceName,DateTime? firstReadingDtm, DateTime? lastReadingDtm,
                int? temperatureCount, double? averageTemperature, int? humudityCount, double? averageHumidity)
        {
            Assert.Equal(summary.DeviceName, deviceName);
            Assert.Equal(summary.CompanyName, companyName);
            Assert.Equal(summary.CompanyId, companyId);
            Assert.Equal(summary.DeviceId, deviceId); 
            Assert.Equal(summary.FirstReadingDtm, firstReadingDtm);
            Assert.Equal(summary.LastReadingDtm, lastReadingDtm);
            Assert.Equal(summary.TemperatureCount, temperatureCount);
            Assert.Equal(summary.AverageTemperature?.ToString("0.00"), averageTemperature?.ToString("0.00"));
            Assert.Equal(summary.HumidityCount, humudityCount);
            Assert.Equal(summary.AverageHumdity?.ToString("0.00"), averageHumidity?.ToString("0.00"));
        }

        [Fact]
        public void AggregateDeviceFiles_TestCase1_Standard()
        {
            var dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location.Replace("\\bin\\Debug\\net6.0", string.Empty));

            File.Delete("Files/TestCaseFiles/TestCase1/Output.json");
            var mergedSummary = _deviceProcessor.AggregateDeviceFiles(File.ReadAllBytes("Files/TestCaseFiles/TestCase1/DeviceDataFoo1.json")
                , File.ReadAllBytes("Files/TestCaseFiles/TestCase1/DeviceDataFoo2.json"));
            //save file
            File.WriteAllText($"{dirName}//Files//TestCaseFiles//TestCase1//Output.json", mergedSummary);

            var outputData = Utilities.ReadJsonFile<List<DeviceSummary>>($"{dirName}/Files/TestCaseFiles/TestCase1/output.json");

            Assert.True(outputData.Count == 4);
            AssertResults(outputData[0],
              companyId: 1, companyName: "Foo1",
              deviceId: 1, deviceName: "ABC-100",
              firstReadingDtm: Convert.ToDateTime("2020-08-17T10:35:00"),
              lastReadingDtm: Convert.ToDateTime("2020-08-17T10:45:00"),
              temperatureCount: 3, averageTemperature: 23.15,
              humudityCount: 3, averageHumidity: 81.5);

            AssertResults(outputData[1],
             companyId: 1, companyName: "Foo1",
             deviceId: 2, deviceName: "ABC-200",
             firstReadingDtm: Convert.ToDateTime("2020-08-18T10:35:00"),
             lastReadingDtm: Convert.ToDateTime("2020-08-18T10:45:00"),
             temperatureCount: 3, averageTemperature: 24.15,
             humudityCount: 3, averageHumidity: 82.5);
        }

    }
}