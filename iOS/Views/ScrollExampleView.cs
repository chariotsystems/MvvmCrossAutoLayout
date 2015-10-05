using System;
using UIKit;
using System.Drawing;
using System.Collections.Generic;
using Foundation;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using MvvmCrossAutoLayout.Core.ViewModels;
using ObjCRuntime;
using MvvmCrossAutoLayout.iOS;
using CoreGraphics;

namespace AutoLayout
{

	[Register ("ScrollExampleView")]
	public class ScrollExampleView : MvxViewController
	{
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			NavigationController.NavigationBarHidden = true;
			View = BuildView ();
		}

		public float LabelTextSize = 12;
		public float DataTextSize = 20;

		private UIView BuildView ()
		{
			var Set = this.CreateBindingSet<ScrollExampleView, ScrollExampleViewModel> ();

			var Root = AutoLayoutContentView.CreateRoot ("Root", UIColor.DarkGray, "Helvetica-Bold");
			var ScrollView = Root.AddScrollView ("ScrollView", UIColor.DarkGray);

			var ContactBorder1 = ScrollView.AddContainer ("ContactBorder1", UIColor.White);
			var ContactBorder2 = ScrollView.AddContainer ("ContactBorder2", UIColor.White);
			var ContactBorder3 = ScrollView.AddContainer ("ContactBorder3", UIColor.White);
			var ContactBorder4 = ScrollView.AddContainer ("ContactBorder4", UIColor.White);
			ScrollView.AddConstraint ("V:|-16-[ContactBorder1]-[ContactBorder2]-[ContactBorder3]-[ContactBorder4]-(>=100)-|");
			ScrollView.AddConstraint ("H:|-4-[ContactBorder1]-4-|");
			ScrollView.AddConstraint ("H:|-4-[ContactBorder2]-4-|");
			ScrollView.AddConstraint ("H:|-4-[ContactBorder3]-4-|");
			ScrollView.AddConstraint ("H:|-4-[ContactBorder4]-4-|");

			SetCustomerAndContact (ContactBorder1);
			SetCustomerAndContact (ContactBorder2);
			SetCustomerAndContact (ContactBorder3);
			SetCustomerAndContact (ContactBorder4);

			return Root;
		}

		void SetCustomerAndContact (AutoLayoutContentView ContactBorder)
		{
			var Customer = ContactBorder.AddContainer ("Customer", UIColor.White);
			var Contact = ContactBorder.AddContainer ("Contact", UIColor.White);
			ContactBorder.AddConstraint ("V:|-[Customer]-25-[Contact]|");
			ContactBorder.AddConstraint ("H:|-[Customer]-(>=8)-|");
			ContactBorder.AddConstraint ("H:|-[Contact]-(>=8)-|");
			SetCustomer (Customer);
			SetContact (Contact);
		}

		void SetCustomer (AutoLayoutContentView Customer)
		{
			Customer.AddLabel ("CustomerLabel", "CUSTOMER", UIColor.LightGray, LabelTextSize);
			var name = Customer.AddLabel ("CustomerName", "Alex Eadie", UIColor.Black, DataTextSize);
			Customer.AddConstraint ("V:|-18-[CustomerLabel]-7-[CustomerName]|");
			Customer.AddConstraint ("H:|-19-[CustomerLabel]-(>=8)-|");
			Customer.AddConstraint ("H:|-19-[CustomerName]-(>=8)-|");
		}

		void SetContact (AutoLayoutContentView Contact)
		{
			Contact.AddLabel ("ContactLabel", "CONTACT", UIColor.LightGray, LabelTextSize);
			var name = Contact.AddLabel ("ContactName", "Natasha Eadie", UIColor.Black, DataTextSize);
			var phone = Contact.AddLabel ("ContactPhone", "0412 123 456", UIColor.Black, DataTextSize);
			Contact.AddConstraint ("V:|[ContactLabel]-3-[ContactName]-3-[ContactPhone]-18-|");
			Contact.AddConstraint ("H:|-19-[ContactLabel]-(>=8)-|");
			Contact.AddConstraint ("H:|-19-[ContactName]-(>=8)-|");
			Contact.AddConstraint ("H:|-19-[ContactPhone]-(>=8)-|");
		}



	}
}

