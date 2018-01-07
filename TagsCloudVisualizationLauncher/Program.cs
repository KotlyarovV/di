using System;
using System.Windows.Forms;


namespace TagsCloudVisualizationLauncher
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
                if (args.Length == 2 && args[0] == "--conf")
                {
                    var fileReader = new FileReader();
                    var configResult = fileReader.GetText(args[1]);
                    if (!configResult.IsSuccess)
                    {
                        Console.WriteLine(configResult.Error);
                        return;
                    }
                    args = configResult.GetValueOrThrow().Split();
                }

                var parametersReader = new ParametersReader();
                var paramsResult = parametersReader.ParseParameters(args);
                
                var cloudBuilder = new CloudBuilder();
                var bitmapResult = cloudBuilder.TryBuildCloud(paramsResult);

                var outPuter = new ImageOutputer();
                var saveResult = outPuter.SaveImage(
                        paramsResult.GetValueOrThrow(), 
                        bitmapResult
                        );

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
