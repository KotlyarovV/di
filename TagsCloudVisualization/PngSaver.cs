using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization
{
    class PngSaver : ISaver
    {
        public void SaveBitmap(Bitmap bitmap)
        {
            bitmap.Save("circularCloud.png", ImageFormat.Png);
        }
    }
}
