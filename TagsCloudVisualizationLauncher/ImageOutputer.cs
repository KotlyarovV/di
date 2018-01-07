using System.Drawing;
using System.IO;
using TagsCloudVisualization;

namespace TagsCloudVisualizationLauncher
{
    internal class ImageOutputer
    {
        public Result<None> SaveImage(
            Result<Parameters> parametersResult, 
            Result<Bitmap> bitmapResult
            )
        {
            if(!parametersResult.IsSuccess)
                return Result.Fail<None>(parametersResult.Error);

            if (!bitmapResult.IsSuccess)
                return Result.Fail<None>(bitmapResult.Error);

            var parameters = parametersResult.GetValueOrThrow();
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
