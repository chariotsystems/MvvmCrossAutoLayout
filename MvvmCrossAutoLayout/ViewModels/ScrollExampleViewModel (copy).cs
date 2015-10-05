using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore.UI;
using MvvmCrossAutoLayout.Core.Model;

namespace MvvmCrossAutoLayout.Core.ViewModels
{
	public class SimpleExampleViewModel : MvxViewModel
	{


		private MvxColor _BackgroundColor = MvxColors.Cornsilk;

		public MvxColor BackgroundColor {
			get { return _BackgroundColor; }

			set {
				_BackgroundColor = value;
				RaisePropertyChanged (() => BackgroundColor);
			}
		}

		private MvxColor _TextColor = MvxColors.Cyan;

		public MvxColor TextColor {
			get { return _TextColor; }
			set {
				_TextColor = value;
				RaisePropertyChanged (() => TextColor);
			}
		}


		private ContactDetail _ContactDetail = new ContactDetail ();

		public ContactDetail ContactDetail { 
			get { return _ContactDetail; }
			set {
				_ContactDetail = value;
				RaisePropertyChanged (() => ContactDetail);
			}
		}

		//		public MvxCommand<string> TestCommand {
		//			get { return new MvxCommand<string> (Test); }
		//		}

		//		public void Test (string commandParameter)
		//		{
		//			// Don't call the individual set methods as the action of firing RaisePropertyChanged for each one
		//			// causes some kind of race condition;
		//			_hello = commandParameter;
		//			RaiseAllPropertiesChanged ();
		//		}
	}
}
