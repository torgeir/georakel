using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MapKit;

namespace Georakel
{
	public partial class MapViewController : UIViewController
	{
		public MapViewController() : base("MapViewController", null)
		{
			Title = NSBundle.MainBundle.LocalizedString("Second", "Second");
			TabBarItem.Image = UIImage.FromBundle("second");
		}

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
			MKMapView map = new MKMapView(UIScreen.MainScreen.Bounds);
			View = map;
		}
			
	}
}

