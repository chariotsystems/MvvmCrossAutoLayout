using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;
using UIKit;
using MvvmCrossAutoLayout.Core.Model;

/*
 * Author: Alex Eadie
 * https://github.com/chariotsystems/MvvmCrossAutoLayout
 * 
 * Using material from https://github.com/benhysell/V.MvvmCross.CustomCell
 */
namespace AutoLayout.Views
{
	[Register ("ListExampleTableRow")]
	public class ListExampleTableRow : MvxTableViewCell
	{
		public ListExampleTableRow ()
		{
			CreateLayout ();
		}

		public ListExampleTableRow (IntPtr handle)
			: base (handle)
		{
			CreateLayout ();
		}


		private void CreateLayout ()
		{
			var ProfileBorder = AutoLayoutContentView.CreateListContentRoot ("ListContentRoot", UIColor.LightGray, ContentView, "Helvetica-Bold"); 
			var Details = ProfileBorder.AddContainer ("Details", UIColor.White);
			var Photo = ProfileBorder.AddContainer ("Photo", UIColor.White);
			ProfileBorder.AddConstraint ("V:|-[Details(>=30)]-|");
			ProfileBorder.AddConstraint ("V:|-(>=2)-[Photo]-(>=2)-|");
			ProfileBorder.AddConstraint ("H:|-[Details]-(>=8)-[Photo]-26-|");
			Photo.AddImage ("Picture", "Alex.jpg");
			Photo.AddConstraint ("H:|[Picture(24)]|");
			Photo.AddConstraint ("V:|[Picture(32)]|");
			var name = Details.AddLabelLeft ("Name", "Alex Eadie", UIColor.Black, 10);
			var phone = Details.AddLabelLeft ("Phone", "0456 234 154", UIColor.Blue, 10);
			Details.AddConstraint ("V:|-(>=2)-[Name]-(>=2)-[Phone]-(>=2)-|");

			ContentView.AddSubviews (ProfileBorder);
			this.DelayBind (() => {
				var set = this.CreateBindingSet<ListExampleTableRow, ContactDetail> ();
				set.Bind (phone).To (vm => vm.PhoneNumber);
				set.Bind (name).To (vm => vm.Name);
				set.Apply ();
			});

		}

	}
}