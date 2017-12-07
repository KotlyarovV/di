using System.IO;
using System.Text;

namespace TagsCloudVisualization
{
    class FileTextInputer
    {
        private readonly string textName;

        public FileTextInputer(string fileName)
        {
            textName = fileName;
        }

        public string GetText() => File.ReadAllText(textName, Encoding.Default);
    }
}
