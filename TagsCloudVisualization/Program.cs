using System;
using System.Drawing;
using System.Windows.Forms;
using Autofac;
using Fclp;

namespace TagsCloudVisualization
{
    static class Program
    {
        
        //Tagscloudvisualization.exe --wh 3000 --ht 3000 --filename battle.txt --fontmin 20 --fontmax 50
        static void PrepareParser(FluentCommandLineParser<Parameters> parser)
        {
            parser.Setup(arg => arg.FileName)
                .As("filename");

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

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {


            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ParametersForm());
            }

            else
            {
                var parser = new FluentCommandLineParser<Parameters>();
                PrepareParser(parser);
                var result = parser.Parse(args);

                if (result.HasErrors)
                {
                    Console.WriteLine("One of the parameters was missed!");
                    return;
                }
                if (result.EmptyArgs || result.HelpCalled) return;

                var arguments = parser.Object;
                var container = ConteinerConfigurator.ConfigureContainer(arguments);
                var cloudPainter = container.Resolve<CloudPainter>();
                Bitmap bitmap = cloudPainter.GetBitmap(arguments);
                cloudPainter.SaveBitmap(bitmap);
                
            }
        }
    }
}
