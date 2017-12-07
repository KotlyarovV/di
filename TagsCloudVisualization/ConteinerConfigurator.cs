using System.Drawing;
using Autofac;
using TagsCloudVisualization.Extensions;
using YandexMystem.Wrapper;
using YandexMystem.Wrapper.Enums;

namespace TagsCloudVisualization
{
    public static class ConteinerConfigurator
    {
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
        
            var mysteam = new Mysteam();
            var colors = new[] { Color.Blue, Color.Brown, Color.Coral, Color.DarkGreen, };

            var builder = new ContainerBuilder();

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


            builder.RegisterType<Splitter>()
                .As<ISplitter>()
                .WithParameters(new[] {
                    new TypedParameter(typeof(char[]), chars),
                    new TypedParameter(typeof(Mysteam), mysteam)
                });

            builder.RegisterType<Formatter>()
                .As<IFormatter>();

            builder.RegisterType<CircularCloudLayouter>()
                .As<ICloudLayouter>();

            builder.RegisterType<TextVisualisator>()
                .As<ITextVisualisator>()
                .WithParameter(
                    new TypedParameter(typeof(Color[]), colors)
                );                

            builder.RegisterType<CloudPainter>();
            return builder.Build();
        }
    }
}
