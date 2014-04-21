using System;
using System.Linq;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MapKit;
using MonoTouch.CoreLocation;
using Georakel.Core;
using Georakel.Core.Domain;
using TinyMessenger;
using Georakel.Messages;

namespace Georakel
{
	public partial class MapViewController : UIViewController
	{

		ITinyMessengerHub messages;

		BusStopService busStopService;
		
		public MapViewController(ITinyMessengerHub messages, BusStopService busStopService) : base("MapViewController", null)
		{
			this.messages = messages;
			this.busStopService = busStopService;

			Title = NSBundle.MainBundle.LocalizedString("Map", "Map");
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
			
			var map = new MKMapView(UIScreen.MainScreen.Bounds);

			map.GetViewForAnnotation += GetViewForAnnotation;
			map.CalloutAccessoryControlTapped += CalloutAccessoryControlTapped;

			BusStop studentersamfundet = busStopService.FindBusStopWithName("Studentersamfundet");
			map.SetRegion(
				new MKCoordinateRegion(
					new CLLocationCoordinate2D(studentersamfundet.Lat, studentersamfundet.Lon),
					new MKCoordinateSpan(0.1, 0.1)),
				true);

			map.AddAnnotations(busStopService.BusStops
				.Select(stop => new MKPointAnnotation() {
					Title = stop.Name,
					Coordinate = new CLLocationCoordinate2D(stop.Lat, stop.Lon)
				})
				.ToArray());

			View = map;
		}


		MKAnnotationView GetViewForAnnotation (MKMapView mapView, NSObject annotation)
		{
			string pid = "pin-annotation-id";

			if (annotation is MKUserLocation)
				return null;

			MKPinAnnotationView pinView = mapView.DequeueReusableAnnotation(pid) as MKPinAnnotationView;

			if (pinView == null)
			{
				pinView = new MKPinAnnotationView(annotation, pid);
			}

			pinView.PinColor = MKPinAnnotationColor.Red;
			pinView.CanShowCallout = true;
			pinView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);

			return pinView;
		}
			
		void CalloutAccessoryControlTapped (object sender, MKMapViewAccessoryTappedEventArgs e)
		{

			var annotation = e.View.Annotation as MKPointAnnotation;
			var title = annotation.Title;

			UIActionSheet actionSheet = new UIActionSheet("Travel", null, "Cancel", null, new string[] { "From", "To" });
			actionSheet.Clicked += (so, se) => {
				var busStop = busStopService.FindBusStopWithName(title);
				switch (se.ButtonIndex)
				{
				case 0: messages.Publish(new FromChosenMessage(this, busStop)); break;
				case 1: messages.Publish(new ToChosenMessage(this, busStop)); break;
				}
			};
			actionSheet.Style = UIActionSheetStyle.Default;
			actionSheet.ShowInView(TabBarController.View);
		}

	}
}

