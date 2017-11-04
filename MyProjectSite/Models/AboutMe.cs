using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyProjectSite.Models
{
    public class AboutMe
    {
        public string Joke { get; set; }

        public static string GetAbout()
		{
			var client = new RestClient("https://icanhazdadjoke.com/");
			var request = new RestRequest();
			var response = new RestResponse();
            request.AddHeader("User-Agent", "Me");
            request.AddHeader("Accept", "application/json");
			Task.Run(async () =>
			{
				response = await GetResponseContentAsync(client, request) as RestResponse;
			}).Wait();
			JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
			var properResponse = (string)jsonResponse["joke"];
			return properResponse;
		}

		public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
		{
			var tcs = new TaskCompletionSource<IRestResponse>();
			theClient.ExecuteAsync(theRequest, response => {
				tcs.SetResult(response);
			});
			return tcs.Task;
		}
    }
}
