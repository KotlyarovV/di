using Autofac;
using System;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization;

namespace TagsCloudVisualizationLauncher
{
    static class Program
    {

        private static Result<string[]> GetArgsFromFile(string fileName)
        {
            var fileReader = new FileReader();
            var configResult = fileReader.GetText(fileName);

            return (configResult.IsSuccess) ? 
                Result.Ok(configResult.GetValueOrThrow().Split()) : 
                Result.Fail<string[]>(configResult.Error);

        }

        private static Result<None> SaveCloud(string[] args)
        {
            var parametersReader = new ParametersReader();
            var paramsResult = parametersReader.ParseParameters(args);

            var cloudBuilder = new CloudBuilder();
            var bitmapResult = cloudBuilder.TryBuildCloud(paramsResult);

            var outPuter = new ImageOutputer();
            return outPuter.SaveImage(
                    paramsResult,
                    bitmapResult
                    );
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
                if (args.Length == 2 && args[0] == "--conf")
                {
                    var argsResult = GetArgsFromFile(args[1]);                    
                    if (!argsResult.IsSuccess)
                    {
                        Console.WriteLine(argsResult.Error);
                        return;
                    }
                }

                var saveResult = SaveCloud(args);
                if (!saveResult.IsSuccess)
                {
                    Console.WriteLine(saveResult.Error);
                    return;
                }

                Console.WriteLine("File was saved");
            }
        }
    }
}
