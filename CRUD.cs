using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using System.Reflection.PortableExecutable;
using System.Xml.Serialization;

namespace M3UF5CSVFileManagement
{
    public static class CRUD
    {
        public static List<Class> CSVDeserialize<Class,TMap>(StreamReader reader) where TMap : ClassMap
        {
            List<Class> result;
            using CsvReader excelReader = new CsvReader(reader, CultureInfo.InvariantCulture);

            excelReader.Context.RegisterClassMap<TMap>();
            result = excelReader.GetRecords<Class>().ToList();
            return result;
        }

        public static void XMLSerialize<Class>(StreamWriter writer, List<Class> toBeStored)
        {
            XmlSerializer xmlConvert = new XmlSerializer(toBeStored.GetType());
            xmlConvert.Serialize(writer, toBeStored);
        }
        public static List<Class> XMLDeserialize<Class>(StreamReader reader)
        {
            List<Class> result = new List<Class>();
            XmlSerializer xmlConvert = new XmlSerializer(result.GetType());
            result = (List<Class>)xmlConvert.Deserialize(reader);

            return result;
        }
    }
}
