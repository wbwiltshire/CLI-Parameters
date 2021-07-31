using System;
using CommandLine;

namespace CLI_VerbExample
{

    public enum Action
    {
        START = 1,
        STOP = 2,
        STATUS = 3
    }

    public class CLParameters
    {
        public VerbOptionsBase Options { get; set; }
    }

    public abstract class VerbOptionsBase
    {
        [Option(shortName:'d', HelpText = "Enter days", Required = false, Default = 0)]
        public int Days { get; set; }                                                               // Long name defaults to name of property, ie "--days"
        [Option(shortName:'b', longName: "BeginDate", HelpText = "Enter begin date", Required = true)]
        public DateTime BeginDate { get; set; }
        [Option(shortName: 'f', longName: "FileName", HelpText = "Enter file name", Required = false, Default = "SampleInput.csv")]
        public string FileName { get; set; }
        public Action Value { get; set; }

        public void PrintOptions()
        {
            Console.WriteLine("\nCommand Line Parameters:");
            Console.WriteLine($"\tDays      : {Days}");
            Console.WriteLine($"\tBegin date: {BeginDate.ToString("MM/dd/yyyy")}");
            Console.WriteLine($"\tFile name : {FileName}");
            Console.WriteLine($"\tAction    : {this.ToString()}");
            Console.WriteLine($"\tType: {this.GetType().Name}");
        }

        public abstract void Run();
    }

    [Verb("start", HelpText = "Start the process")]
    public class StartOptions : VerbOptionsBase
    {
        public override void Run()
        {
            this.Value = Action.START;
            PrintOptions();

        }

        public override string ToString()
        {
            return "Start";
        }
    }

    [Verb("stop", HelpText = "Stop the process")]
    public class StopOptions : VerbOptionsBase
    {
        public override void Run()
        {
            this.Value = Action.STOP;
            PrintOptions();
        }

        public override string ToString()
        {
            return "Stop";
        }

    }

    [Verb("status", HelpText = "Display process status")]
    public class StatusOptions : VerbOptionsBase
    {
        public override void Run()
        {
            this.Value = Action.STATUS;
            PrintOptions();
        }

        public override string ToString()
        {
            return "Status";
        }
    }
}
