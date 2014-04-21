using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Georakel
{
	public partial class AskViewController : UIViewController
	{
		public AskViewController() : base ("AskViewController", null)
		{
			Title = NSBundle.MainBundle.LocalizedString("First", "First");
			TabBarItem.Image = UIImage.FromBundle("first");
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad();
			

		}

	}
}

