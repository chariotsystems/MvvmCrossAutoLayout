using System;
using UIKit;
using System.Drawing;
using AutoLayout;
using Foundation;

namespace MvvmCrossAutoLayout.iOS
{
	// reuse kittens code here with acknowledgement
	public class AutoLayoutListView //: UIListView
	{
		//		public string Name { get; set; }
		//
		//		public Constraints OurConstraints { get; set; }
		//
		//		public ContentView Parent;
		//
		//		public UIListView (ContentView parent, string name, UIColor color)
		//		{
		//			Name = name;
		//			OurConstraints = new Constraints ();
		//			base.BackgroundColor = color;
		//			Parent = parent;
		//			OurConstraints.ViewNames.Add (new NSString (Name));
		//			OurConstraints.Views.Add (this);
		//			Frame = new RectangleF (10, 10, 10, 10);
		//			TranslatesAutoresizingMaskIntoConstraints = false;
		//		}
		//
		//		public ContentView AddContainer (string name, UIColor color)
		//		{
		//			ContentView content = ContentView.CreateScrollViewContent (this, name, color);
		//			OurConstraints.ViewNames.Add (new NSString (name));
		//			OurConstraints.Views.Add (content);
		//			this.Add (content);
		//			return content;
		//		}
		//
		//		public void AddConstraint (string rule)
		//		{
		//			this.AddConstraints (NSLayoutConstraint.FromVisualFormat (rule, 0, new NSDictionary (), OurConstraints.Dictionary ()));
		//		}
		//
		//		public void AddParentConstraint (string rule)
		//		{
		//			Parent.AddConstraints (NSLayoutConstraint.FromVisualFormat (rule, 0, new NSDictionary (), Parent.OurConstraints.Dictionary ()));
		//		}

	}
}

