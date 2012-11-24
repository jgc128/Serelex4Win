
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerelexClient
{
	public class SearchResults
	{
		public string Word { get; set; }
		public string Model { get; set; }

		public List<SearchResultItem> Relations { get; set; }

		public override string ToString()
		{
			return Word;
		}
	}
}
