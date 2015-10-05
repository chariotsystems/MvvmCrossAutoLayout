using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore.UI;
using MvvmCrossAutoLayout.Core.Model;

namespace MvvmCrossAutoLayout.Core.ViewModels
{
	public class ScrollExampleViewModel : MvxViewModel
	{
		public MvxCommand<string> GotoSimpleView  { get; set; }

		public ScrollExampleViewModel ()
		{
			GotoSimpleView = new MvxCommand<string> (param => {
				ShowViewModel<SimpleExampleViewModel> ();
			});
		}


	}
}

