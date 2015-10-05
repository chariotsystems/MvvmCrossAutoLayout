using System;
using System.Collections.Generic;
using Foundation;

/*
 * Author: Alex Eadie
 * https://github.com/chariotsystems/MvvmCrossAutoLayout
 */
namespace AutoLayout
{
	public class AutoLayoutConstraints
	{

		public List<NSObject> Views { get; set; }

		public List<NSObject> ViewNames  { get; set; }

		public AutoLayoutConstraints ()
		{
			Views = new List<NSObject> ();
			ViewNames = new List<NSObject> ();
		}

		public NSDictionary Dictionary ()
		{
			return NSDictionary.FromObjectsAndKeys (Views.ToArray (), ViewNames.ToArray ());
		}
	}
}

