﻿using System.Drawing;
using System.IO;
using Autofac;
using TagsCloudVisualization;
using TagsCloudVisualization.Extensions;
using YandexMystem.Wrapper;
using YandexMystem.Wrapper.Enums;

namespace TagsCloudVisualizationLauncher
{
    public static class ConteinerConfigurator
    {
        public const string OutStreamName = "outStreamName";
        public const string InputStreamName = "inputStreamName";

        public static IContainer ConfigureContainer(Parameters parameters)
        {
            
            var chars = new [] {'!', ',', '"', '\r', '\n'};

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

            var builder = new ContainerBuilder();
            builder.RegisterType<Mysteam>().As<Mysteam>().SingleInstance();

            builder.RegisterType<ArchimedeanSpiral>()
                .As<ISpiral>()
                .WithParameter(new TypedParameter(typeof(Point), parameters.Size.GetCenter()));
            
            builder.RegisterType<Analysator>()
                .As<IAnalysator>();

            builder.RegisterType<Filter>()
                .As<IFilter>()
                .WithParameter( 
                    new TypedParameter(typeof(GramPartsEnum[]), excludedGramParts)
                    );

            builder
                .RegisterType<WordExtractor>()
                .As<IWordExtractor>();

            builder
                .RegisterType<TextCleaner>()
                .As<ITextCleaner>()
                .WithParameters(new[] {
                    new TypedParameter(typeof(char[]), chars),
                });

            builder
                .RegisterType<Formatter>()
                .As<IFormatter>();

            builder
                .RegisterType<CircularCloudLayouter>()
                .As<ICloudLayouter>();

            builder
                .RegisterType<TextVisualisator>()
                .As<ITextVisualisator>()
                .WithParameter(
                    new TypedParameter(typeof(Color[]), colors)
                );
            
            builder
                .RegisterType<FileStream>()
                .Named<Stream>(OutStreamName)
                .WithParameters(
                    new []
                    {
                        new TypedParameter(typeof(string), parameters.ImageName),
                        new TypedParameter(typeof(FileMode), FileMode.Create)
                    });
                
            builder
                .RegisterType<FileStream>()
                .Named<Stream>(InputStreamName)
                .WithParameters(
                    new []
                    {
                        new TypedParameter(typeof(string), parameters.FileName),
                        new TypedParameter(typeof(FileMode), FileMode.Open)
                    }
                );

            builder.RegisterType<CloudPainter>().As<ICloudPainter>();
            return builder.Build();
        }
    }
}
