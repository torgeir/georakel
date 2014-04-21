using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Georakel.Core;
using Georakel.Core.Domain;
using TinyMessenger;
using Georakel.Messages;

namespace Georakel
{
	public partial class OracleViewController : UIViewController
	{
		OracleService atbOracleService;

		BusStop fromStop, toStop;

		string LastResponse = string.Empty;

		public OracleViewController(ITinyMessengerHub messenger, OracleService atbOracleService) : base("OracleViewController", null)
		{
			this.atbOracleService = atbOracleService;

			messenger.Subscribe<FromChosenMessage>(message => {
				fromStop = message.Content;
				QueryAtb();
			});

			messenger.Subscribe<ToChosenMessage>(message => {
				toStop = message.Content;
				QueryAtb();
			});

			Title = NSBundle.MainBundle.LocalizedString("Oracle", "Oracle");
			TabBarItem.Image = UIImage.FromBundle("First");
		}

		public void QueryAtb()
		{
			if (fromStop != null && toStop != null)
			{
				atbOracleService.Ask(fromStop, toStop, result => {
					if (result.HasError())
					{
						LastResponse = "Querying the oracle failed: " + result.Error.Message;
					}
					else
					{
						LastResponse = result.Value;
					}
					UpdateResponseLabel();
				});
			}
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
			UpdateResponseLabel();
		}

		void UpdateResponseLabel()
		{
			InvokeOnMainThread(() => {
				if (IsViewLoaded) ResultTextView.Text = LastResponse;
			});
		}
	}
}

