using System;
using System.Threading.Tasks;

namespace HttpClientRepro
{
public static class Program
{
    static async Task Main(string[] args)
    {
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

            var r = await client.GetAsync(new Uri("https://cors-anywhere.herokuapp.com/https://www.google.com/images/branding/googlelogo/1x/googlelogo_light_color_272x92dp.png"));
            Console.WriteLine($"Got Response");
            var c = await r.Content.ReadAsByteArrayAsync();
            Console.WriteLine($"Got {c.Length} bytes");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failure: " + e);
        }
    }
}
}
