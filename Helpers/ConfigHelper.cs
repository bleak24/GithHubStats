using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace GitHubStats.Helpers
{
    public static class ConfigHelper
    {
        private static string GetConfigKeyValue(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        internal static List<string> GetTargetExtensions()
        {
            return GetConfigKeyValue("GitHubTargetExtensions").Split(',').Select(x=>(x.Trim()).ToUpper()).ToList();
        }
        internal static string GetAccessToken()
        {
            return GetConfigKeyValue("GitHubAccessToken");
        }
        internal static string GetRepositoryName()
        {
            return GetConfigKeyValue("GitHubRepositoryName");
        }
    }
}
