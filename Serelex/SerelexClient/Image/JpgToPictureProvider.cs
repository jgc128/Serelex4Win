using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerelexClient.Common;

namespace SerelexClient.Image
{
	public class JpgToPictureProvider : IPictureProvider
	{
		static string uri = "http://{0}.jpg.to/m";

		public async Task<Uri> GetPictureFromQuery(string Query)
		{
			string q = Query.Replace(" ", "");
			string u = String.Format(uri, q);

			string result = await ServiceClient<string>.MakeAsyncRequest(u);

			int src = result.IndexOf("src=") + 5;
			int last = result.LastIndexOf('"');
			string imgUri = result.Substring(src, last - src);

			return new Uri(imgUri, UriKind.Absolute);
		}
	}
}
