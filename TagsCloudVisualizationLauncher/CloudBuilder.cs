using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudVisualizationLauncher
{
    internal class CloudBuilder
    {
        public Result<Bitmap> TryBuildCloud(Result<Parameters> parametersResult)
        {
            if (!parametersResult.IsSuccess)
                return Result.Fail<Bitmap>(parametersResult.Error);

            var parameters = parametersResult.GetValueOrThrow();

            var fileReader = new FileReader();
            var textResult = fileReader.GetText(parameters.FileName);

            if (!textResult.IsSuccess)
                return Result.Fail<Bitmap>(textResult.Error);

            var cloudPainter = CloudConfigurator.ConfigureCloud(parameters);

            if (parameters.Width <= 0 || parameters.Height <= 0)
                return Result.Fail<Bitmap>("Sizes must be more then zero!");

            if (parameters.FontSizeMax <= 0 || parameters.FontSizeMin <= 0 
                || parameters.FontSizeMin > parameters.FontSizeMax)
                return Result.Fail<Bitmap>("Wrong sizes of font!");

            return cloudPainter.GetBitmap(
                textResult.GetValueOrThrow(),
                parameters.Width,
                parameters.Height,
                parameters.FontSizeMin,
                parameters.FontSizeMax
            );

        }
    }
}
