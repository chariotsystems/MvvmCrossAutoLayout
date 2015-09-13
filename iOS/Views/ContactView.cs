using System;
using UIKit;
using System.Drawing;
using System.Collections.Generic;
using Foundation;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using MvvmCrossAutoLayout.ViewModels;
using ObjCRuntime;
using MvvmCrossAutoLayout.iOS;

namespace AutoLayout
{

	[Register ("ContactView")]
	public class ContactView : MvxViewController
	{
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			View = BuildView ();
			//LocalNotificationHelper.SendLocalNotification ();
		}


		private UIView BuildView ()
		{
			var Set = this.CreateBindingSet<ContactView, ContactViewModel> ();
			var Root = ContentView.CreateRoot ("Root", UIColor.DarkGray);
			NavigationController.NavigationBarHidden = true;

			// Scroll View uses all of main view width
//			var scrollView = mainView.AddScrollView("scrollView", TelstraGreyL6, constraints);
//			mainView.AddConstraint("H:|[scrollView]|");
//			var scrollView = mainView;
			//TODO: wrap uiscrollview like contentview so that it can have its own AddConstraints method

			var ProfileBorder = Root.AddContainer ("ProfileBorder", UIColor.White);
			var NotifyBorder = Root.AddContainer ("NotifyBorder", UIColor.White);
			var HobbiesBorder = Root.AddContainer ("HobbiesBorder", UIColor.White);
			var FriendsBorder = Root.AddContainer ("FriendsBorder", UIColor.White);
			ProfileBorder.AddParentConstraint ("V:|-16-[ProfileBorder(80)]-4-[NotifyBorder]-[HobbiesBorder]-[FriendsBorder]-(>=4)-|");
			ProfileBorder.AddParentConstraint ("H:|-4-[ProfileBorder]-4-|");
			NotifyBorder.AddParentConstraint ("H:|-4-[NotifyBorder]-4-|");
			HobbiesBorder.AddParentConstraint ("H:|-4-[HobbiesBorder]-4-|");
			FriendsBorder.AddParentConstraint ("H:|-4-[FriendsBorder]-4-|");

			SetProfile (Set, ProfileBorder);


			Set.Apply ();

			return Root;
		}

		//TODO: edit button
		void SetProfile (MvxFluentBindingDescriptionSet<ContactView, ContactViewModel> Set, ContentView ProfileBorder)
		{
			var Details = ProfileBorder.AddContainer ("Details", UIColor.White);
			var Photo = ProfileBorder.AddContainer ("Photo", UIColor.White);
			Details.AddParentConstraint ("V:|-[Details]-|");
			Photo.AddParentConstraint ("V:|-[Photo]-(>=8)-|");
			Details.AddParentConstraint ("H:|-[Details]-(>=8)-[Photo]-|");
			Photo.AddImageCenteredX ("Picture", "Alex.jpg");
			Photo.AddConstraint ("H:|[Picture(48)]|");
			Photo.AddConstraint ("V:|[Picture(64)]|");
			Details.AddLabelLeft ("Name", "Alex Eadie", UIColor.Black, 12);
			Details.AddLabelLeft ("Phone", "0456 234 154", UIColor.Blue, 12);
			Details.AddLabelLeft ("Email", "alex.eadie@themail.com", UIColor.Blue, 12);
			Details.AddConstraint ("V:|[Name]-[Phone]-[Email]-(>=8)-|");
		}

		//		void SetCustomer (MvxFluentBindingDescriptionSet<TicketView, TicketViewModel> Set, ContentView Customer)
		//		{
		//			Customer.AddLabel ("CustomerLabel", "CUSTOMER", UIColor.LightGray/*TelstraGreyL6*/, 12);
		//			var name = Customer.AddLabel ("CustomerName", "Peter Parker", UIColor.Black, 12);
		//			var telstra = Customer.AddImage ("Customer_telstra_img", "ic_telstra_logo.png");
		//			Customer.AddConstraint ("V:|-18-[CustomerLabel]-7-[CustomerName]|");
		//			Customer.AddConstraint ("V:|-20-[Customer_telstra_img(28)]-(>=8)-|");
		//			Customer.AddConstraint ("H:|-19-[CustomerLabel]-(>=8)-|");
		//			Customer.AddConstraint ("H:|-19-[CustomerName]-(>=8)-|");
		//			Customer.AddConstraint ("H:|-(>=8)-[Customer_telstra_img(28)]-27-|");
		//			Set.Bind (name).To (vm => vm.Hello);
		//			Set.Bind (telstra).For ("Visibility").To (vm => vm.Telstra).WithConversion ("Visibility");
		//
		//		}



		//		static void SetPremisesTabs (MvxFluentBindingDescriptionSet<TicketView, TicketViewModel> Set, ContentView PremisesBorder)
		//		{
		//			var PremisesTabBox = PremisesBorder.AddContainer ("PremisesTabBox", UIColor.White);
		//			var ConnectionPillarTabBox = PremisesBorder.AddContainer ("ConnectionPillarTabBox", UIColor.LightGray);
		//			var ExchangeTabBox = PremisesBorder.AddContainer ("ExchangeTabBox", UIColor.LightGray);
		//			var PremisesLabel = PremisesTabBox.AddLabelCenteredXY ("PremisesTab", "PREMISES", UIColor.Black, 15);
		//			ConnectionPillarTabBox.AddLabelCenteredXY ("ConnectionPillarTab", "P74", UIColor.Blue, 15);
		//			ExchangeTabBox.AddLabelCenteredXY ("ExchangeTab", "PARR", UIColor.Blue, 15);
		//
		//			Set.Bind (PremisesTabBox).For ("Tap").To (vm => vm.TestCommand).WithConversion ("CommandParameter", "alex");
		//			// https://github.com/MvvmCross/MvvmCross/wiki/Value-Converters
		//			Set.Bind (PremisesTabBox).For (field => field.BackgroundColor).To (vm => vm.PremisesTabBackgroundColor).WithConversion ("RGBA");
		//			Set.Bind (PremisesLabel).For (field => field.TextColor).To (vm => vm.PremisesTabTextColor).WithConversion ("RGBA");
		//			SetPremises (Set, PremisesBorder);
		//		}
		//
		//		static void SetPremises (MvxFluentBindingDescriptionSet<TicketView, TicketViewModel> Set, ContentView PremisesBorder)
		//		{
		//			var address = PremisesBorder.AddLabel ("PremisesAddress", "UNIT 103, 402 DRAYTON-WELLCAMP RD PARRAMATTA", UIColor.Black, 15);
		//			Set.Bind (address).To (vm => vm.Hello);
		//			SetPremisesDirections (Set, PremisesBorder);
		//			PremisesBorder.AddConstraint ("V:|[PremisesTabBox(55)]-20-[PremisesAddress(60)]-18-|");
		//			PremisesBorder.AddConstraint ("V:|[ExchangeTabBox(55)]-20-[PremisesAddress(60)]-18-|");
		//			PremisesBorder.AddConstraint ("V:|[ConnectionPillarTabBox(55)]-20-[PremisesAddress(60)]-18-|");
		//			PremisesBorder.AddConstraint ("V:|[PremisesTabBox(55)]-28-[PremisesDirectionsBox]-(>=8)-|");
		//			PremisesBorder.AddConstraint ("H:|[PremisesTabBox]-2-[ConnectionPillarTabBox(==PremisesTabBox)]-2-[ExchangeTabBox(==PremisesTabBox)]|");
		//			PremisesBorder.AddConstraint ("H:|-19-[PremisesAddress(<=200)]-(>=6)-[PremisesDirectionsBox(66)]-10-|");
		//		}
		//
		//		static void SetPremisesDirections (MvxFluentBindingDescriptionSet<TicketView, TicketViewModel> Set, ContentView PremisesBorder)
		//		{
		//			var PremisesDirectionsBox = PremisesBorder.AddContainer ("PremisesDirectionsBox", UIColor.White);
		//			PremisesDirectionsBox.AddLabelCenteredX ("PremisesDirections", "Directions", UIColor.Blue, 12);
		//			PremisesDirectionsBox.AddImageCenteredX ("PremisesDirectionsImage", "ic_directions.png");
		//			PremisesDirectionsBox.AddConstraint ("V:|[PremisesDirectionsImage(36)]-3-[PremisesDirections]|");
		//			PremisesDirectionsBox.AddConstraint ("H:|-(>=1)-[PremisesDirectionsImage(36)]-(>=1)-|");
		//			PremisesDirectionsBox.AddConstraint ("H:|-(>=1)-[PremisesDirections]-(>=1)-|");
		//		}

	}
}

