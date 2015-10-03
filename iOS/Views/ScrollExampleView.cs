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
			var Root = AutoLayoutContentView.CreateRoot ("Root", UIColor.DarkGray, "Helvetica-Bold");
			var ScrollView = Root.AddScrollView ("ScrollView", UIColor.DarkGray);

			var ContactBorder = ScrollView.AddContainer ("ContactBorder", UIColor.White);
			var PremisesBorder = ScrollView.AddContainer ("PremisesBorder", UIColor.White);
			ScrollView.AddConstraint ("V:|-16-[ContactBorder]-300-[PremisesBorder]-(>=4)-|");
			ScrollView.AddConstraint ("H:|-4-[ContactBorder]-4-|");
			ScrollView.AddConstraint ("H:|-4-[PremisesBorder]-4-|");

			SetCustomerAndContact (ContactBorder);

			return Root;
		}

		void SetCustomerAndContact (AutoLayoutContentView ContactBorder)
		{
			var Customer = ContactBorder.AddContainer ("Customer", UIColor.White);
			var SiteContact = ContactBorder.AddContainer ("SiteContact", UIColor.White);
			ContactBorder.AddConstraint ("V:|-[Customer]-25-[SiteContact]|");
			ContactBorder.AddConstraint ("H:|-[Customer]-(>=8)-|");
			ContactBorder.AddConstraint ("H:|-[SiteContact]-(>=8)-|");
			SetCustomer (Customer);
			SetContact (SiteContact);
		}

		void SetCustomer (AutoLayoutContentView Customer)
		{
			Customer.AddLabel ("CustomerLabel", "CUSTOMER", UIColor.LightGray/*TelstraGreyL6*/, LabelTextSize);
			var name = Customer.AddLabel ("CustomerName", "Alex Eadie", UIColor.Black, DataTextSize);
			Customer.AddConstraint ("V:|-18-[CustomerLabel]-7-[CustomerName]|");
			Customer.AddConstraint ("H:|-19-[CustomerLabel]-(>=8)-|");
			Customer.AddConstraint ("H:|-19-[CustomerName]-(>=8)-|");
		}

		void SetContact (AutoLayoutContentView SiteContact)
		{
			SiteContact.AddLabel ("SiteContactLabel", "SITE CONTACT", UIColor.LightGray/*TelstraGreyL6*/, LabelTextSize);
			var name = SiteContact.AddLabel ("SiteContactName", "Natasha Eadie", UIColor.Black, DataTextSize);
			var phone = SiteContact.AddLabel ("SiteContactPhone", "0412 123 456", UIColor.Black, DataTextSize);
			SiteContact.AddConstraint ("V:|[SiteContactLabel]-3-[SiteContactName]-3-[SiteContactPhone]-18-|");
			SiteContact.AddConstraint ("V:|-20-[SitePhoneImage(23)]-(>=8)-|");
			SiteContact.AddConstraint ("H:|-19-[SiteContactLabel]-(>=8)-|");
			SiteContact.AddConstraint ("H:|-19-[SiteContactPhone]-(>=8)-|");
		}



	}
}

