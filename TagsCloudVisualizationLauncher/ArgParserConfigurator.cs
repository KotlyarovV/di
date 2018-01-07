using System;
using Fclp;

namespace TagsCloudVisualizationLauncher
{
    internal static class ArgParserConfigurator
    {
        //Tagscloudvisualization.exe --wh 3000 --ht 3000 --filename text.txt --fontmin 20 --fontmax 50 --imagename cloud.png
        public static void ArgParserConfigurate(IFluentCommandLineParser<Parameters> parser)
        {
            parser.Setup(arg => arg.FileName)
                .As("filename")
                .Required();

            parser.Setup(arg => arg.ImageName)
                .As("imagename")
                .Required();

            parser.Setup(arg => arg.Width)
                .As("wh")
                .Required();

            parser.Setup(arg => arg.Height)
                .As("ht")
                .Required();

            parser.Setup(arg => arg.FontSizeMax)
                .As("fontmax")
                .Required();

            parser.Setup(arg => arg.FontName)
                .As("fontname");


            parser.Setup(arg => arg.FontSizeMin)
                .As("fontmin")
                .Required();


            parser.SetupHelp("?", "help")
                .Callback((help) => Console.WriteLine());
        }
    }
}
