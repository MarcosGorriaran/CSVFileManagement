using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
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
        public static List<Class> CSVDeserialize<Class>(StreamReader reader)
        {
            List<Class> result;
            using CsvReader excelReader = new CsvReader(reader, CultureInfo.InvariantCulture);

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
        public static List<Class> SearchOnList<Class>(List<Class> list, Func<Class, bool> condition)
        {
            List<Class> result;

            result = (from element in list
                      where condition(element)
                      select element).ToList();

            return result;
        }
        public static List<Class> OrderBy<Class>(List<Class> list, Func<Class, object> orderElement, int limit, bool orderAsc)
        {
            List<Class> result;
            if (!orderAsc)
            {
                var procesingData = from element in list
                                    orderby orderElement(element)
                                    select element;
                result = procesingData.Take(limit).ToList();
            }
            else
            {
                var procesingData = from element in list
                                    orderby orderElement(element) descending
                                    select element;
                result = procesingData.Take(limit).ToList();
            }
            return result;
        }
        public static Dictionary<string,float> GetAvarage<Class>(List<Class> list, Func<Class,object> groupElement, Func<Class,float>avgValue, Func<Class,string> keyValue)
        {
            var query = from element in list
                         group element by groupElement(element);
            Dictionary<string, float> result=new Dictionary<string, float>();
            foreach(var item in query)
            {
                result.Add(keyValue(item.First()), item.Average(avgValue));
            }
            return result;
        }
    }
}
