# IotDeviceProcessor
Instructions:
# Please check checkout master branch
# Tools/Language useds used
    1. .net 6.0 
    2. Visual 2012
    3. Xunit Unit tests
    
 Object Oriented Design
#Device represents an IoT device that captures temperature and humidity data.
#Temperature and Humidity represent sensor data captured by the device, with each containing a timestamp and a sensor value.
#SensorData represents a collection of temperature and humidity data captured by a device.
#Company represents the company that owns the device.
#DeviceProcessor.cs is responsible for reading the data from the JSON files, processing the data, and generating a new JSON file with the merged and summarized data.
#JsonFileParse is responsible for writing the merged and summarized data to a new JSON file.
#The DeviceProcessor.cs uses the other classes to process the data from the two JSON files, calculate the required summary information, and create a new List<SensorData> containing the merged and summarized data. The JsonWriter is then used to write this data to a new JSON file.
               
