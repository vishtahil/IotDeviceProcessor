# IotDeviceProcessor
Instructions:
# Please check checkout master branch
# Tools/Language useds used
    1. .net 6.0 
    2. Visual 2012
    3. Xunit Unit tests
    
 Object Oriented Design
                 +--------------+
                 |     Device   |
                 +--------------+
                 | int Id       |
                 | string Name  |
                 +--------------+
                        ^
                        |
                        |
           +------------+------------+
           |                         |
+--------------+             +-----------------+
| Temperature  |             |    Humidity     |
+--------------+             +-----------------+
| DateTime     |             | DateTime        |
| float Value  |             | float Value     |
+--------------+             +-----------------+
          ^                            ^
          |                            |
          |                            |
+------------------+          +------------------+
|    SensorData    |          |    SensorData    |
+------------------+          +------------------+
| List<Temperature>|          | List<Humidity>  |
| Device Device   |          | Device Device   |
+------------------+          +------------------+
          ^                            ^
          |                            |
          |                            |
+------------------+          +------------------+
|      Company     |          |      Company     |
+------------------+          +------------------+
| int CompanyId    |          | int CompanyId    |
| string Company   |          | string Company   |
+------------------+          +------------------+
          ^                            ^
          |                            |
          |                            |
+------------------+          +------------------+
|    DataProcessor |          |     JsonWriter   |
+------------------+          +------------------+
| List<SensorData> |          | string FilePath  |
+------------------+          +------------------+
          ^                            ^
          |                            |
          +----------------------------+
                     Uses
