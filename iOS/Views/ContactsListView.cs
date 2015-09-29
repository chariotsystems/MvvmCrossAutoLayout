// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the FirstView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Drawing;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.BindingContext;
using Foundation;
using UIKit;
using AutoLayout.iOS.Helpers;
using MvvmCrossAutoLayout.ViewModels;


//https://github.com/benhysell/V.MvvmCross.CustomCell
using Cirrious.MvvmCross.Touch.Views;


namespace AutoLayout.Views
{

	[Register ("ContactsListView")]
	public class ContactsListView : MvxViewController
	{
		private ContactsListViewModel viewModel;

		public new ContactsListViewModel ViewModel {
			get { return viewModel ?? (viewModel = base.ViewModel as ContactsListViewModel); }
		}

		public override void ViewDidLoad ()
		{
			this.View = new UIView { BackgroundColor = UIColor.White };

			base.ViewDidLoad ();

			var Set = this.CreateBindingSet<ContactsListView, ContactsListViewModel> ();
			var Root = AutoLayoutContentView.CreateRoot ("Root", UIColor.DarkGray, "Helvetica-Bold");
			var ListPanel = Root.AddContainer ("ListPanel", UIColor.White);
			var Name = Root.AddLabel ("Name", "List Title", UIColor.Blue, 12);
			Root.AddConstraint ("V:|-16-[Name]-[ListPanel]-|");
			Root.AddConstraint ("H:|-40-[Name]-40-|");
			Root.AddConstraint ("H:|-4-[ListPanel]-4-|");
			var Rabbit = ListPanel.AddLabelLeft ("Rabbit", "Rabbit is here", UIColor.Green, 12);
//			var hoursTable = ListPanel.AddTableView ("HoursTable");
//			ListPanel.AddConstraint ("V:|-16-[Rabbit]-[HoursTable]-|");
//			ListPanel.AddConstraint ("H:|-[HoursTable]-|");
//			var hoursTableSource = new ContactsListTableViewSource (ViewModel, hoursTable);
//			var refreshControl = new MvxUIRefreshControl ();
//			hoursTable.AddSubview (refreshControl);
//			hoursTable.Source = hoursTableSource;
//			hoursTable.ReloadData ();
//
//			View = Root;
//
//			var set = this.CreateBindingSet<ContactsListView, ContactsListViewModel> ();
//			set.Bind (hoursTableSource).To (vm => vm.Contacts);
//			set.Bind (refreshControl).For (r => r.RefreshCommand).To (vm => vm.ReloadCommand);
//			set.Bind (refreshControl).For (r => r.IsRefreshing).To (vm => vm.IsBusy);
//			set.Apply ();          
		}
	}
}