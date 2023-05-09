using Emerson.ColdChain.DeviceProcessing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerson.ColdChain.DeviceProcessing.Interfaces
{
    public interface IDeviceProcessor
    {
        List<DeviceSummary> AggregateDeviceData(DeviceInformation1 deviceData1, DeviceInformation2 deviceData2);
        List<DeviceSummary> SummarizeData(List<DeviceSummary> deviceMergedSummary);
        List<DeviceSummary> StandardizeDevice2(DeviceInformation2 device2Data);
        List<DeviceSummary> StandardizeDevice1(DeviceInformation1 device1Data);
        string AggregateDeviceFiles(Byte[] file1Bytes, Byte[] file2Bytes);
    }
}
