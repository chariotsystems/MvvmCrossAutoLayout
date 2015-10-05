using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Touch.Views;
using UIKit;
using Foundation;
using MvvmCrossAutoLayout.Core.Interfaces;
using AutoLayout.Views;


namespace AutoLayout.iOS.Helpers
{
	public class ListExampleTableViewSource : MvxTableViewSource
	{


		private IMvxTableRowCommands viewModel;

		public ListExampleTableViewSource (IMvxTableRowCommands viewModel, UITableView tableView)
			: base (tableView)
		{
			this.viewModel = viewModel;
			// Warning this string must line up with the registered string for the row class.
			tableView.RegisterClassForCellReuse (typeof(ListExampleTableRow), 
				new NSString ("ListExampleTableRow"));
		}



		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}


		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			switch (editingStyle) {
			case UITableViewCellEditingStyle.Delete:
				viewModel.RemoveCommand.Execute (indexPath.Row);
				break;
			case UITableViewCellEditingStyle.None:
				break;
			}
		}

		public override UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return UITableViewCellEditingStyle.Delete;
		}

		public override bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
		{
			return false;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			viewModel.SelectedCommand.Execute (indexPath.Row);
		}

		protected override UITableViewCell GetOrCreateCellFor (UITableView tableView, NSIndexPath indexPath, object item)
		{
			// Warning this string must line up with the registered string for the row class.
			return (ListExampleTableRow)
				tableView.DequeueReusableCell ("ListExampleTableRow");
		}
	}
}

