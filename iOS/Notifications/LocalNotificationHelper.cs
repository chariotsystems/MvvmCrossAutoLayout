using System;
using UIKit;
using Foundation;

//https://developer.xamarin.com/guides/cross-platform/application_fundamentals/notifications/ios/local_notifications_in_ios_walkthrough/
namespace MvvmCrossAutoLayout.iOS
{
	public class LocalNotificationHelper
	{
		public static void SendLocalNotification ()
		{
			// create the notification
			var notification = new UILocalNotification ();

			// set the fire date (the date time in which it will fire)
			notification.FireDate = NSDate.FromTimeIntervalSinceNow (15);

			// configure the alert
			notification.AlertAction = "View Alert";
			notification.AlertBody = "Your one minute alert has fired!";

			// modify the badge
			notification.ApplicationIconBadgeNumber = 1;

			// set the sound to be the default sound
			notification.SoundName = UILocalNotification.DefaultSoundName;

			// schedule it
			UIApplication.SharedApplication.ScheduleLocalNotification (notification);
		}
	}
}

