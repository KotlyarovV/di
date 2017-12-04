using System.IO;
using System.Text;

namespace TagsCloudVisualization
{
    class FileTextInputer : IInputer
    {
        private readonly string text;

        public FileTextInputer(string fileName)
        {
            text = File.ReadAllText(fileName, Encoding.Default);
        }

        public string GetText() => text;
    }
}
