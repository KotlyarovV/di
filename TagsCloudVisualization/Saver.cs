using System.Drawing;

namespace TagsCloudVisualization
{
    public class Saver
    {
        public void SaveBitmap(string name, Bitmap bitmap)
        {
            bitmap.Save(name);
        }
    }
}
