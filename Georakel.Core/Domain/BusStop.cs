using System;

namespace Georakel.Core.Domain
{
	public class BusStop
	{
		public string Name { get; private set; }

		public double Lat { get; private set; }

		public double Lon { get; private set; }

		public BusStop (string Name, double Lat, double Lon)
		{
			this.Name = Name;
			this.Lat = Lat;
			this.Lon = Lon;
		}

		public BusStop (string Name, string Lat, string Lon) :
			this(Name, Convert.ToDouble(Lat), Convert.ToDouble(Lon)) { }

		public override string ToString ()
		{
			return string.Format("[BusStop: Name={0}, Lat={1}, Lon={2}]", Name, Lat, Lon);
		}

		public override bool Equals (object obj)
		{
			if (obj == null)
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (obj.GetType() != typeof(BusStop))
				return false;
			BusStop other = (BusStop) obj;
			return Name == other.Name && Lat == other.Lat && Lon == other.Lon;
		}
		

		public override int GetHashCode ()
		{
			unchecked {
				return (Name != null ? Name.GetHashCode() : 0) ^ (Lat != null ? Lat.GetHashCode() : 0) ^ (Lon != null ? Lon.GetHashCode() : 0);
			}
		}
		
	}
}

