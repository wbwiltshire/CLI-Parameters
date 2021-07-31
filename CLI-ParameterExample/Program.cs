using System;
using System.Collections.Generic;
using CommandLine;

namespace CLI_ParameterExample
{
    public class Program
    {
        //
        // Note: Assembly Information (e.g. program name, version, etc.) is contained in the Project Properties (Project->CLI-Parameters->Package)
        //

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
            ParserResult<CLParameters> parsed = Parser.Default.ParseArguments<CLParameters>(args)
                .WithParsed<CLParameters>(options => options.Run())
                .WithNotParsed(HandleErrors);

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
