using System;
using System.Runtime.Serialization;

namespace BookingPlaceInRestaurant.Models.Chart
{
    [DataContract]
	public class DataPoint
    {
		public DataPoint(string label, double y)
		{
			this.Label = label;
			this.Y = y;
		}
		[DataMember(Name = "label")]
		public string Label = "";
		[DataMember(Name = "y")]
		public Nullable<double> Y = null;
	}
}
