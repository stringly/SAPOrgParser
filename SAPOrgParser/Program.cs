using SAPOrgParser.Utility;
using System;
using System.IO;

namespace SAPOrgParser
{
    class Program
    {
        private const string defaultFilePath = @"C:\Users\jcs30\OneDrive\Desktop\Org Structure.txt";
        private const string defaultJSONOutputPath = @"C:\Users\jcs30\OneDrive\Desktop\ParsedEntities.json";
        private const string defaultFinalComponentJSONOutputPath = @"C:\Users\jcs30\OneDrive\Desktop\FinaComponent.json";
        private static string desktop;
        private static Parser Parser;

        static void Main(string[] args)
        {
            
            desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Parser = new Parser();
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }
        public static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine(@"=========================================================================================================");
            Console.WriteLine(@" _____   ___  ______   _____              _    _____         _      ______                               ");
            Console.WriteLine(@"/  ___| / _ \ | ___ \ |_   _|            | |  |_   _|       | |     | ___ \                              ");
            Console.WriteLine(@"\ `--. / /_\ \| |_/ /   | |    ___ __  __| |_   | |    __ _ | |__   | |_/ /  __ _  _ __  ___   ___  _ __ ");
            Console.WriteLine(@" `--. \|  _  ||  __/    | |   / _ \\ \/ /| __|  | |   / _` || '_ \  |  __/  / _` || '__|/ __| / _ \| '__|");
            Console.WriteLine(@"/\__/ /| | | || |       | |  |  __/ >  < | |_   | |  | (_| || |_) | | |    | (_| || |   \__ \|  __/| |   ");
            Console.WriteLine(@"\____/ \_| |_/\_|       \_/   \___|/_/\_\ \__|  \_/   \__,_||_.__/  \_|     \__,_||_|   |___/ \___||_|   ");
            Console.WriteLine(@"                                                                                                         ");
            Console.WriteLine(@"=========================================================================================================");
            Console.WriteLine(@"                                                                                                         ");
            Console.WriteLine(@">   MAIN MENU");
            Console.WriteLine(@">   Please enter the location of the SAP Text/Tab Flat file output:");
            Console.WriteLine(@">   (Press [Enter] to use the default: attempt to find 'Org Structure.txt' on the local desktop.)");
            Console.WriteLine(@">   (Type 'exit' to exit the program.)");

            try
            {
                string filePath;
                string input = Console.ReadLine();
                switch (input)
                {
                    case "":
                        Console.WriteLine(@$">  Attempting to locate file at default path of: {desktop}\Org Structure.txt");
                        if (!File.Exists($@"{desktop}\Org Structure.txt"))
                        {
                            throw new FileNotFoundException();
                        }

                        filePath = $@"{desktop}\Org Structure.txt";
                        break;
                    case "exit":
                    case "Exit":
                        Console.WriteLine(">    Exiting SAP Text Tab Parser. Goodbye!");
                        return false;
                    default:
                        Console.WriteLine($@">  Attempting to locate file at: {input}");
                        if (!File.Exists(input))
                        {
                            throw new FileNotFoundException();
                        }
                        filePath = input;
                        break;
                }
                Console.WriteLine(">    Attempting to parse the file.");
                Parser.Parse(File.ReadAllLines(filePath));
                Console.WriteLine(">    File parsed successfully.");
                bool showOutputMenu = true;
                while (showOutputMenu)
                {
                    showOutputMenu = OutputMenu();
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"SAP TextTab Parser Exception: {ex.Message}");
                return true;
            }
        }
        public static bool OutputMenu()
        {
            Console.WriteLine(@"=========================================================================================================");
            Console.WriteLine(@"                                                                                                         ");
            Console.WriteLine(@">   OUTPUT OPTIONS");
            Console.WriteLine(@">   Choose an output option:");
            Console.WriteLine(@">   1) Raw String Array Items -> JSON");
            Console.WriteLine(@">   2) Raw String Array Items -> XML");
            Console.WriteLine(@">   3) Parsed Entity List -> JSON");
            Console.WriteLine(@">   4) Parsed Entity List -> XML");
            Console.WriteLine(@">   5) Non-Nested Component List (No Parent/Child Relationships) -> JSON");
            Console.WriteLine(@">   6) Non-Nested Component List (No Parent/Child Relationships) -> XML");
            Console.WriteLine(@">   7) Component Flat List -> JSON");
            Console.WriteLine(@">   8) Component Flat List -> XML");
            Console.WriteLine(@">   9) Position Flat List -> JSON");
            Console.WriteLine(@">   10) Position Flat List -> XML");
            Console.WriteLine(@">   11) Person Flat List -> JSON");
            Console.WriteLine(@">   12) Person Flat List -> XML");
            Console.WriteLine(@">   13) Top-Level Component (including nested Children) -> JSON");
            Console.WriteLine(@">   14) Top-Level Component (including nested Children) -> XML");
            Console.WriteLine(@">   'exit') Exit the program.");
            string input = Console.ReadLine();
            try
            {
                string path;
                //FileStream file;
                switch (input)
                {
                    case "1":
                        Console.WriteLine($@">  Attempting to output the String Array to {desktop}\SAPOrgParser_StringArray.json");
                        File.WriteAllText($@"{desktop}\SAPOrgParser_StringArray.json", Parser.Lines.ToJSONString());
                        Console.WriteLine($@">  Success! File created: {desktop}\SAPOrgParser_StringArray.json");
                        return true;
                    case "2":
                        Console.WriteLine($@"Attempting to output the String Array to {desktop}\SAPOrgParser_StringArray.xml");
                        path = desktop + "//SAPOrgParser_StringArray.xml";
                        using (FileStream file = File.Create(path))
                        {
                            Parser.Lines.ToXml(file);
                            file.Close();
                        }       
                        Console.WriteLine($@">  Success! File created: {desktop}\SAPOrgParser_StringArray.xml");
                        return true;
                    case "3":
                        Console.WriteLine($@">  Attempting to output the Parsed Entity List to {desktop}\SAPOrgParser_ParsedEntities.json");
                        File.WriteAllText($@"{desktop}\SAPOrgParser_ParsedEntities.json", Parser.ParsedEntities.ToJSONString());
                        Console.WriteLine($@">  Success! File created: {desktop}\SAPOrgParser_ParsedEntities.json");
                        return true;
                        
                    case "4":
                        Console.WriteLine($@">  Attempting to output the Parsed Entity List to {desktop}\SAPOrgParser_ParsedEntities.xml");
                        path = desktop + "//SAPOrgParser_ParsedEntities.xml";
                        using (FileStream file = File.Create(path))
                        {
                            Parser.ParsedEntities.ToXml(file);
                            file.Close();
                        }
                        Console.WriteLine($@">  Success! File created: {desktop}\SAPOrgParser_ParsedEntities.xml");                        
                        return true;
                    case "5":
                        Console.WriteLine($@">  Attempting to output the Non-Nested Component List to {desktop}\SAPOrgParser_NonNestedComponents.json");
                        File.WriteAllText($@"{desktop}\SAPOrgParser_NonNestedComponents.json", Parser.NonNestedComponentList.ToJSONString());
                        Console.WriteLine($@">  Success! File created: {desktop}\SAPOrgParser_NonNestedComponents.json");
                        return true;
                    case "6":
                        Console.WriteLine($@">  Attempting to output the Parsed Entity List to {desktop}\SAPOrgParser_NonNestedComponents.xml");
                        path = desktop + "//SAPOrgParser_NonNestedComponents.xml";
                        using (FileStream file = File.Create(path))
                        {
                            Parser.NonNestedComponentList.ToXml(file);
                            file.Close();
                        }
                        Console.WriteLine($@">  Success! File created: {desktop}\SAPOrgParser_NonNestedComponents.xml");
                        return true;
                    case "7":
                        Console.WriteLine($@">  Attempting to output the Component Flat File List to {desktop}\SAPOrgParser_ComponentsFlatFile.json");
                        File.WriteAllText($@"{desktop}\SAPOrgParser_ComponentsFlatFile.json", Parser.ComponentsFlatList.ToJSONString());
                        Console.WriteLine($@">  Success! File created: {desktop}\SAPOrgParser_ComponentsFlatFile.json");
                        return true;
                    case "8":
                        Console.WriteLine($@">  Attempting to output the Component Flat File File to {desktop}\SAPOrgParser_ComponentsFlatFile.xml");
                        path = desktop + "//SAPOrgParser_ComponentsFlatFile.xml";
                        using (FileStream file = File.Create(path))
                        {
                            Parser.ComponentsFlatList.ToXml(file);
                            file.Close();
                        }
                        Console.WriteLine($@">  Success! File created: {desktop}\SAPOrgParser_ComponentsFlatFile.xml");
                        return true;
                    case "9":
                        Console.WriteLine($@">  Attempting to output the Position FLat File List to {desktop}\SAPOrgParser_PositionsFlatFile.json");
                        File.WriteAllText($@"{desktop}\SAPOrgParser_PositionsFlatFile.json", Parser.PositionsFlatList.ToJSONString());
                        Console.WriteLine($@">  Success! File created: {desktop}\SAPOrgParser_PositionsFlatFile.json");
                        return true;
                    case "10":
                        Console.WriteLine($@">  Attempting to output the Position FLat File to {desktop}\SAPOrgParser_PositionsFlatFile.xml");
                        path = desktop + "//SAPOrgParser_PositionsFlatFile.xml";
                        using (FileStream file = File.Create(path))
                        {
                            Parser.PositionsFlatList.ToXml(file);
                            file.Close();
                        }
                        Console.WriteLine($@">  Success! File created: {desktop}\SAPOrgParser_PositionsFlatFile.xml");
                        return true;
                    case "11":
                        Console.WriteLine($@">  Attempting to output the Person FLat File List to {desktop}\SAPOrgParser_PersonsFlatFile.json");
                        File.WriteAllText($@"{desktop}\SAPOrgParser_PersonsFlatFile.json", Parser.PersonsFlatList.ToJSONString());
                        Console.WriteLine($@">  Success! File created: {desktop}\SAPOrgParser_PersonsFlatFile.json");
                        return true;
                    case "12":
                        Console.WriteLine($@">  Attempting to output the Person FLat File to {desktop}\SAPOrgParser_PersonsFlatFile.xml");
                        path = desktop + "//SAPOrgParser_PersonsFlatFile.xml";
                        using (FileStream file = File.Create(path))
                        {
                            Parser.PersonsFlatList.ToXml(file);
                            file.Close();
                        }
                        Console.WriteLine($@">  Success! File created: {desktop}\SAPOrgParser_PersonsFlatFile.xml");
                        return true;
                    case "13":
                        Console.WriteLine($@">  Attempting to output the Top-Level component to {desktop}\SAPOrgParser_TopLevelComponent.json");
                        File.WriteAllText($@"{desktop}\SAPOrgParser_TopLevelComponent.json", Parser.Department.ToJSONString());
                        Console.WriteLine($@">  Success! File created: {desktop}\SAPOrgParser_TopLevelComponent.json");
                        return true;
                    case "14":
                        Console.WriteLine($@">  Attempting to output the Top-Level component to {desktop}\SAPOrgParser_TopLevelComponent.xml");
                        path = desktop + "//SAPOrgParser_TopLevelComponent.xml";
                        using (FileStream file = File.Create(path))
                        {
                            Parser.Department.ToXml(file);
                            file.Close();
                        }
                        Console.WriteLine($@">  Success! File created: {desktop}\SAPOrgParser_TopLevelComponent.xml");
                        return true;
                    case "Exit":
                    case "exit":
                        return false;
                    default:
                        Console.WriteLine($">   {input} is not a recognized option.");
                        return true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"SAP TextTab Parser Exception: {ex.Message}");
                return true;
            }
        }
    }
}
