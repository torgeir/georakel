using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Georakel.Core;
using TinyMessenger;

namespace Georakel
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		UITabBarController tabBarController;
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			window = new UIWindow(UIScreen.MainScreen.Bounds);

			var messenger = new TinyMessengerHub();

			var busStopService = new BusStopService(XmlReader.Create("busstops.xml"));
			var oracleService = new OracleService();

			var mapController = new MapViewController(messenger, busStopService);
			var oracleController = new OracleViewController(messenger, oracleService);

			tabBarController = new UITabBarController();
			tabBarController.ViewControllers = new UIViewController [] { mapController, oracleController };
			
			window.RootViewController = tabBarController;
			window.MakeKeyAndVisible();
			
			return true;
		}
	}
}

