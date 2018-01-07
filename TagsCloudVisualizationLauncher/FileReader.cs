using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using TagsCloudVisualization;

namespace TagsCloudVisualizationLauncher
{
    public class FileReader
    {
        public Result<string> GetText(string fileName)
        {
            if (!File.Exists(fileName))
                return Result.Fail<string>($"File {fileName} doesn't exist");

            FileStream inputStream = null;
            var openResult = Result.Of(() => inputStream = new FileStream(fileName, FileMode.Open));

            if (!openResult.IsSuccess)
                return Result.Fail<string>($"File {fileName} can't be used");

            string text;

            using (var reader = new StreamReader(inputStream, Encoding.Default))
            {
                text = reader.ReadToEnd();
            }

            return Result.Ok(text);
        }
    }
}
