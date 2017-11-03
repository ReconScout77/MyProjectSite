using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyProjectSite.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Html_Url { get; set; }
        public string Language { get; set; }
        public int Stargazers_Count { get; set; }

        public static List<Project> GetProjects()
        {
            var client = new RestClient("https://api.github.com");
            var request = new RestRequest("/users/ReconScout77/repos?per_page=5&sort=updated", Method.GET);
            request.AddHeader("User-Agent", "ReconScout77");
            var response = new RestResponse();
            Task.Run(async () => 
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(response.Content);
            var projectList = JsonConvert.DeserializeObject<List<Project>>(jsonResponse.ToString());
            return projectList;
        }

		public static List<Project> GetStarredProjects()
		{
			var client = new RestClient("https://api.github.com");
			var request = new RestRequest("/search/repositories?q=user:ReconScout77&sort=stars&per_page=3", Method.GET);
			request.AddHeader("User-Agent", "ReconScout77");
			var response = new RestResponse();
			Task.Run(async () =>
			{
				response = await GetResponseContentAsync(client, request) as RestResponse;
			}).Wait();
			JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
			var projectList = JsonConvert.DeserializeObject<List<Project>>(jsonResponse["items"].ToString());
			return projectList;
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
