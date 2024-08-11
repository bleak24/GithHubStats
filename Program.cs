using GitHubStats.Core;
using System;
using System.Threading.Tasks;

namespace GitHubStats
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{DateTime.Now} - Task started");

            try
            {
                Task task = GitHubCore.GetRepositoryFiles();
                task.Wait();

                if (task.IsCompleted && task.Status.Equals(TaskStatus.RanToCompletion))
                {
                    var result = GitHubCore.GetFinalStats();

                    Console.WriteLine($"{DateTime.Now} - Overall counters:");
                    Console.WriteLine();

                    foreach (var item in result)
                    {
                        Console.WriteLine($"Char: {item.Char}, appeared {item.Count} times");
                    }
                }
                else
                {
                    Console.WriteLine($"{DateTime.Now} - Task not completed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} - {ex.InnerException}: {ex.Message}");
            }
            finally
            {

                Console.WriteLine();
                Console.WriteLine($"{DateTime.Now} - Task ended");
                Console.WriteLine();

                Console.WriteLine(Console.ReadKey());
            }
        }
    }
}
