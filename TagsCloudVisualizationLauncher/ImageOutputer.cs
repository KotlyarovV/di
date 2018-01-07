using System.Drawing;
using System.IO;
using TagsCloudVisualization;

namespace TagsCloudVisualizationLauncher
{
    class ImageOutputer
    {
        public Result<None> SaveImage(Parameters parameters, Result<Bitmap> bitmapResult)
        {
            if (!bitmapResult.IsSuccess)
                return Result.Fail<None>(bitmapResult.Error);

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


            bitmapResult
                .GetValueOrThrow()
                .Save(outStream, imageFormatResult.GetValueOrThrow());

            return Result.Ok();
        }
    }
}
