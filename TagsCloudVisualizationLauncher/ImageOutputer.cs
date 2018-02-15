using System.Drawing;
using System.IO;
using TagsCloudVisualization;

namespace TagsCloudVisualizationLauncher
{
    internal class ImageOutputer
    {
        private Result<None> CheckResults(
            Result<Parameters> parametersResult,
            Result<Bitmap> bitmapResult
            )
        {
            if (!parametersResult.IsSuccess)
                return Result.Fail<None>(parametersResult.Error);

            if (!bitmapResult.IsSuccess)
                return Result.Fail<None>(bitmapResult.Error);

            var imageFormatResult = parametersResult.GetValueOrThrow().GetImageFormat();
            if (!imageFormatResult.IsSuccess)
                return Result.Fail<None>(imageFormatResult.Error);

            return Result.Ok();
        }

        private Result<FileStream> GetSaveResult(Result<Parameters> parameterResult)
        {
            var parameters = parameterResult.GetValueOrThrow();
            FileStream outStream = null;

            return Result.Of(() =>
            outStream = new FileStream(
                parameters.ImageName,
                FileMode.Create
                ));
        }


        public Result<None> SaveImage(
            Result<Parameters> parametersResult, 
            Result<Bitmap> bitmapResult
            )
        {
            if(!CheckResults(parametersResult, bitmapResult).IsSuccess)
                return Result.Fail<None>(CheckResults(parametersResult, bitmapResult).Error);

            var outStreamCreationResult = GetSaveResult(parametersResult);

            if (!outStreamCreationResult.IsSuccess)
                return Result.Fail<None>($"Can't create {parametersResult.GetValueOrThrow().ImageName}");
            
            var imageFormat = parametersResult
                    .GetValueOrThrow()
                    .GetImageFormat()
                    .GetValueOrThrow();

            bitmapResult
                .GetValueOrThrow()
                .Save(outStreamCreationResult.GetValueOrThrow(), imageFormat);

            return Result.Ok();
        }
    }
}
