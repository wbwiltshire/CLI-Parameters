using System;
using System.Collections.Generic;
using System.Reflection;
using CommandLine;
using CommandLine.Text;

namespace CLI_ParameterExample
{
    public class Program
    {
        //
        // Note: Assembly Information (e.g. program name, version, etc.) is contained in the Project Properties (Project->CLI-Parameters->Package)
        //

        private static int ReturnCode = 0;
        private static bool hasNoErrors = true;

        static int Main(string[] args)
        {
            CLParameters parms = null;

            // Set the parser options
            Parser parser = new Parser(with => {
                with.AutoHelp = true;
                with.AutoVersion = true;
                //with.CaseInsensitiveEnumValues = true;
                with.CaseSensitive = false;
                //with.EnableDashDash = true;
                with.HelpWriter = null;
                //with.IgnoreUnknownArguments = true;
                //with.MaximumDisplayWidth = false;
                //with.ParsingCulture = System.Globalization.CultureInfo.InvariantCulture;
            });

            // Run the singleton version of the Parser
            ParserResult<CLParameters> parsed = parser.ParseArguments<CLParameters>(args);

            parsed.WithParsed<CLParameters>(options => options.Run())
                .WithNotParsed(errors => HandleErrors(parsed, errors));

            // return the parsed object to be used by the application
            if (hasNoErrors)
                parms = ((Parsed<CLParameters>)parsed).Value;

            return ReturnCode;
        }

        private static void HandleErrors<T>(ParserResult<T> result, IEnumerable<Error> errors)
        {
            ReturnCode = 1;
            hasNoErrors = false;
            HelpText helpText = null;


            if (errors.IsVersion())
            {
                helpText = HelpText.AutoBuild(result);      // Use the default, if just printing version
                ReturnCode = 0;
            }
            else if (errors.IsHelp()) {
                // Else, build a custom help message
                helpText = HelpText.AutoBuild(result, h =>
                {
                    h.AdditionalNewLineAfterOption = false;
                    h.Heading = $"CLI-ParameterExample {Assembly.GetExecutingAssembly().GetName().Version}";        // Set header text
                    h.Copyright = "Copyright (c) 2021 TestCompany.com";                                             // Set copyright text
                    return h;
                });
                ReturnCode = 0;
            }
            else {
                // Else, build a custom error message
                helpText = HelpText.AutoBuild(result, h =>
                {
                    h.AdditionalNewLineAfterOption = false;
                    h.Heading = $"CLI-ParameterExample {Assembly.GetExecutingAssembly().GetName().Version}";        // Set header text
                    h.Copyright = "Copyright (c) 2021 TestCompany.com";                                             // Set copyright text
                    return HelpText.DefaultParsingErrorsHandler(result, h);
                }, e => e);
                ReturnCode = 1;
            }
            Console.WriteLine(helpText);
        }
    }
}
