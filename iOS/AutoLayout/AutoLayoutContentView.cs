using System;
using UIKit;
using System.Drawing;
using Foundation;
using CoreAnimation;
using CoreGraphics;
using MaterialControls;
using Cirrious.MvvmCross.Binding.BindingContext;
using MvvmCrossAutoLayout.iOS;
using AutoLayout.iOS.Helpers;

namespace AutoLayout
{
	public class AutoLayoutContentView : UIView
	{
		public string Font { get; set; }

		public string Name { get; set; }

		public AutoLayoutConstraints OurConstraints { get; set; }

		public AutoLayoutContentView Parent;

		private AutoLayoutContentView (string name, UIColor color, string font)
		{
			OurConstraints = new AutoLayoutConstraints ();
			base.BackgroundColor = color;
			Font = font;
			Name = name;

		}


		public static AutoLayoutContentView CreateRoot (string name, UIColor color, string font)
		{
			AutoLayoutContentView root = new AutoLayoutContentView (name, null, color, font);
			root.TranslatesAutoresizingMaskIntoConstraints = true;
			return root;
		}

		public static AutoLayoutContentView CreateListContentRoot (string name, UIColor color, UIView view, string font)
		{
			AutoLayoutContentView root = new AutoLayoutContentView (name, null, color, font);
			root.TranslatesAutoresizingMaskIntoConstraints = true;
			int width = (int)view.Bounds.Size.Width;
			int height = (int)view.Bounds.Size.Height;
			root.Frame = new RectangleF (0, 0, width, height);
			return root;
		}



		private AutoLayoutContentView (string name, AutoLayoutContentView parent, UIColor color, String font, float x = 10, float y = 10, float width = 10, float height = 10)
		{
			Font = font;
			OurConstraints = new AutoLayoutConstraints ();
			Parent = parent;
			base.BackgroundColor = color;
			Name = name;
			base.TranslatesAutoresizingMaskIntoConstraints = false;
			if (parent == null) {
				base.Frame = new RectangleF (0, 0, (float)UIScreen.MainScreen.Bounds.Width, (float)UIScreen.MainScreen.Bounds.Height);
			} else {
				base.Frame = new RectangleF (x, y, width, height);// Is this still required?
			}
			if (parent != null) {
				parent.Add (this);
				Parent.OurConstraints.ViewNames.Add (new NSString (Name));
				Parent.OurConstraints.Views.Add (this);
			}
			// Add to ourself as well.
			OurConstraints.ViewNames.Add (new NSString (Name));
			OurConstraints.Views.Add (this);
		}

		private UILabel DebugLabel (string name)
		{
			var label = new UILabel ();
			label.Text = name;
			label.TextColor = UIColor.Black;
			label.Font = UIFont.FromName (Font, 12);
			label.Frame = new RectangleF (10, 10, 200, 100);
			return label;
		}


		public AutoLayoutContentView AddContainerCenteredX (string name, UIColor color)
		{
			AutoLayoutContentView view = AddContainer (name, color);
			AddConstraintToCenterX (view);
			return view;
		}

		public AutoLayoutContentView AddContainer (string name, UIColor color)
		{
			AutoLayoutContentView content = new AutoLayoutContentView (name, this, color, this.Font);
			return content;
		}


		public void AddConstraint (string rule)
		{
			this.AddConstraints (NSLayoutConstraint.FromVisualFormat (rule, 0, new NSDictionary (), OurConstraints.Dictionary ()));
		}

		public void AddConstraint (string rule, UIView view, AutoLayoutConstraints constraints)
		{
			view.AddConstraints (NSLayoutConstraint.FromVisualFormat (rule, 0, new NSDictionary (), constraints.Dictionary ()));
		}

		public void AddConstraintMatchParentWidth (UIView superView, UIView view)
		{
			this.AddConstraints (new NSLayoutConstraint[] { NSLayoutConstraint.Create (view, NSLayoutAttribute.Width, NSLayoutRelation.Equal, superView, NSLayoutAttribute.Width, 1, 0) });
		}

		public void AddConstraintMatchParentHeight (UIView superView, UIView view)
		{
			this.AddConstraints (new NSLayoutConstraint[] { NSLayoutConstraint.Create (view, NSLayoutAttribute.Height, NSLayoutRelation.Equal, superView, NSLayoutAttribute.Height, 1, 0) });
		}

		public void AddConstraintLeft (UIView view)
		{
			this.AddConstraints (new NSLayoutConstraint[] { NSLayoutConstraint.Create (view, NSLayoutAttribute.Left, NSLayoutRelation.Equal, this, NSLayoutAttribute.Left, 1, 0) });
		}

		public void AddConstraintToCenterXY (UIView superview, UIView view)
		{
			this.AddConstraints (new NSLayoutConstraint[] { NSLayoutConstraint.Create (view, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, superview, NSLayoutAttribute.CenterX, 1, 0) });
			this.AddConstraints (new NSLayoutConstraint[] { NSLayoutConstraint.Create (view, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, superview, NSLayoutAttribute.CenterY, 1, 0) });
		}

		public void AddConstraintToCenterX (UIView view)
		{
			AddConstraintToCenterX (this, view);
		}

		public void AddConstraintToCenterX (UIView superView, UIView view)
		{
			this.AddConstraints (new NSLayoutConstraint[] { NSLayoutConstraint.Create (view, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, superView, NSLayoutAttribute.CenterX, 1, 0) });
		}

		public void AddConstraintToCenterY (UIView view)
		{
			AddConstraintToCenterY (this, view);
		}

		public void AddConstraintToCenterY (UIView superView, UIView view)
		{
			this.AddConstraints (new NSLayoutConstraint[] { NSLayoutConstraint.Create (view, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, superView, NSLayoutAttribute.CenterX, 1, 0) });
		}

		//		public void AddParentConstraint (string rule)
		//		{
		//			Parent.AddConstraints (NSLayoutConstraint.FromVisualFormat (rule, 0, new NSDictionary (), Parent.OurConstraints.Dictionary ()));
		//		}


		public UIImageView AddImageCenteredX (string name, string imageFileName)
		{
			var imageView = AddImage (name, imageFileName);
			AddConstraintToCenterX (this, imageView);
			return imageView;
		}

		public UIImageView AddImageCenteredY (string name, string imageFileName)
		{
			var imageView = AddImage (name, imageFileName);
			AddConstraintToCenterY (this, imageView);
			return imageView;
		}

		public UIImageView AddImageCenteredXY (string name, string imageFileName)
		{
			var imageView = AddImage (name, imageFileName);
			AddConstraintToCenterXY (this, imageView);
			return imageView;
		}

		public UIImageView AddImage (string name, string imageFileName)
		{
			UIImage image = UIImage.FromBundle (imageFileName);
			if (image == null) {
				throw new Exception ("Image file not found " + imageFileName);
			} 
			var imageView = new UIImageView (image);
			imageView.Frame = new RectangleF (10, 10, imageView.Image.CGImage.Width, imageView.Image.CGImage.Height);
			imageView.TranslatesAutoresizingMaskIntoConstraints = false;
			OurConstraints.ViewNames.Add (new NSString (name));
			OurConstraints.Views.Add (imageView);
			this.Add (imageView);
			return imageView;
		}

		public UILabel AddLabelCenteredX (UIView superView, string name, string text, UIColor color, float size)
		{
			UILabel label = AddLabel (name, text, color, size);
			AddConstraintToCenterX (superView, label);
			return label;
		}

		public UILabel AddLabelCenteredX (string name, string text, UIColor color, float size)
		{
			UILabel label = AddLabel (name, text, color, size);
			AddConstraintToCenterX (this, label);
			return label;
		}

		public UILabel AddLabelLeft (string name, string text, UIColor color, float size)
		{
			UILabel label = AddLabel (name, text, color, size);
			AddConstraintLeft (label);
			return label;
		}

		public UILabel AddLabelCenteredXY (string name, string text, UIColor color, float size)
		{
			UILabel label = AddLabel (name, text, color, size);
			AddConstraintToCenterXY (this, label);
			return label;
		}

		public UILabel AddLabel (string name, string text, UIColor color, float size)
		{
			var label = new UILabel ();
			label.Text = text;
			label.TextColor = color;
			label.Font = UIFont.FromName (Font, size);
			label.Frame = DefaultFrame ();
			label.TranslatesAutoresizingMaskIntoConstraints = false;
			label.LineBreakMode = UILineBreakMode.WordWrap;
			label.Lines = 0;  
			OurConstraints.ViewNames.Add (new NSString (name));
			OurConstraints.Views.Add (label);
			this.Add (label);
			return label;
		}

		public void AddVerticalGradient (UIColor startColor, UIColor endColor)
		{
			CAGradientLayer gradient = new CAGradientLayer ();
			gradient.Frame = this.Frame;
			gradient.Colors = new CGColor[] { startColor.CGColor, endColor.CGColor };
			this.Layer.AddSublayer (gradient);
		}

		public MDProgress AddSpinnerCenteredXY (string name, UIColor progressColor, UIColor trackColor, int trackWidth)
		{
			MDProgress spinner = AddSpinner (name, progressColor, trackColor, trackWidth);
			AddConstraintToCenterXY (this, spinner);
			return spinner;
		}
		// https://github.com/fpt-software/Material-Controls-For-iOS/tree/master/iOSUILibDemo
		// NuGet the Materials
		public MDProgress AddSpinner (string name, UIColor progressColor, UIColor trackColor, int trackWidth)
		{
			MDProgress mdProgress = new MDProgress ();
			mdProgress.ProgressColor = progressColor;
			if (trackColor != null) {
				mdProgress.TrackColor = trackColor;
				mdProgress.EnableTrackColor = true;
			}
			if (trackWidth > 0) {
				//mdProgress.TrackWidth = trackWidth;// Why can't I set this??
			}
			mdProgress.Type = (int)MDProgressType.Indeterminate;
			mdProgress.Style = (int)MDProgressStyle.Circular;
			mdProgress.TranslatesAutoresizingMaskIntoConstraints = false;
			OurConstraints.ViewNames.Add (new NSString (name));
			OurConstraints.Views.Add (mdProgress);
			this.Add (mdProgress);
			return mdProgress;
		}

		public UITextView AddTextCenteredX (string name, string text, UIColor textColor, UIColor backGroundColor, float size)
		{
			UITextView view = AddText (name, text, textColor, backGroundColor, size);
			AddConstraintToCenterX (this, view);
			return view;
		}

		public UITextView AddText (string name, string text, UIColor textColor, UIColor backgroundColor, float size)
		{
			var textView = new UITextView ();
			textView.Text = text;
			textView.TextColor = textColor;
			textView.BackgroundColor = backgroundColor;
			textView.Font = UIFont.FromName (Font, size);
			textView.Frame = DefaultFrame ();
			textView.TranslatesAutoresizingMaskIntoConstraints = false;
			OurConstraints.ViewNames.Add (new NSString (name));
			OurConstraints.Views.Add (textView);
			this.Add (textView);
			return textView;
		}

		public UIButton AddButton (string name, string text, UIColor backgroundColor, float size)
		{
			var buttonView = new UIButton ();
			buttonView.BackgroundColor = backgroundColor;
			buttonView.Frame = DefaultFrame ();
			buttonView.TranslatesAutoresizingMaskIntoConstraints = false;
			OurConstraints.ViewNames.Add (new NSString (name));
			OurConstraints.Views.Add (buttonView);
			this.Add (buttonView);
			return buttonView;
		}

		public UISegmentedControl AddSegmentedControl (string name, UIColor backgroundColor)
		{
			var segmentedControlView = new UISegmentedControl ();
			segmentedControlView.BackgroundColor = backgroundColor;
			segmentedControlView.Frame = DefaultFrame ();
			segmentedControlView.TranslatesAutoresizingMaskIntoConstraints = false;
			OurConstraints.ViewNames.Add (new NSString (name));
			OurConstraints.Views.Add (segmentedControlView);
			this.Add (segmentedControlView);
			return segmentedControlView;
		}

		public UISlider AddSlider (string name, UIColor backgroundColor)
		{
			var slider = new UISlider ();
			slider.BackgroundColor = backgroundColor;
			slider.Frame = DefaultFrame ();
			slider.TranslatesAutoresizingMaskIntoConstraints = false;
			OurConstraints.ViewNames.Add (new NSString (name));
			OurConstraints.Views.Add (slider);
			this.Add (slider);
			return slider;
		}

		public UISwitch AddSwitch (string name, UIColor backgroundColor)
		{
			var switchView = new UISwitch ();
			switchView.BackgroundColor = backgroundColor;
			switchView.Frame = DefaultFrame ();
			switchView.TranslatesAutoresizingMaskIntoConstraints = false;
			OurConstraints.ViewNames.Add (new NSString (name));
			OurConstraints.Views.Add (switchView);
			this.Add (switchView);
			return switchView;
		}

		public UIActivityIndicatorView AddActivityIndicator (string name, UIColor backgroundColor)
		{
			var activityIndicatorView = new UIActivityIndicatorView ();
			activityIndicatorView.BackgroundColor = backgroundColor;
			activityIndicatorView.Frame = DefaultFrame ();
			activityIndicatorView.TranslatesAutoresizingMaskIntoConstraints = false;
			OurConstraints.ViewNames.Add (new NSString (name));
			OurConstraints.Views.Add (activityIndicatorView);
			this.Add (activityIndicatorView);
			return activityIndicatorView;
		}

		public UIProgressView AddProgressView (string name, UIColor backgroundColor)
		{
			var progressView = new UIProgressView ();
			progressView.BackgroundColor = backgroundColor;
			progressView.Frame = DefaultFrame ();
			progressView.TranslatesAutoresizingMaskIntoConstraints = false;
			OurConstraints.ViewNames.Add (new NSString (name));
			OurConstraints.Views.Add (progressView);
			this.Add (progressView);
			return progressView;
		}

		public UIPageControl AddPageControl (string name, UIColor backgroundColor)
		{
			var pageControl = new UIPageControl ();
			pageControl.BackgroundColor = backgroundColor;
			pageControl.Frame = DefaultFrame ();
			pageControl.TranslatesAutoresizingMaskIntoConstraints = false;
			OurConstraints.ViewNames.Add (new NSString (name));
			OurConstraints.Views.Add (pageControl);
			this.Add (pageControl);
			return pageControl;
		}

		public UIStepper AddStepper (string name, UIColor backgroundColor)
		{
			var stepper = new UIStepper ();
			stepper.BackgroundColor = backgroundColor;
			stepper.Frame = DefaultFrame ();
			stepper.TranslatesAutoresizingMaskIntoConstraints = false;
			OurConstraints.ViewNames.Add (new NSString (name));
			OurConstraints.Views.Add (stepper);
			this.Add (stepper);
			return stepper;
		}

		public UIView AddView (string name, UIView view)
		{
			view.Frame = DefaultFrame ();
			view.TranslatesAutoresizingMaskIntoConstraints = false;
			OurConstraints.ViewNames.Add (new NSString (name));
			OurConstraints.Views.Add (view);
			this.Add (view);
			return view;
		}

		private RectangleF DefaultFrame ()
		{
			return new RectangleF (10, 10, 10, 10);
		}

		//https://developer.apple.com/library/ios/technotes/tn2154/_index.html
		//Pure approach
		public AutoLayoutContentView AddScrollView (string name, UIColor color)
		{
			var scrollView = new UIScrollView ();
			this.Add (scrollView);
			scrollView.Frame = new RectangleF (10, 10, 10, 10);// TODO: Is this still required?
			AutoLayoutContentView contentView = new AutoLayoutContentView (name, color, Font);
			contentView.Frame = new RectangleF (10, 10, 10, 10);// TODO: Is this still required?
			scrollView.Add (contentView);

			// Setup constraints
			scrollView.TranslatesAutoresizingMaskIntoConstraints = false;
			contentView.TranslatesAutoresizingMaskIntoConstraints = false;
			OurConstraints.ViewNames.Add (new NSString ("PrivateScrollView"));
			OurConstraints.Views.Add (scrollView);
			int width = (int)this.Bounds.Size.Width;
			int height = (int)this.Bounds.Size.Height;

			AddConstraint ("H:|[PrivateScrollView(" + width + ")]|");
			AddConstraint ("V:|[PrivateScrollView(>=" + height + ")]|");
			AutoLayoutConstraints ScrollViewConstraints = new AutoLayout.AutoLayoutConstraints ();
			ScrollViewConstraints.ViewNames.Add (new NSString ("PrivateContentView"));
			ScrollViewConstraints.Views.Add (contentView);
			AddConstraint ("H:|[PrivateContentView(" + width + ")]|", scrollView, ScrollViewConstraints);
			AddConstraint ("V:|[PrivateContentView(>=" + height + ")]|", scrollView, ScrollViewConstraints);

			OurConstraints.ViewNames.Add (new NSString (name));
			OurConstraints.Views.Add (contentView);
			contentView.Parent = Parent;
			return contentView;
		}

		public UITableView AddTableView (string name)
		{
			var tableView = new UITableView ();
			this.AddSubview (tableView);
			tableView.Frame = new RectangleF ((float)this.Bounds.X, (float)this.Bounds.Y, (float)this.Bounds.Size.Width, (float)this.Bounds.Size.Height);

			// Setup constraints
			tableView.TranslatesAutoresizingMaskIntoConstraints = false;
			OurConstraints.ViewNames.Add (new NSString (name));
			OurConstraints.Views.Add (tableView);

			return tableView;

		}


	}

}

