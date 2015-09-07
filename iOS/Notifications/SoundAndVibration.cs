using System;

// https://developer.xamarin.com/recipes/ios/media/sound/syssound-example/
using Foundation;
using AudioToolbox;


namespace MvvmCrossAutoLayout.iOS
{
	public class SoundAndVibration
	{
		public static void PlaySound (bool withVibrate, string fileName)
		{
			NSUrl	url = NSUrl.FromFilename (fileName);
			SystemSound systemSound = new SystemSound (url);
			if (withVibrate) {
				systemSound.PlayAlertSound ();
			} else {
				systemSound.PlaySystemSound ();
			}
		}

		public static void VibrateOnly ()
		{
			SystemSound.Vibrate.PlaySystemSound ();
		}
	}
}

