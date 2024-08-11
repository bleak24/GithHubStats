namespace GitHubStats.Models
{
    public class GitHubFileInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Download_Url { get; set; }
        public GitHubLinkFields _Links { get; set; }

        public class GitHubLinkFields
        {
            public string Self { get; set; }
        }
    }
}
