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

namespace Georakel.Messages
{
	class FromChosenMessage : BusStopMessage
	{
		public FromChosenMessage (object sender, BusStop busStop) : base(sender, busStop)
		{
		}
		
	}
}

