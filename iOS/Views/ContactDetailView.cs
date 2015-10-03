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

namespace AutoLayout
{

	[Register ("ContactDetailView")]
	public class ContactDetailView : MvxViewController
	{
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			View = BuildView ();
			//LocalNotificationHelper.SendLocalNotification ();
		}


		private UIView BuildView ()
		{
			var Set = this.CreateBindingSet<ContactDetailView, ContactDetailViewModel> ();
			var Root = AutoLayoutContentView.CreateRoot ("Root", UIColor.DarkGray, "Helvetica-Bold");
			NavigationController.NavigationBarHidden = true;

			// Scroll View uses all of main view width
//			var scrollView = mainView.AddScrollView("scrollView", TelstraGreyL6, constraints);
//			mainView.AddConstraint("H:|[scrollView]|");
//			var scrollView = mainView;
			//TODO: wrap uiscrollview like contentview so that it can have its own AddConstraints method

			var ProfileBorder = Root.AddContainer ("ProfileBorder", UIColor.White);
			var ComponentsBorder = Root.AddContainer ("ComponentsBorder", UIColor.White);
			var NotifyBorder = Root.AddContainer ("NotifyBorder", UIColor.White);
			var HobbiesBorder = Root.AddContainer ("HobbiesBorder", UIColor.White);
			var FriendsBorder = Root.AddContainer ("FriendsBorder", UIColor.White);
			Root.AddConstraint ("V:|-16-[ProfileBorder(80)]-4-[ComponentsBorder]-[NotifyBorder]-[HobbiesBorder]-[FriendsBorder]-(>=4)-|");
			Root.AddConstraint ("H:|-4-[ProfileBorder]-4-|");
			Root.AddConstraint ("H:|-4-[ComponentsBorder]-4-|");
			Root.AddConstraint ("H:|-4-[NotifyBorder]-4-|");
			Root.AddConstraint ("H:|-4-[HobbiesBorder]-4-|");
			Root.AddConstraint ("H:|-4-[FriendsBorder]-4-|");

			SetProfile (Set, ProfileBorder);
			SetComponents (Set, ComponentsBorder);


			Set.Apply ();

			return Root;
		}

		//TODO: edit button
		void SetProfile (MvxFluentBindingDescriptionSet<ContactDetailView, ContactDetailViewModel> Set, AutoLayoutContentView ProfileBorder)
		{
			var Details = ProfileBorder.AddContainer ("Details", UIColor.White);
			var Photo = ProfileBorder.AddContainer ("Photo", UIColor.White);
			ProfileBorder.AddConstraint ("V:|-[Details]-|");
			ProfileBorder.AddConstraint ("V:|-[Photo]-(>=8)-|");
			ProfileBorder.AddConstraint ("H:|-[Details]-(>=8)-[Photo]-|");
			Photo.AddImageCenteredX ("Picture", "Alex.jpg");
			Photo.AddConstraint ("H:|[Picture(48)]|");
			Photo.AddConstraint ("V:|[Picture(64)]|");
			Details.AddLabelLeft ("Name", "Alex Eadie", UIColor.Black, 12);
			Details.AddLabelLeft ("Phone", "0456 234 154", UIColor.Blue, 12);
			Details.AddLabelLeft ("Email", "alex.eadie@themail.com", UIColor.Blue, 12);
			Details.AddConstraint ("V:|[Name]-[Phone]-[Email]-(>=8)-|");
		}

		void SetComponents (MvxFluentBindingDescriptionSet<ContactDetailView, ContactDetailViewModel> Set, AutoLayoutContentView ComponentsBorder)
		{
			ComponentsBorder.AddButton ("Button1", "Button1", UIColor.Green, UIColor.White, 12);
			ComponentsBorder.AddActivityIndicator ("ActivityIndicator", UIColor.Blue);
			ComponentsBorder.AddPageControl ("PageControl", UIColor.Blue);
			ComponentsBorder.AddProgressView ("ProgressView", UIColor.Blue);
			ComponentsBorder.AddSlider ("Slider", UIColor.Blue);
			ComponentsBorder.AddSwitch ("Switch", UIColor.Blue);
			ComponentsBorder.AddSegmentedControl ("SegmentedControl", UIColor.Blue);
			// We can add in other UIViews that don't have their own bespoke Add methods.
			var button2 = (UIButton)ComponentsBorder.AddView ("Button2", UIButton.FromType (UIButtonType.RoundedRect));
			button2.SetTitle ("Button2", UIControlState.Normal);
			ComponentsBorder.AddConstraint ("V:|-[Button1(20)]-[ActivityIndicator(60)]-[PageControl(50)]-[ProgressView(50)]-[Slider]-[Switch]-[SegmentedControl]-|");
			ComponentsBorder.AddConstraint ("V:|-[Button2(20)]-(>=8)-|");
			ComponentsBorder.AddConstraint ("H:|-[Button1(<=100)]-[Button2(<=100)]-(>=8)-|");
			ComponentsBorder.AddConstraint ("H:|-[ActivityIndicator(60)]-(>=8)-|");
			ComponentsBorder.AddConstraint ("H:|-[PageControl(50)]-(>=8)-|");
			ComponentsBorder.AddConstraint ("H:|-[ProgressView(50)]-(>=8)-|");
			ComponentsBorder.AddConstraint ("H:|-[Slider(100)]-(>=8)-|");
			ComponentsBorder.AddConstraint ("H:|-[Switch]-(>=8)-|");
			ComponentsBorder.AddConstraint ("H:|-[SegmentedControl]-(>=8)-|");
		}

		//TODO: input fields and text conversions
		//Content.AddTextCenteredX("Fnn", "Hello", UIColor.Black, UIColor.Yellow, 16);
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

