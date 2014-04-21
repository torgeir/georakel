using System;
using System.IO;
using System.Linq;
using Georakel.Core.Domain;
using NUnit.Framework;
using System.Xml;

namespace Georakel.Core.Tests
{
	[TestFixture]
	public class BusStopServiceTest
	{
		BusStopService Service;

		[Test]
		public void Parses_bus_stops ()
		{
			var service = new BusStopService(XmlReader.Create("../../Resources/busstops.xml"));

			BusStop firstStop = service.BusStops.First();

			Assert.AreEqual(firstStop, new BusStop("Risvollvegen", "63.3945372", "10.4236422"));
		}

	}
}

