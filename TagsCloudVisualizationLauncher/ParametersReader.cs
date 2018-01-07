﻿using Fclp;
using TagsCloudVisualization;

namespace TagsCloudVisualizationLauncher
{
    internal class ParametersReader
    {
        public Result<Parameters> ParseParameters(string[] parameters)
        {
            var parser = new FluentCommandLineParser<Parameters>();
            ArgParserConfigurator.ArgParserConfigurate(parser);
            var result = parser.Parse(parameters);

            if (result.HasErrors)
                return Result.Fail<Parameters>("One or more parameters was missed!");

            if (result.EmptyArgs || result.HelpCalled)
                return Result.Fail<Parameters>("Please enter parameters or configuration file!");

            return Result.Ok(parser.Object);
        }
    }
}
