using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore.UI;
using MvvmCrossAutoLayout.Core.Model;
using System.Windows.Input;

/*
 * Author: Alex Eadie
 * https://github.com/chariotsystems/MvvmCrossAutoLayout
 */
namespace MvvmCrossAutoLayout.Core.ViewModels
{
	public class SimpleExampleViewModel : MvxViewModel
	{
		public MvxCommand<string> GotoScrollView  { get; set; }

		public MvxCommand<string> GotoListView  { get; set; }

		public SimpleExampleViewModel ()
		{
			GotoScrollView = new MvxCommand<string> (param => {
				ShowViewModel<ScrollExampleViewModel> ();
			});
			GotoListView = new MvxCommand<string> (param => {
				ShowViewModel<ListExampleViewModel> ();
			});
		}


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

	}
}
