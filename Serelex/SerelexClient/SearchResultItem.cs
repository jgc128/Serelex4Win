using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerelexClient
{
	public class SearchResultItem
	{
		public string Word { get; set; }
		public double Value { get; set; }
		public bool HaveIcon { get; set; }

		public override string ToString()
		{
			return String.Format("{0} - {1:0.###}",Word, Value);
		}
	}
}
