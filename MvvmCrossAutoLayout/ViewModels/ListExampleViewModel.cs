using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.FieldBinding;
using Cirrious.MvvmCross.ViewModels;
using MvvmCrossAutoLayout.Core.Model;
using MvvmCrossAutoLayout.Core.Interfaces;

/*
 * Author: Alex Eadie
 * https://github.com/chariotsystems/MvvmCrossAutoLayout
 */
namespace MvvmCrossAutoLayout.Core.ViewModels
{
	public class ListExampleViewModel : MvxViewModel, IMvxTableRowCommands
	{
		/// <summary>
		/// busy indicator for table refreshes
		/// </summary>
		public INC<bool> IsBusy = new NC<bool> ();

		public List<ContactDetail> Contacts { get; set; }

		public ListExampleViewModel ()
		{
			ReloadCommand = new MvxCommand (RefreshDataForTable);

			SelectedCommand = new MvxCommand<int> (rowId => {
				int x = rowId;
				// TODO: navigate to detail of this row.
			});

			RemoveCommand = new MvxCommand<int> (rowId => {
				int x = rowId;
			});

			RefreshDataForTable ();

		}

		public ICommand RemoveCommand  { get; set; }

		public ICommand SelectedCommand  { get; set; }

		public ICommand ReloadCommand { get; set; }

		/// <summary>
		/// Pull to refresh support
		/// </summary>
		private async void RefreshDataForTable ()
		{
			IsBusy.Value = true;
			Contacts = new List<ContactDetail> (); 
			var entry = new ContactDetail () {
				Name = "Alex",
				Email = "alex@gmail.com",
				PhoneNumber = "0426 565 787"
			};

			var entry2 = new ContactDetail () {
				Name = "Jane",
				Email = "jane@gmail.com",
				PhoneNumber = "0427 565 787"
			};
			var entry3 = new ContactDetail () {
				Name = "Alice",
				Email = "alice@gmail.com",
				PhoneNumber = "0428 565 787"
			};
			Contacts.Add (entry);
			Contacts.Add (entry2);
			Contacts.Add (entry3);
			IsBusy.Value = false;
			RaiseAllPropertiesChanged ();
		}
	}
}

