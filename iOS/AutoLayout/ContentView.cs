using System;
using UIKit;
using System.Drawing;
using Foundation;
using CoreAnimation;
using CoreGraphics;
using MaterialControls;
using Cirrious.MvvmCross.Binding.BindingContext;

namespace AutoLayout
{
	// https://forums.xamarin.com/discussion/6570/auto-layout-and-view-constraints
	public class ContentView : UIView {

		public string Name { get; set; }
		public Constraints OurConstraints { get; set; }
		public ContentView Parent;
		//public MvxFluentBindingDescriptionSet IsDirectBinding;

		public ContentView(UIColor color){
			base.BackgroundColor = color;
		}

		private ContentView(string name, ContentView parent, string color, float x=10, float y=10, float width=10, float height=10){
			Init(name, parent, GetColorFromHexString(color), x, y, width, height);	
		}

		public static ContentView CreateRoot(string name, UIColor color){
			ContentView  root = new ContentView(name, null, color);
			root.TranslatesAutoresizingMaskIntoConstraints = true;
			return root;
		}

//		public static ContentView CreateRoot<TTarget, TSource>(this TTarget target, string name, UIColor color) where TTarget : class, IMvxBindingContextOwner {
//			ContentView  root = new ContentView(name, null, color);
//			MvxFluentBindingDescriptionSet<TTarget, TSource> (target);
//			return root;
//		}

		private ContentView(string name, ContentView parent, UIColor color, float x=10, float y=10, float width=10, float height=10){
			Init(name, parent, color, x,  y, width, height);	
		}

		private void Init(string name, ContentView parent, UIColor color, float x=10, float y=10, float width=10, float height=10){
			OurConstraints = new Constraints ();
			Parent = parent;
			base.BackgroundColor = color;
			Name = name;
			//UIColor color = GetColorFromHexString("#004B46");
			base.TranslatesAutoresizingMaskIntoConstraints = false;
			if (parent == null) {
				base.Frame = new RectangleF (0, 0, (float) UIScreen.MainScreen.Bounds.Width, (float) UIScreen.MainScreen.Bounds.Height);
			} else {
				base.Frame = new RectangleF (x, y, width, height);
			}
			if (parent != null) {
				parent.Add (this);
				Parent.OurConstraints.ViewNames.Add (new NSString(Name));
				Parent.OurConstraints.Views.Add (this);
			}
			// Add to ourself as well.
			OurConstraints.ViewNames.Add (new NSString(Name));
			OurConstraints.Views.Add (this);
		}

		public ContentView AddContainerCenteredX(string name, UIColor color)
		{
			ContentView view = AddContainer (name, color);
			AddConstraintToCenterX (view);
			return view;
		}
		public ContentView AddContainer(string name, UIColor color)
		{
			ContentView content = new ContentView (name, this, color);
			return content;
		}

		//TODO: fixme - not sure why this does not work - use debugger.
		public static UIColor GetColorFromHexString(string hexString, uint alpha = 0xFF){
			// bypass '#' character
			uint rgbValue = Convert.ToUInt32(hexString.Substring(1), 16);
			return UIColor.FromRGBA((rgbValue & 0xFF0000) >> 16, (rgbValue & 0xFF00) >> 8, (rgbValue & 0xFF), alpha);
		}

		public void AddConstraint(string rule){
			this.AddConstraints(NSLayoutConstraint.FromVisualFormat(rule, 0, new NSDictionary(), OurConstraints.Dictionary()));
		}
//		public void AddConstraintAlignAllCenterX(string rule){
//			this.AddConstraints(NSLayoutConstraint.FromVisualFormat(rule, NSLayoutFormatOptions.AlignAllCenterX, new NSDictionary(), Constraints.Dictionary()));
//		}
//
//		public void AddConstraintAlignAllCenterY(string rule){
//			this.AddConstraints(NSLayoutConstraint.FromVisualFormat(rule, NSLayoutFormatOptions.AlignAllCenterY, new NSDictionary(), Constraints.Dictionary()));
//		}
			
		public void AddConstraintToCenterXY(UIView view){
			this.AddConstraints (new NSLayoutConstraint[] {NSLayoutConstraint.Create(view, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, this, NSLayoutAttribute.CenterX, 1, 0)});
			this.AddConstraints (new NSLayoutConstraint[] {NSLayoutConstraint.Create(view, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, this, NSLayoutAttribute.CenterY, 1, 0)});
		}

		public void AddConstraintToCenterX(UIView view){
			AddConstraintToCenterX(this, view);
		}

		public void AddConstraintToCenterX(UIView superView, UIView view){
			this.AddConstraints (new NSLayoutConstraint[] {NSLayoutConstraint.Create(view, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, superView, NSLayoutAttribute.CenterX, 1, 0)});
		}

		public void AddParentConstraint(string rule){
			Parent.AddConstraints(NSLayoutConstraint.FromVisualFormat(rule, 0, new NSDictionary(), Parent.OurConstraints.Dictionary()));
		}

		public UIImageView AddImageCenteredX(string name, string imageFileName){
			var imageView = AddImage(name, imageFileName);
			AddConstraintToCenterX (this, imageView);
			return imageView;
		}
		public UIImageView AddImage(string name, string imageFileName){
			var imageView = new UIImageView (UIImage.FromBundle(imageFileName));
			imageView.Frame = new RectangleF (10,10,imageView.Image.CGImage.Width, imageView.Image.CGImage.Height);
			imageView.TranslatesAutoresizingMaskIntoConstraints = false;
			OurConstraints.ViewNames.Add (new NSString(name));
			OurConstraints.Views.Add (imageView);
			this.Add (imageView);
			return imageView;
		}

		public UILabel AddLabelCenteredX(UIView superView, string name, string text, UIColor color, float size){
			UILabel label = AddLabel(name, text, color, size);
			AddConstraintToCenterX (superView, label);
			return label;
		}

		public UILabel AddLabelCenteredX(string name, string text, UIColor color, float size){
			UILabel label = AddLabel(name, text, color, size);
			AddConstraintToCenterX (this, label);
			return label;
		}

		public UILabel AddLabelCenteredXY(string name, string text, UIColor color, float size){
			UILabel label = AddLabel(name, text, color, size);
			AddConstraintToCenterXY (label);
			return label;
		}

		public UILabel AddLabel(string name, string text, UIColor color, float size){
			var label = new UILabel ();
			label.Text = text;
			label.TextColor = color;//UIColor.LightGray;//GetColorFromHexString (color);
			label.Font = UIFont.FromName("Helvetica-Bold", size);
			label.Frame = new RectangleF (10,10, 10, 10);
			label.TranslatesAutoresizingMaskIntoConstraints = false;
			label.LineBreakMode = UILineBreakMode.WordWrap;
			label.Lines = 0;  
			OurConstraints.ViewNames.Add (new NSString(name));
			OurConstraints.Views.Add (label);
			this.Add (label);
			return label;
		}

		public void AddVerticalGradient(UIColor startColor, UIColor endColor){
			CAGradientLayer gradient = new CAGradientLayer ();
			gradient.Frame = this.Frame;
			gradient.Colors = new CGColor[] { startColor.CGColor, endColor.CGColor };
			this.Layer.AddSublayer(gradient);
		}

		public MDProgress AddSpinnerCenteredXY(string name, UIColor progressColor, UIColor trackColor, int trackWidth){
			MDProgress spinner = AddSpinner(name, progressColor, trackColor, trackWidth);
			AddConstraintToCenterXY (spinner);
			return spinner;
		}
		// https://github.com/fpt-software/Material-Controls-For-iOS/tree/master/iOSUILibDemo
		// NuGet the same
		// But we have an issue with NuGet here :(
		// http://forums.xamarin.com/discussion/1475/changes-to-assembly-strongnames-in-xamarin-ios-6-2-0#latest
		public MDProgress AddSpinner(string name, UIColor progressColor, UIColor trackColor, int trackWidth){
			MDProgress mdProgress = new MDProgress ();
			mdProgress.ProgressColor = progressColor;
			if (trackColor != null) {
				mdProgress.TrackColor = trackColor;
				mdProgress.EnableTrackColor = true;
			}
			if (trackWidth > 0) {
				//mdProgress.TrackWidth = trackWidth;// Why can't I set this??
			}
			mdProgress.Type = (int) MDProgressType.Indeterminate;
			mdProgress.Style = (int) MDProgressStyle.Circular;
			mdProgress.TranslatesAutoresizingMaskIntoConstraints = false;
			OurConstraints.ViewNames.Add (new NSString(name));
			OurConstraints.Views.Add (mdProgress);
			this.Add (mdProgress);
			return mdProgress;
		}

		public UITextView AddTextCenteredX(string name, string text, UIColor textColor, UIColor backGroundColor, float size){
			UITextView view = AddText(name, text, textColor, backGroundColor, size);
			AddConstraintToCenterX (this, view);
			return view;
		}

		public UITextView AddText(string name, string text, UIColor textColor, UIColor backgroundColor, float size){
			var textView = new UITextView ();
			textView.Text = text;
			textView.TextColor = textColor;//GetColorFromHexString (color);
			textView.BackgroundColor = backgroundColor;
			textView.Font = UIFont.FromName("Helvetica-Bold", size);
			textView.Frame = new RectangleF (10,10, 10, 10);
			textView.TranslatesAutoresizingMaskIntoConstraints = false;
			OurConstraints.ViewNames.Add (new NSString(name));
			OurConstraints.Views.Add (textView);
			this.Add (textView);
			return textView;
		}

		public UIScrollView AddScrollView(string name, string color){
			var scrollView = new UIScrollView ();
			scrollView.BackgroundColor = GetColorFromHexString(color);
			scrollView.Frame = new RectangleF (10, 10, 10, 10);
			scrollView.TranslatesAutoresizingMaskIntoConstraints = false;
			OurConstraints.ViewNames.Add (new NSString(name));
			OurConstraints.Views.Add (scrollView);
			this.Add (scrollView);
			return scrollView;
		}

	}

}

