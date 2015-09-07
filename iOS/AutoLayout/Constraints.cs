using System;
using System.Collections.Generic;
using Foundation;

namespace AutoLayout
{
	public class Constraints {

		public List<NSObject> Views { get; set; }  
		public List<NSObject> ViewNames  { get; set; }  

		public Constraints (){
			Views = new List<NSObject> ();
			ViewNames = new List<NSObject> ();
		}

		public NSDictionary Dictionary(){
			return NSDictionary.FromObjectsAndKeys (Views.ToArray (), ViewNames.ToArray ());
		}
	}
}

