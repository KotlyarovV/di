using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure;
using Ninject;

namespace FractalPainting.App.Actions
{
	public class DragonFractalAction : IUiAction
	{
		private IImageHolder imageHolder;
        
	    public DragonFractalAction(IImageHolder imageHolder)
	    {
	        this.imageHolder = imageHolder;
	    }

		public string Category => "Фракталы";
		public string Name => "Дракон";
		public string Description => "Дракон Хартера-Хейтуэя";

		public void Perform()
		{
			var dragonSettings = CreateRandomSettings();
			// редактируем настройки:
			SettingsForm.For(dragonSettings).ShowDialog();
			// создаём painter с такими настройками
			var container = new StandardKernel();
			container.Bind<IImageHolder>().ToConstant(imageHolder);
			container.Bind<DragonSettings>().ToConstant(dragonSettings);
			container.Get<DragonPainter>().Paint();
		}

		private static DragonSettings CreateRandomSettings()
		{
			return new DragonSettingsGenerator(new Random()).Generate();
		}
	}
}