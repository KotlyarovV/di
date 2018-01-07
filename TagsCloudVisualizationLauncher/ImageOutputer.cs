using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization;

namespace TagsCloudVisualizationLauncher
{
    class ImageOutputer
    {
        public Result<None> SaveImage(Parameters parameters, Bitmap bitmap)
        {
            FileStream outStream = null;

            var outStreamCreationResult = Result.Of(() => 
            outStream = new FileStream(
                parameters.ImageName,
                FileMode.Create
                ));

            if (!outStreamCreationResult.IsSuccess)
                return Result.Fail<None>($"Can't create {parameters.ImageName}");

            var imageFormatResult = parameters.GetImageFormat();
            if (!imageFormatResult.IsSuccess)
                return Result.Fail<None>(imageFormatResult.Error);


            bitmap.Save(outStream, imageFormatResult.GetValueOrThrow());
            return Result.Ok();
        }
    }
}
