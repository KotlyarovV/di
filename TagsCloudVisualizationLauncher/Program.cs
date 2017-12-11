using System;
using System.Windows.Forms;
using Autofac;
using Fclp;

namespace TagsCloudVisualization
{
    static class Program
    {
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
                ArgParserConfigurator.ArgParserConfigurate(parser);
                var result = parser.Parse(args);

                if (result.HasErrors)
                {
                    Console.WriteLine("One of the parameters was missed!");
                    return;
                }
                if (result.EmptyArgs || result.HelpCalled) return;

                var parametrs = parser.Object;
                var container = ConteinerConfigurator.ConfigureContainer(parametrs);
                var cloudPainter = container.Resolve<CloudPainter>();
                var textInputer = new FileTextInputer(parametrs.FileName);
                var text = textInputer.GetText();
                
                var bitmap = cloudPainter.GetBitmap(
                    text,
                    parametrs.Width,
                    parametrs.Height,
                    parametrs.FontSizeMin,
                    parametrs.FontSizeMax
                    );
                var saver = new Saver();
                saver.SaveBitmap(parametrs.ImageName, bitmap);
                
            }
        }
    }
}
