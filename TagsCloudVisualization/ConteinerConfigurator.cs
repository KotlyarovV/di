using System.Drawing;
using Autofac;
using YandexMystem.Wrapper;

namespace TagsCloudVisualization
{
    public static class ConteinerConfigurator
    {
        public static IContainer ConfigureContainer(Parameters parameters)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<PngSaver>().As<ISaver>();

            builder.RegisterType<ArchimedeanSpiral>()
                .As<ISpiral>()
                .WithParameter(new TypedParameter(typeof(Point), parameters.Size.GetCenter()));

            builder.RegisterType<LexicAnalysator>()
                .As<IAnalysator>()
                .WithParameter(new TypedParameter(typeof(Mysteam), new Mysteam()));

            builder.RegisterType<CircularCloudLayouter>()
                .As<ICloudLayouter>();

            builder.RegisterType<TextVisualisator>()
                .As<ITextVisualisator>();

            builder.RegisterType<FileTextInputer>()
                .As<IInputer>()
                .WithParameter(new TypedParameter(typeof(string), parameters.FileName));

            builder.RegisterType<CloudPainter>();
            return builder.Build();
        }
    }
}
