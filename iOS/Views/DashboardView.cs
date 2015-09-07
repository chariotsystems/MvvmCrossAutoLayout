using System;
using System.Drawing;
using System.Collections.Generic;
using UIKit;
 
namespace AutoLayout
{

	public class DashboardView: UIViewController {

		public override void LoadView ()
		{
			base.LoadView ();
			View = BuildView();
		}

		private UIView BuildView(){

			ContentView Root = AutoLayout.ContentView.CreateRoot("Root", UIColor.Blue);
			var Content = Root;
			Content.AddVerticalGradient(UIColor.Blue, UIColor.White);
//			Content.AddImageCenteredX("TelstraLogo", "ic_logo.png");
//			Content.AddLabelCenteredX("ToolKit", "TOOLKIT", UIColor.White, 38);
//			Content.AddLabelCenteredX("Spinner", "Spinner", UIColor.White, 38);
//			//MainView.AddSpinner("Spinner", UIColor.White, null, 0);
//
//			Content.AddConstraint("V:|-82-[TelstraLogo(116)]-83-[ToolKit]-[Spinner]-(>=4)-|");
//			Content.AddConstraint("H:[TelstraLogo(100)]");

			return Root;
		}
	}
}

