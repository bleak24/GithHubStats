using System.Collections.Generic;

namespace GitHubStats.Models
{
    public class GitHubDirectory
    {
        public string Name { get; set; }
        public List<GitHubDirectory> SubDirs { get; set; }
    }
}
