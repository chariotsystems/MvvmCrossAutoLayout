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
using MvvmCrossAutoLayout.Core.ViewModels;



using Cirrious.MvvmCross.Touch.Views;

/*
 * Author: Alex Eadie
 * https://github.com/chariotsystems/MvvmCrossAutoLayout
 * 
 * Using material from https://github.com/benhysell/V.MvvmCross.CustomCell
 */
namespace AutoLayout.Views
{

	[Register ("ListExampleView")]
	public class ListExampleView : MvxViewController
	{
		private ListExampleViewModel viewModel;

		public new ListExampleViewModel ViewModel {
			get { return viewModel ?? (viewModel = base.ViewModel as ListExampleViewModel); }
		}

		public override void ViewDidLoad ()
		{
			this.View = new UIView { BackgroundColor = UIColor.White };

			base.ViewDidLoad ();

			var Set = this.CreateBindingSet<ListExampleView, ListExampleViewModel> ();
			var Root = AutoLayoutContentView.CreateRoot ("Root", UIColor.DarkGray, "Helvetica-Bold");
			var ListPanel = Root.AddContainer ("ListPanel", UIColor.White);
			var OuterLabel = Root.AddLabel ("OuterLabel", "The Beautiful App", UIColor.White, 12);
			Root.AddConstraint ("V:|-24-[OuterLabel]-[ListPanel]-|");
			Root.AddConstraint ("H:|-[OuterLabel]");
			Root.AddConstraint ("H:|-4-[ListPanel]-4-|");
			var InnerLabel = ListPanel.AddLabel ("InnerLabel", "Contacts List", UIColor.Black, 12);
			var DataTable = ListPanel.AddTableView ("DataTable");
			ListPanel.AddConstraint ("H:|-[InnerLabel]-(>=8)-|");
			ListPanel.AddConstraint ("V:|-16-[InnerLabel]-[DataTable]-|");
			ListPanel.AddConstraint ("H:|-[DataTable]-|");
			var listExampleTableSource = new ListExampleTableViewSource (ViewModel, DataTable);
			var refreshControl = new MvxUIRefreshControl ();
			DataTable.AddSubview (refreshControl);
			DataTable.Source = listExampleTableSource;
			DataTable.ReloadData ();

			View = Root;

			Set.Bind (listExampleTableSource).To (vm => vm.Contacts);
			Set.Bind (refreshControl).For (r => r.RefreshCommand).To (vm => vm.ReloadCommand);
			Set.Bind (refreshControl).For (r => r.IsRefreshing).To (vm => vm.IsBusy);
			Set.Apply ();          
		}
	}
}