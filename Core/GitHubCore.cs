using GitHubStats.Helpers;
using GitHubStats.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubStats.Core
{
    public static class GitHubCore
    {
        internal static List<GitHubFileData> GitHubFiles = new List<GitHubFileData>();    
        internal static async Task<GitHubDirectory> GetRepositoryFiles()
        {
            GitHubClient client = new GitHubClient(ConfigHelper.GetAccessToken(), new HttpClient());

            GitHubDirectory result = await GetDirectoryFiles("root", String.Format("https://api.github.com/repos/{0}/contents/", ConfigHelper.GetRepositoryName()), client);
            client.HttpClient.Dispose();

            return result;
        }

        private static async Task<GitHubDirectory> GetDirectoryFiles(string dirName, string uri, GitHubClient client)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
                request.Headers.Add("Authorization",
                    "Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(string.Format("{0}:{1}", client.AccessToken, "x-oauth-basic"))));
                request.Headers.Add("User-Agent", "github-client");

                HttpResponseMessage response = await client.HttpClient.SendAsync(request);
                string jsonStr = await response.Content.ReadAsStringAsync();
                response.Dispose();

                List<GitHubFileInfo> dirContents = JsonConvert.DeserializeObject<List<GitHubFileInfo>>(jsonStr);

                GitHubDirectory result = new GitHubDirectory();
                result.Name = dirName;
                result.SubDirs = new List<GitHubDirectory>();

                foreach (GitHubFileInfo file in dirContents)
                {
                    if (file.Type.Equals("dir"))
                    {
                        GitHubDirectory sub = await GetDirectoryFiles(file.Name, file._Links.Self, client);
                        result.SubDirs.Add(sub);
                    }
                    else
                    {
                        string fileExtension = StringHelper.GetFileExtension(file.Name);

                        if (client.FileExtensions.Contains(fileExtension))
                        {
                            string content = await GetFileContent(file.Download_Url, client);

                            GitHubFiles.Add(new GitHubFileData(file.Name, content));
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task<string> GetFileContent(string downloadURL, GitHubClient client)
        {
            try
            {
                HttpRequestMessage downLoadUrl = new HttpRequestMessage(HttpMethod.Get, downloadURL);
                downLoadUrl.Headers.Add("Authorization",
                    "Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(string.Format("{0}:{1}", client.AccessToken, "x-oauth-basic"))));
                downLoadUrl.Headers.Add("User-Agent", "github-client");

                HttpResponseMessage contentResponse = await client.HttpClient.SendAsync(downLoadUrl);
                string content = await contentResponse.Content.ReadAsStringAsync();
                contentResponse.Dispose();

                return content;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static List<GitHubFileOccurrences> GetFinalStats()
        {
            List<GitHubFileStats> fileStats = new List<GitHubFileStats>();

            foreach (GitHubFileData fileData in GitHubFiles)
            {
                fileStats.Add(GitHubHelper.GetCharOcurrencesInFile(fileData));
            }
            
            return GitHubHelper.GetFinalFileOccurrences(fileStats);
        }

        
    }
}
