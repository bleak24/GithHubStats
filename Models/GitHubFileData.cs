namespace GitHubStats.Models
{
    public class GitHubFileData
    {
        public string Name { get; set; }
        public string Content { get; set; }

        public GitHubFileData(string name, string content)
        {
            Name = name;
            Content = content;
        }
    }
}
