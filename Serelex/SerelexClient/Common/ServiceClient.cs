using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SerelexClient.Common
{
	/// <summary>
	/// This class represent client of the REST-service
	/// </summary>
	/// <see cref="http://weblogs.asp.net/jeff/archive/2012/10/31/portable-class-libraries-and-fetching-json.aspx"/>
	/// <typeparam name="T">Type of the result</typeparam>
	class ServiceClient<T> where T : class
	{
		string url;

		public ServiceClient(string Url)
		{
			url = Url;
		}

		public ServiceClient()
		{
 
		}

		public async Task<T> GetResult()
		{
			var response = await MakeAsyncRequest(url);
			var result = JsonConvert.DeserializeObject<T>(response);
			return result;
		}

		public async Task<T> GetResult(string Url)
		{
			url = Url;
			return await this.GetResult();
		}

		public static Task<string> MakeAsyncRequest(string Url)
		{
			var request = (HttpWebRequest)WebRequest.Create(Url);
			//request.ContentType = "application/json";
			Task<WebResponse> task = Task.Factory.FromAsync(
				request.BeginGetResponse,
				asyncResult => request.EndGetResponse(asyncResult),
				null);
			return task.ContinueWith(t => ReadStreamFromResponse(t.Result));
		}

		private static string ReadStreamFromResponse(WebResponse response)
		{
			using (var responseStream = response.GetResponseStream())
			using (var reader = new StreamReader(responseStream))
			{
				var content = reader.ReadToEnd();
				return content;
			}
		}
	}
}
