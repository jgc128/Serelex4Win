using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SerelexClient.Common;


namespace SerelexClient
{
	public class Serelex
	{
		//string BaseAddress = "http://serelex.it-claim.ru/";
		//string SearchAddress = "find/norm60-corpus-all/";
		//string suggestAddress = "http://trytoimagine.org:3000/suggest/";

		string BaseAddress = "http://serelex.cloudapp.net/";
		string SearchAddress = "find/norm60-corpus-all/";
		string SuggestAddress = "suggest/";


		int MaxResults = 30;

		ServiceClient<SearchResults> client;
		ServiceClient<JArray> suggestClient;
	
		const int searchPaneMaxSuggestions = 5;

		public Serelex()
		{
			client = new ServiceClient<SearchResults>();
			suggestClient = new ServiceClient<JArray>();

		}



		public async Task<SearchResults> Search(string Query)
		{
			//string url = String.Format("{0}{1}{2}/{3}", BaseAddress, SearchAddress, query, MaxResults);
			string url = BaseAddress + SearchAddress + Query + "/" + MaxResults;

			return await client.GetResult(url);
		}

		public async Task<List<string>> GetSuggestions(string Query)
		{
			List<string> suggest = new List<string>();

			//string url = String.Format("{0}{1}{2}", BaseAddress, SuggestAddress, Query);
			string url = BaseAddress + SuggestAddress + Query;

			var result = await suggestClient.GetResult(url);

			if (result == null)
			{
				return suggest;
			}


			var suggests = result[1];


			foreach (var s in suggests)
			{
				string ss = s.ToString();
				suggest.Add(ss);
				if (suggest.Count >= searchPaneMaxSuggestions)
				{
					break;
				}
			}

			return suggest;
		}


	}
}
