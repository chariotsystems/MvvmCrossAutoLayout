using System;
using UIKit;

namespace MvvmCrossAutoLayout.iOS
{
	public class ColorConversion
	{
		//TODO: fixme - not sure why this does not work - use debugger.
		public static UIColor GetColorFromHexString (string hexString, uint alpha = 0xFF)
		{
			// bypass '#' character
			uint rgbValue = Convert.ToUInt32 (hexString.Substring (1), 16);
			return UIColor.FromRGBA ((rgbValue & 0xFF0000) >> 16, (rgbValue & 0xFF00) >> 8, (rgbValue & 0xFF), alpha);
		}

	}
}

