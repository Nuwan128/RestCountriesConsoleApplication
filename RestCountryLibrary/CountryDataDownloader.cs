using RestCountryLibrary.Models;
using System.Text.Json;

namespace RestCountryLibrary;

public class CountryDataDownloader
{

    private const string ApiUrl = "https://restcountries.com/v2/all";

    public static async Task DownloadAndSaveDataAsync(string downloadPath)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(ApiUrl);
                response.EnsureSuccessStatusCode();

                string jsonString = await response.Content.ReadAsStringAsync();

                JsonElement root = JsonSerializer.Deserialize<JsonElement>(jsonString);

                List<Country> countries = new List<Country>();

                foreach (JsonElement element in root.EnumerateArray())
                {
                    Country country = new Country();

                    if (element.TryGetProperty("alpha2Code", out JsonElement codeElement))
                    {
                        country.Code = codeElement.GetString();
                    }
                    if (element.TryGetProperty("name", out JsonElement nameElement))
                    {
                        country.Name = nameElement.GetString();
                    }

                    countries.Add(country);
                }

                string jsonData = JsonSerializer.Serialize(countries, new JsonSerializerOptions { });

                await File.WriteAllTextAsync(downloadPath, jsonData);

                Console.WriteLine($"Data downloaded and saved to: {downloadPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing data: {ex.Message}");
            }
        }
    }
}
