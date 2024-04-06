using CsvHelper;
using System.Globalization;
using System.Xml.Serialization;

namespace M3UF5CSVFileManagement;

public class Driver
{
    public static void Main()
    {
        const string CSVFileName = @"..\..\..\DataFiles\WaterConsumptionOnCat.csv";
        const string XMLFileName = @"..\..\..\DataFiles\WaterConsumptionOnCat.xml";

        List<ConsumptionInfo> groupInfo;
        using (StreamReader reader = new StreamReader(CSVFileName)) {
            groupInfo = CRUD.CSVDeserialize<ConsumptionInfo,ConsumptionInfoCSVMap>(reader);

            foreach (ConsumptionInfo info in groupInfo)
            {
                Console.WriteLine(info);
            }
        }

        using (StreamWriter writer = new StreamWriter(XMLFileName))
        {
            CRUD.XMLSerialize(writer, groupInfo);
        }
         
    }
}