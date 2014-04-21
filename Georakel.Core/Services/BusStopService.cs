using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using Georakel.Core.Domain;

namespace Georakel.Core
{
	public class BusStopService
	{
			
		public IEnumerable<BusStop> BusStops {
			get
			{
				if (_busStops == null)
				{
					_busStops = ParseBusStops();
				}
				return _busStops;
			}
		}

		public BusStop FindBusStopWithName (string Name)
		{
			return BusStops
				.Where(stop => stop.Name.Equals(Name))
				.First();
		}

		private IEnumerable<BusStop> ParseBusStops ()
		{
			using(var reader = XmlReader)
			{
				var xml = XDocument.Load(reader);

				return new List<BusStop>(
					from osm in xml.Elements("osm")
					from node in osm.Elements("node")
					from tag in node.Elements("tag")
						where tag.Attribute("k").Value == "name"
							let name = tag.Attribute("v").Value
							let lat = node.Attribute("lat").Value
							let lon = node.Attribute("lon").Value
								select new BusStop(name, lat, lon));
			}
		}

		IEnumerable<BusStop> _busStops;

		XmlReader XmlReader;

		public BusStopService (XmlReader xmlReader)
		{
			this.XmlReader = xmlReader;
		}
	}

}

