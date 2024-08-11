using GitHubStats.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GitHubStats.Helpers
{
    public class GitHubHelper
    {
        internal static GitHubFileStats GetCharOcurrencesInFile(GitHubFileData fileData)
        {
            string content = fileData.Content;
            string strippedString = StringHelper.StripString(content);

            List<GitHubFileOccurrences> fileOccurences = strippedString.GroupBy(c => c).Select(c => new GitHubFileOccurrences { Char = c.Key, Count = c.Count()}).ToList();

            return new GitHubFileStats(fileData.Name, fileOccurences);
        }

        internal static List<GitHubFileOccurrences> GetFinalFileOccurrences(List<GitHubFileStats> fileStatsList)
        {
            return fileStatsList.Select(x => x.FileOccurrences).SelectMany(l => l).GroupBy(x => x.Char).Select(g => new GitHubFileOccurrences { Char = g.Key, Count = g.Sum(x => x.Count) }).OrderByDescending(x => x.Count).ToList();
        }
    }
}
