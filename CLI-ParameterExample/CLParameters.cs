using System;
using CommandLine;

namespace CLI_ParameterExample
{

    public class CLParameters
    {
        [Option(shortName: 'd', HelpText = "Enter days", Required = false, Default = 0)]
        public int Days { get; set; }                                                               // Long name defaults to name of property, ie "--days"
        [Option(shortName: 'b', longName: "BeginDate", HelpText = "Enter begin date", Required = true)]
        public DateTime BeginDate { get; set; }
        [Option(shortName: 'f', longName: "FileName", HelpText = "Enter file name", Required = false, Default = "SampleInput.csv")]
        public string FileName { get; set; }

        public void Run()
        {
            PrintOptions();
        }

        public void PrintOptions()
        {
            Console.WriteLine("\nCommand Line Parameters:");
            Console.WriteLine($"\tDays      : {Days}");
            Console.WriteLine($"\tBegin date: {BeginDate.ToString("MM/dd/yyyy")}");
            Console.WriteLine($"\tFile name : {FileName}");
        }

    }

}
