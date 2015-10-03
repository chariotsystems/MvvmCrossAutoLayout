using Cirrious.CrossCore.IoC;

namespace MvvmCrossAutoLayout.Data
{
	public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
	{
		public override void Initialize ()
		{
			CreatableTypes ()
                .EndingWith ("Service")
                .AsInterfaces ()
                .RegisterAsLazySingleton ();
				
			RegisterAppStart<MvvmCrossAutoLayout.Core.ViewModels.SimpleExampleViewModel> ();
		}
	}
}