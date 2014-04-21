using System;
using Georakel.Core.Domain;
using TinyMessenger;

namespace Georakel.Messages
{
	public class BusStopMessage : GenericTinyMessage<BusStop>
	{

		public BusStopMessage (object sender, BusStop busStop) 
			: base(sender, busStop)
		{
		}
	}
}

