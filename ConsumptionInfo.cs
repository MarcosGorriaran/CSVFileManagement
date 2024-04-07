using CsvHelper.Configuration.Attributes;
using System.Xml.Serialization;

namespace M3UF5CSVFileManagement
{
    [XmlRoot("WaterConsumptionOnCat")]
    public class ConsumptionInfo
    {
        [XmlElement("Any")]
        [Name("Any")]
        public int Year { get; set; }
        [XmlElement("Codi_comarca")]
        [Name("Codi comarca")]
        public int LocCode { get; set; }
        [XmlElement("Comarca")]
        [Name("Comarca")]
        public string LocName { get; set; }
        [XmlElement("Poblacio")]
        [Name("Població")]
        public int Population {  get; set; }
        [XmlElement("Domestic_xarxa")]
        [Name("Domèstic xarxa")]
        public int HouseNet {  get; set; }
        [XmlElement("Activitats_economicas")]
        [Name("Activitats econòmiques i fonts pròpies")]
        public int EconomicAct {  get; set; }
        [XmlElement("Total")]
        [Name("Total")]
        public int Total {  get; set; }
        [XmlElement("Consum_domestic_per_capita")]
        [Name("Consum domèstic per càpita")]
        public float HouseExpenseCapita {  get; set; }

        public static void XMLSerializeGroup(List<ConsumptionInfo> groupInfo)
        {

        }

        public override string ToString()
        {
            return $"Year: {this.Year}"+Environment.NewLine+
            $"LocCode: {this.LocCode}" + Environment.NewLine +
            $"LocName: {this.LocName}" + Environment.NewLine +
            $"Population: {this.Population}" + Environment.NewLine +
            $"HouseNet: {this.HouseNet}" + Environment.NewLine +
            $"EconomicAct: {this.EconomicAct}" + Environment.NewLine +
            $"Total: {this.Total}" + Environment.NewLine +
            $"HouseExpenseCapita: {this.HouseExpenseCapita}" + Environment.NewLine;
        }
    }
}
