using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.ViewModels;
using Foundation;
using UIKit;
using AutoLayout;
using MvvmCrossAutoLayout.iOS;


namespace FSD.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : MvxApplicationDelegate
	{
		UIWindow _window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			_window = new UIWindow (UIScreen.MainScreen.Bounds);

			var setup = new Setup (this, _window);
			setup.Initialize ();

			var startup = Mvx.Resolve<IMvxAppStart> ();
			startup.Start ();


			_window.MakeKeyAndVisible ();

			//https://developer.xamarin.com/guides/cross-platform/application_fundamentals/notifications/ios/local_notifications_in_ios_walkthrough/
			//https://github.com/xamarin/monotouch-samples/blob/master/Notifications/AppDelegate.cs
			// check for a notification
			if (options != null) {
				// check for a local notification
				if (options.ContainsKey (UIApplication.LaunchOptionsLocalNotificationKey)) {
					var localNotification = options [UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;
					if (localNotification != null) {
						ReceivedLocalNotification (null, localNotification);
					}
				}
				// check for a remote notification
				if (options.ContainsKey (UIApplication.LaunchOptionsRemoteNotificationKey)) {

					NSDictionary remoteNotification = options [UIApplication.LaunchOptionsRemoteNotificationKey] as NSDictionary;
					if (remoteNotification != null) {
						ReceivedRemoteNotification (null, remoteNotification);
					}
				}
			}

			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes (
					                           UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null
				                           );

				app.RegisterUserNotificationSettings (notificationSettings);
				app.RegisterForRemoteNotifications ();

			} else {
				// We don't want to support iOS7 and below as they don't have AutoLayout.
				// throw new NotSupportedException("Only iOS8 and above are supported");
			}

			return true;
		}

		public override void ReceivedLocalNotification (UIApplication application, UILocalNotification notification)
		{
			// show an alert
			UIAlertController okayAlertController = UIAlertController.Create (notification.AlertAction, notification.AlertBody, UIAlertControllerStyle.Alert);
			okayAlertController.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, null));
			_window.RootViewController.PresentViewController (okayAlertController, true, null);

			// reset our badge
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
		}

		public override void ReceivedRemoteNotification (UIApplication application, NSDictionary userInfo)
		{
			//This method gets called whenever the app is already running and receives a push notification
			// YOU MUST HANDLE the notifications in this case.  Apple assumes if the app is running, it takes care of everything
			// this includes setting the badge, playing a sound, etc.
			processNotification (userInfo, false);
		}

		// https://roycornelissen.wordpress.com/2011/05/12/push-notifications-in-ios-with-monotouch/
		void processNotification (NSDictionary options, bool fromFinishedLaunching)
		{
			//Check to see if the dictionary has the aps key.  This is the notification payload you would have sent
			if (null != options && options.ContainsKey (new NSString ("aps"))) {
				//Get the aps dictionary
				NSDictionary aps = options.ObjectForKey (new NSString ("aps")) as NSDictionary;

				string alert = string.Empty;
				string sound = string.Empty;
				int badge = -1;

				//Extract the alert text
				//NOTE: If you're using the simple alert by just specifying "  aps:{alert:"alert msg here"}  "
				//      this will work fine.  But if you're using a complex alert with Localization keys, etc., your "alert" object from the aps dictionary
				//      will be another NSDictionary... Basically the json gets dumped right into a NSDictionary, so keep that in mind
				if (aps.ContainsKey (new NSString ("alert")))
					alert = (aps [new NSString ("alert")] as NSString).ToString ();

				//Extract the sound string
				if (aps.ContainsKey (new NSString ("sound")))
					sound = (aps [new NSString ("sound")] as NSString).ToString ();

				//Extract the badge
				if (aps.ContainsKey (new NSString ("badge"))) {
					string badgeStr = (aps [new NSString ("badge")] as NSObject).ToString ();
					int.TryParse (badgeStr, out badge);
				}

				//If this came from the ReceivedRemoteNotification while the app was running,
				// we of course need to manually process things like the sound, badge, and alert.
				if (!fromFinishedLaunching) {
					//Manually set the badge in case this came from a remote notification sent while the app was open
					if (badge >= 0)
						UIApplication.SharedApplication.ApplicationIconBadgeNumber = badge;
					//Manually play the sound
					if (!string.IsNullOrEmpty (sound)) {
						//This assumes that in your json payload you sent the sound filename (like sound.caf)
						// and that you've included it in your project directory as a Content Build type.
						SoundAndVibration.PlaySound (true, sound);
					}

					//Manually show an alert
					if (!string.IsNullOrEmpty (alert)) {
						UIAlertView avAlert = new UIAlertView ("Notification", alert, null, "OK", null);
						avAlert.Show ();
					}
				}
			}

			//You can also get the custom key/value pairs you may have sent in your aps (outside of the aps payload in the json)
			// This could be something like the ID of a new message that a user has seen, so you'd find the ID here and then skip displaying
			// the usual screen that shows up when the app is started, and go right to viewing the message, or something like that.
			if (null != options && options.ContainsKey (new NSString ("customKeyHere"))) {
				//		launchWithCustomKeyValue = (options[new NSString("customKeyHere")] as NSString).ToString();

				//You could do something with your customData that was passed in here
			}
		}

		/// <summary>
		/// The iOS will call the APNS in the background and issue a device token to the device. when that's
		/// accomplished, this method will be called.
		///
		/// Note: the device token can change, so this needs to register with your server application everytime
		/// this method is invoked, or at a minimum, cache the last token and check for a change.
		/// </summary>
		public override void RegisteredForRemoteNotifications (UIApplication application, NSData deviceToken)
		{
			// Get current device token
			var DeviceToken = deviceToken.Description;
			if (!string.IsNullOrWhiteSpace (DeviceToken)) {
				DeviceToken = DeviceToken.Trim ('<').Trim ('>');
			}

			// Get previous device token
			var oldDeviceToken = NSUserDefaults.StandardUserDefaults.StringForKey ("PushDeviceToken");

			// Has the token changed?
			if (string.IsNullOrEmpty (oldDeviceToken) || !oldDeviceToken.Equals (DeviceToken)) {
				//TODO: Put your own logic here to notify your server that the device token has changed/been created!
			}

			// Save new device token - change to use our database.
			NSUserDefaults.StandardUserDefaults.SetString (DeviceToken, "PushDeviceToken");
		}

		/// <summary>
		/// Registering for push notifications can fail, for instance, if the device doesn't have network access.
		///
		/// In this case, this method will be called.
		/// </summary>
		public override void FailedToRegisterForRemoteNotifications (UIApplication application, NSError error)
		{
			new UIAlertView ("Error registering push notifications", error.LocalizedDescription, null, "OK", null).Show ();
		}

	}


	//
	//	[Register ("AppDelegate")]
	//	public partial class AppDelegate : UIApplicationDelegate
	//	{
	//		// class-level declarations
	//		UIWindow window;
	//
	//		//
	//		// This method is invoked when the application has loaded and is ready to run. In this
	//		// method you should instantiate the window, load the UI into it and then make the window
	//		// visible.
	//		//
	//		// You have 17 seconds to return from this method, or iOS will terminate your application.
	//		//
	//		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
	//		{
	//			// create a new window instance based on the screen size
	//			window = new UIWindow (UIScreen.MainScreen.Bounds);
	//
	//			// If you have defined a root view controller, set it here:
	//			window.RootViewController = new SplashView();
	//
	//			// make the window visible
	//			window.MakeKeyAndVisible ();
	//
	//			return true;
	//		}
	//	}
}

