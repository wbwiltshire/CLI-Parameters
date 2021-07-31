using System;
using System.Collections.Generic;
using CommandLine;

namespace CLI_VerbExample
{

    //
    // Note: Assembly Information (e.g. program name, version, etc.) is contained in the Project Properties (Project->CLI-Parameters->Package)
    //

    public class Program
    {
        private static int ReturnCode = 0;
        private static bool hasErrors = false;

        static int Main(string[] args)
        {
            CLParameters parms = new();

            // Set the parser options
            Parser parser = new Parser(with => {
                with.AutoHelp = true;
                with.AutoVersion = true;
                //with.CaseInsensitiveEnumValues = true;
                with.CaseSensitive = false;
                //with.EnableDashDash = true;
                //with.HelpWriter = null;
                //with.IgnoreUnknownArguments = true;
                //with.MaximumDisplayWidth = false;
                //with.ParsingCulture = System.Globalization.CultureInfo.InvariantCulture;
            });

            // Run the singleton version of the Parser
            ParserResult<object> parsed = Parser.Default.ParseArguments<StartOptions, StopOptions, StatusOptions>(args)
                .WithParsed<StartOptions>(options => options.Run())
                .WithParsed<StopOptions>(options => options.Run())
                .WithParsed<StatusOptions>(options => options.Run())
                .WithNotParsed(HandleErrors);

            if (!hasErrors)
            {
                parms.Options = (VerbOptionsBase)((Parsed<object>)parsed).Value;

                switch ((object)parms.Options)
                {
                    case CLI_VerbExample.StartOptions:
                        Console.WriteLine("It's a Start verb.");
                        break;
                    case CLI_VerbExample.StopOptions:
                        Console.WriteLine("It's a Stop verb.");
                        break;
                    case CLI_VerbExample.StatusOptions:
                        Console.WriteLine("It's a Status verb.");
                        break;
                    default:
                        break;
                }
            }

            return ReturnCode;
        }

        private static void HandleErrors(IEnumerable<Error> errors)
        {
            ReturnCode = 1;
            hasErrors = true;

            if (errors.IsVersion())
            {
                ReturnCode = 0;
            }

            if (errors.IsHelp())
            {
                ReturnCode = 0;
            }
        }

    }
}
