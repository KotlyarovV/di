using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization;
using TagsCloudVisualization.Extensions;
using YandexMystem.Wrapper.Enums;

namespace TagsCloudVisualizationLauncher
{
    internal static class CloudConfigurator
    {
        public static CloudPainter ConfigureCloud(Parameters parameters)
        {
            
            var chars = new [] {'!', ',', '"', '\r', '\n', ';'};

            var excludedGramParts = new []
            {
                GramPartsEnum.Conjunction,
                GramPartsEnum.NounPronoun,
                GramPartsEnum.Pretext,
                GramPartsEnum.Part,
                GramPartsEnum.PronounAdjective
            };
        
            var colors = new[]
            {
                Color.Crimson,
                Color.DarkSalmon,
                Color.IndianRed,
                Color.Plum,
                Color.Violet,
                Color.MediumOrchid
            };

            var filter = new Filter();
            var wordExtractor = new WordExtractor();
            var formatter = new Formatter();
            var textCleaner = new TextCleaner();

            Func<IEnumerable<PointF>> getPoints = () => (new ArchimedeanSpiral())
                .GetSpiralPoints(parameters.Size.GetCenter());

            var circularCloudLayouter = new CircularCloudLayouter();

            var cloudPainter = new CloudPainter(
                (str) => wordExtractor.ExtractWords(str),
                (word) => filter.IsNecessaryPartOfSpeech(excludedGramParts, word),
                (word) => formatter.GetOriginal(word),
                new Analysator(),
                new TextVisualisator(colors),
                sizes => circularCloudLayouter.GetCloudRectangles(sizes, getPoints),
                (str) => textCleaner.RemoveSigns(chars, str));
            
            return cloudPainter;
        }
    }
}
