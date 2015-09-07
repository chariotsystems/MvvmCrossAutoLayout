using Cirrious.MvvmCross.ViewModels;

namespace MvvmCrossAutoLayout.ViewModels
{
	public class TicketViewModel 
		: MvxViewModel
	{
		public MvxCommand<string> TestCommand { 
			get { return new MvxCommand<string> (Test); } 
		}


		private string _hello = "Hello Tocklet";

		public string Hello { 
			get { return _hello; }
			set {
				_hello = value;
				RaisePropertyChanged (() => Hello);
			}
		}

		public void Test (string commandParameter)
		{
			Hello = commandParameter;
		}
	}
}
