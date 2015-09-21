using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.FieldBinding;
using Cirrious.MvvmCross.ViewModels;
using MvvmCrossAutoLayout.Model;
using MvvmCrossAutoLayout.Interfaces;

namespace MvvmCrossAutoLayout.ViewModels
{
	public class ContactsListViewModel : MvxViewModel, IRemove
	{

		public List<ContactDetail> Contacts { get; set; }

		public ContactsListViewModel ()
		{
			ReloadCommand = new MvxCommand (RefreshHoursFromTable);
		}

		public System.Windows.Input.ICommand RemoveCommand {
			get { throw new System.NotImplementedException (); }
		}

		public System.Windows.Input.ICommand SelectedCommand {
			get { throw new System.NotImplementedException (); }
		}

		public ICommand ReloadCommand { get; set; }

		/// <summary>
		/// Pull to refresh support
		/// </summary>
		private async void RefreshHoursFromTable ()
		{
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

			RaiseAllPropertiesChanged ();
		}
	}
}

