using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace GitHubStats.Helpers
{
    public static class StringHelper
    {
        internal static string GetFileExtension(string fileName)
        {
            try
            {
                return Path.GetExtension(fileName).Split('.').LastOrDefault().ToUpper();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static string StripString(string content)
        {
            if (!string.IsNullOrWhiteSpace(content))
            {
                return Regex.Replace(content.ToLower(), @"[^a-zA-Z]", "");
            }

            return null;
        }
    }
}
