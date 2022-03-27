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

        static void Main(string[] args)
        {
            string input = "init";
            while (input != "exit")
            {
                Console.WriteLine("Please provide the filepath of the SAP Text/Tab File:" +
                    $"\n Press [Enter] to use the default at {defaultFilePath}" + 
                    "\n Type 'exit' to exit the program.");
                input = Console.ReadLine();
                try
                {
                    Parser parser = new Parser();
                    string filePath;
                    switch (input)
                    {
                        case "":
                            if (!File.Exists(defaultFilePath))
                            { 
                                throw new FileNotFoundException(); 
                            }
                            Console.WriteLine($"Using the default path at {defaultFilePath}");
                            filePath = defaultFilePath;
                            break;
                        default:
                            if (!File.Exists(input))
                            {
                                throw new FileNotFoundException();
                            }
                            Console.WriteLine($"Using the input path at {input}");
                            filePath = input;
                            break;
                    }
                    Console.WriteLine("Attempting to parse the file.");
                    parser.Parse(File.ReadAllLines(filePath));
                    Console.WriteLine($"Attempting to output the Parsed Entity file to {defaultJSONOutputPath}");
                    File.WriteAllText(defaultJSONOutputPath, parser.ParsedEntities.ToJSONString());
                    Console.WriteLine($"Attempting to output the Final Component to {defaultFinalComponentJSONOutputPath}");
                    File.WriteAllText(defaultFinalComponentJSONOutputPath, parser.Department.ToJSONString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
