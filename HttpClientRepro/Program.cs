using System;
using System.Threading.Tasks;

namespace HttpClientRepro
{
    public static class Program
    {
        static void Main(string[] args)
        {
            NewMethod();
        }

        private static async Task NewMethod()
        {
            await Fetch();
            await Fetch();
        }

        private static async Task Fetch()
        {
            try
            {
                var client = new System.Net.Http.HttpClient()
                {
                    DefaultRequestHeaders = { { "origin", "WindowsCalculator" } }
                };

                var r = await client.GetStringAsync(new Uri("https://cors-anywhere.herokuapp.com/https://go.microsoft.com/fwlink/?linkid=2041093&localizeFor=en-US"));

                Console.WriteLine(r);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
