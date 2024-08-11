using System.Collections.Generic;

namespace GitHubStats.Models
{
    public class GitHubFileStats
    {
        public string FileName { get; set; }

        public List<GitHubFileOccurrences> FileOccurrences { get; set; }

        public GitHubFileStats(string fileName, List<GitHubFileOccurrences> fileOccurences)
        {
            this.FileName = fileName;
            this.FileOccurrences = fileOccurences;
        }
    }

    public class GitHubFileOccurrences
    {
        public char Char { get; set; }
        public int Count { get; set; }

    }
}


