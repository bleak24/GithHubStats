using GitHubStats.Helpers;
using System.Collections.Generic;
using System.Net.Http;

namespace GitHubStats.Models
{
    public class GitHubClient
    {
        public string AccessToken { get; set; }
        public HttpClient HttpClient { get; set; }
        public List<string> FileExtensions
        {
            get { return ConfigHelper.GetTargetExtensions(); }
        }

        public GitHubClient(string accessToken, HttpClient httpClient)
        {
            AccessToken = accessToken;
            HttpClient = httpClient;
        }
    }
}
