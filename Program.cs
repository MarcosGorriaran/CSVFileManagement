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
        const string Menu = "1. Read CSV info\n" +
            "2. Store CSV info into an XML file\n" +
            "3. Get regions with populations over {0}\n" +
            "4. Get avarage house expense\n" +
            "5. Get top {1} Biggest house expenses\n" +
            "6. Get top {1} Lowest house expenses\n" +
            "7. Filter by region code\n" +
            "8. Exit\n" +
            "Pick an option: ";
        const string ErrorOutsideRange = "The specified value is outside the menu options";
        const string OverSearchAmountTitle = "List of populations over {0}: ";
        const string AvgSearchResultShow = "The avarage house expense is {0}";
        const string BiggestExpensesResult = "The top {0} biggest house expenses are the following: ";
        const string LowestExpenseResult = "The top {0} lowest house expenses are the following: ";
        const string ShowSpecifiedInfo = "Ths is the information recovered from the selected code";
        const string StoreSuccess = "The information has been stored succesfully";
        const string AskCode = "Specify the code you desire to search: ";
        const char SectionSpliter = '-';
        const int LimitSearch = 5;
        const int ReadCSVOption = 1;
        const int WriteXMLOption = 2;
        const int FindOverValue = 3;
        const int FindAvgExpenses = 4;
        const int FindLowestExpenses = 5;
        const int FindHighestExpenses = 6;
        const int FilterByCode = 7;
        const int ExitOption = 8;
        const int SearchAmount = 200000;

        int option=0;
        int code=0;
        List<ConsumptionInfo> groupInfo;
        List<ConsumptionInfo> filteredInfo;
        using (StreamReader reader = new StreamReader(CSVFileName)) {
            groupInfo = CRUD.CSVDeserialize<ConsumptionInfo,ConsumptionInfoCSVMap>(reader);
        }
        do
        {
            
            bool error;
            do
            {
                string errorMsg;
                error = false;
                Console.WriteLine(new string(SectionSpliter, Console.WindowWidth));
                Console.Write(Menu, SearchAmount,LimitSearch);
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    error = true;
                    errorMsg = e.Message;
                }
                if(option<ReadCSVOption || option > ExitOption)
                {
                    error = true;
                    errorMsg = ErrorOutsideRange;
                }
                Console.WriteLine(new string(SectionSpliter,Console.WindowWidth));
            } while (error);
            
            switch (option)
            {
                case ReadCSVOption:
                    foreach (ConsumptionInfo info in groupInfo)
                    {
                        Console.WriteLine(info);
                    }
                    break;
                case WriteXMLOption:
                    using (StreamWriter writer = new StreamWriter(XMLFileName))
                    {
                        CRUD.XMLSerialize(writer, groupInfo);
                    }
                    Console.WriteLine(StoreSuccess);
                    break;
                case FindOverValue:
                    filteredInfo = CRUD.SearchOnList(groupInfo,info=>info.Population>SearchAmount);
                    Console.WriteLine(OverSearchAmountTitle,SearchAmount);
                    foreach(ConsumptionInfo info in filteredInfo)
                    {
                        Console.WriteLine(info);
                    }
                    break;
                case FindAvgExpenses:
                    Console.WriteLine(AvgSearchResultShow,groupInfo.Average(info=>info.HouseExpenseCapita));
                    break;
                case FindHighestExpenses:
                    filteredInfo = CRUD.OrderBy(groupInfo,info=>info.HouseExpenseCapita,LimitSearch,false);
                    Console.WriteLine(BiggestExpensesResult,LimitSearch);
                    foreach (ConsumptionInfo info in filteredInfo)
                    {
                        Console.WriteLine(info);
                    }
                    break;
                case FindLowestExpenses:
                    filteredInfo = CRUD.OrderBy(groupInfo, info => info.HouseExpenseCapita, LimitSearch, true);
                    Console.WriteLine(LowestExpenseResult, LimitSearch);
                    foreach (ConsumptionInfo info in filteredInfo)
                    {
                        Console.WriteLine(info);
                    }
                    break;
                case FilterByCode:
                    
                    do
                    {
                        error = false;
                        Console.Write(AskCode);
                        try
                        {
                            code = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                            error = true;
                        }
                    }while (error);
                    filteredInfo = CRUD.SearchOnList(groupInfo,info=>info.LocCode==code);
                    Console.WriteLine(ShowSpecifiedInfo);
                    foreach (ConsumptionInfo info in filteredInfo)
                    {
                        Console.WriteLine(info);
                    }
                    break;
                case ExitOption:
                    break;
            }
        } while (option!=ExitOption);
        

    }
}