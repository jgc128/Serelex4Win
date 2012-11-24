using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerelexClient.Common;

namespace SerelexClient
{
    public class Serelex
    {
		string BaseAddress = "http://serelex.it-claim.ru/";
		string SearchAddress = "find/norm60-corpus-all/";

		int MaxResults = 30;

		ServiceClient<SearchResults> client;

		public Serelex()
		{
			client = new ServiceClient<SearchResults>();

		}

		public async Task<SearchResults> Search(string query)
		{
			string url = String.Format("{0}{1}{2}/{3}", BaseAddress, SearchAddress, query, MaxResults);

			return await client.GetResult(url);
		}
    }
}
