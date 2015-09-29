using System;
using UIKit;
using System.Drawing;
using System.Collections.Generic;

namespace AutoLayout
{

	public class SplashView: UIViewController
	{

		public override void LoadView ()
		{
			base.LoadView ();
			View = BuildView ();
		}

		private UIView BuildView ()
		{

			AutoLayoutContentView Root = AutoLayoutContentView.CreateRoot ("Root", UIColor.Blue, "Helvetica-Bold");
			Root.AddVerticalGradient (UIColor.Blue, UIColor.White);
			Root.AddImageCenteredX ("TelstraLogo", "ic_logo.png");
			Root.AddLabelCenteredX ("ToolKit", "TOOLKIT", UIColor.White, 38);
			var SpinnerContainer = Root.AddContainerCenteredX ("SpinnerContainer", UIColor.Clear);
			SpinnerContainer.AddSpinnerCenteredXY ("Spinner", UIColor.White, null, 0);

			Root.AddConstraint ("V:|-82-[TelstraLogo(116)]-83-[ToolKit]-20-[SpinnerContainer(40)]-(>=4)-|");
			Root.AddConstraint ("H:[TelstraLogo(100)]");
			Root.AddConstraint ("H:[SpinnerContainer(40)]");

			return Root;
		}
	}
}

