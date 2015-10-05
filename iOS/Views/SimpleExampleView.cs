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

	[Register ("SimpleExampleView")]
	public class SimpleExampleView : MvxViewController
	{
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			View = BuildView ();
		}


		private UIView BuildView ()
		{
			var Set = this.CreateBindingSet<SimpleExampleView, SimpleExampleViewModel> ();
			var Root = AutoLayoutContentView.CreateRoot ("Root", UIColor.DarkGray, "Helvetica-Bold");
			NavigationController.NavigationBarHidden = true;


			var ProfileBorder = Root.AddContainer ("ProfileBorder", UIColor.White);
			var ComponentsBorder = Root.AddContainer ("ComponentsBorder", UIColor.White);
			Root.AddConstraint ("V:|-16-[ProfileBorder(80)]-4-[ComponentsBorder]-(>=4)-|");
			Root.AddConstraint ("H:|-4-[ProfileBorder]-4-|");
			Root.AddConstraint ("H:|-4-[ComponentsBorder]-4-|");

			SetProfile (Set, ProfileBorder);
			SetComponents (Set, ComponentsBorder);


			Set.Apply ();

			return Root;
		}

		void SetProfile (MvxFluentBindingDescriptionSet<SimpleExampleView, SimpleExampleViewModel> Set, AutoLayoutContentView ProfileBorder)
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

		void SetComponents (MvxFluentBindingDescriptionSet<SimpleExampleView, SimpleExampleViewModel> Set, AutoLayoutContentView ComponentsBorder)
		{
			var button1 = ComponentsBorder.AddButton ("Button1", "Scroll Example", UIColor.Green, UIColor.White, 12);
			ComponentsBorder.AddActivityIndicator ("ActivityIndicator", UIColor.Blue);
			ComponentsBorder.AddPageControl ("PageControl", UIColor.Blue);
			ComponentsBorder.AddProgressView ("ProgressView", UIColor.Blue);
			ComponentsBorder.AddSlider ("Slider", UIColor.Blue);
			ComponentsBorder.AddSwitch ("Switch", UIColor.Blue);
			ComponentsBorder.AddSegmentedControl ("SegmentedControl", UIColor.Blue);
			// We can add in other UIViews that don't have their own bespoke Add methods.
			var button2 = (UIButton)ComponentsBorder.AddView ("Button2", UIButton.FromType (UIButtonType.RoundedRect));
			button2.SetTitle ("List Example", UIControlState.Normal);
			ComponentsBorder.AddConstraint ("V:|-[Button1(20)]-[ActivityIndicator(60)]-[PageControl(50)]-[ProgressView(50)]-[Slider]-[Switch]-[SegmentedControl]-|");
			ComponentsBorder.AddConstraint ("V:|-[Button2(20)]-(>=8)-|");
			ComponentsBorder.AddConstraint ("H:|-[Button1(<=100)]-[Button2(<=100)]-(>=8)-|");
			ComponentsBorder.AddConstraint ("H:|-[ActivityIndicator(60)]-(>=8)-|");
			ComponentsBorder.AddConstraint ("H:|-[PageControl(50)]-(>=8)-|");
			ComponentsBorder.AddConstraint ("H:|-[ProgressView(50)]-(>=8)-|");
			ComponentsBorder.AddConstraint ("H:|-[Slider(100)]-(>=8)-|");
			ComponentsBorder.AddConstraint ("H:|-[Switch]-(>=8)-|");
			ComponentsBorder.AddConstraint ("H:|-[SegmentedControl]-(>=8)-|");


			Set.Bind (button1).For ("Tap").To (vm => vm.GotoScrollView).WithConversion ("CommandParameter", "scrollView");
			Set.Bind (button2).For ("Tap").To (vm => vm.GotoListView).WithConversion ("CommandParameter", "listView");

		}


	}
}

