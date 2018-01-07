using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using YandexMystem.Wrapper.Enums;

namespace TagsCloudVisualization
{
    public class CloudPainter
    {
        private readonly IAnalysator lexicAnalysator;
        private readonly ITextVisualisator textVisualisator;
        private readonly Func<string, Result<Word[]>> extractWords;
        private readonly Func<string, string> removeSigns;
        private readonly Func<Word, bool> isNecessaryPartOfSpeech;
        private readonly Func<Word, string> formatWord;
        private readonly Func<IEnumerable<Size>, IEnumerable<Rectangle>> putRectangles;

        public CloudPainter(
            Func<string, Result<Word[]>> extractWords,
            Func<Word, bool> isNecessaryPartOfSpeech,
            Func<Word, string> formatWord,
            IAnalysator lexicAnalysator,
            ITextVisualisator textVisualisator,
            Func<IEnumerable<Size>, IEnumerable<Rectangle>> putRectangles,
            Func<string, string> removeSigns
            )
        {
            this.extractWords = extractWords;
            this.lexicAnalysator = lexicAnalysator;
            this.textVisualisator = textVisualisator;
            this.removeSigns = removeSigns;
            this.isNecessaryPartOfSpeech = isNecessaryPartOfSpeech;
            this.formatWord = formatWord;
            this.putRectangles = putRectangles;
        }


        private Result<string[]> GetWords(string text)
        {
            var textWithoutSigns = removeSigns(text);

            var extractWordsResult = extractWords(textWithoutSigns)
                .Then(wordsArray => wordsArray.Where(isNecessaryPartOfSpeech))
                .Then(filteredWords => filteredWords.Select(formatWord).ToArray());
            
            return extractWordsResult;
        }


        private Result<TextImage[]> GetStringImages(
            string text, 
            double minFont = 1.0, 
            double maxFont = 10.0, 
            string fontName = "Arial"
            )
        {
            var wordsResult = GetWords(text);
            if (!wordsResult.IsSuccess)
                return new Result<TextImage[]>(wordsResult.Error);

            var weights = lexicAnalysator.GetWeights(wordsResult.Value);

            return textVisualisator
                .CreateTextImages(weights)
                .Then((visualisator) => visualisator.SetFontSizes(minFont, maxFont))
                .Then((visualisator) => visualisator.SetColors())
                .Then((visualisator) => visualisator.SetFontTipe(fontName))
                .Then((visualisator) => visualisator.GetStringImages());
        }

        private bool RectanglesInBitmap(Bitmap bitmap, IEnumerable<Rectangle> rectangles)
        {
            var sortedByXPoints = rectangles
                .Select(rectangle => rectangle.Location)
                .OrderBy(location => location.X);

            var mostLeftPoint = sortedByXPoints.First();
            var mostRightPoint = sortedByXPoints.Last();

            var sortedByYPoints = rectangles
                .Select(rectangle => rectangle.Location)
                .OrderBy(location => location.Y);

            var mostTopPoint = sortedByYPoints.Last();
            var mostBottomPoint = sortedByYPoints.First();

            return mostLeftPoint.X > 0
                   && mostRightPoint.X < bitmap.Width
                   && mostTopPoint.Y < bitmap.Height
                   && mostBottomPoint.Y > 0;
        }

        public Result<Bitmap> GetBitmap(
            string text, 
            int width = 100,
            int height = 100,
            double minFont = 1.0,
            double maxFont = 10.0,
            string fontName = "Arial"
            )
        {
            var bitmap = new Bitmap(width, height);
            var graphics = Graphics.FromImage(bitmap);
            var textImagesResult = GetStringImages(text, minFont, maxFont, fontName);

            if (!textImagesResult.IsSuccess)
                return Result.Fail<Bitmap>(textImagesResult.Error);

            var textImages = textImagesResult.Value
                .OrderBy(stringImage => - stringImage.Size.Width * stringImage.Size.Height);
             
            var flags = TextFormatFlags.NoPadding | TextFormatFlags.NoClipping;

            var rectangles = putRectangles(textImages.Select(textImage => textImage.Size));

            
            if (!RectanglesInBitmap(bitmap, rectangles))
                return Result.Fail<Bitmap>("Too little bitmap for words");

            var rectangleEnumerator = rectangles.GetEnumerator();
            
            foreach (var textImage in textImages)
            {
                rectangleEnumerator.MoveNext();
                var rectangle = rectangleEnumerator.Current;

                TextRenderer.DrawText(
                    graphics, 
                    textImage.Text, 
                    textImage.Font,
                    rectangle.Location, 
                    textImage.Color, 
                    flags
                    );
            }

            return bitmap;
        }        
    }
}
