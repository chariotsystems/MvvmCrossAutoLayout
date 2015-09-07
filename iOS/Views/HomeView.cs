using System;
using UIKit;
using System.Drawing;
using System.Collections.Generic;
 
namespace AutoLayout
{

	public class HomeView: UIViewController {

		public override void LoadView ()
		{
			base.LoadView ();
			View = BuildView();
		}

		private UIView BuildView(){

			ContentView Root = AutoLayout.ContentView.CreateRoot("Root", UIColor.White);
			var Content = Root;
			Content.AddLabelCenteredX("CustomerLabel", "CUSTOMER", UIColor.LightGray, 16);
			Content.AddTextCenteredX("Fnn", "Hello", UIColor.Black, UIColor.Yellow, 16);
			Content.AddConstraint("V:|-82-[CustomerLabel]-[Fnn(20)]-(>=4)-|");
			Content.AddConstraint("H:|-(>=4)-[Fnn(200)]-(>=4)-|");

			return Root;
		}
	}
}

