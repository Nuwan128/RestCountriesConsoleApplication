
using RestCountryLibrary;

Console.Write("Enter the desired download path (including filename): ");

string downloadPath = Console.ReadLine();

Console.WriteLine("Hello, World!");
CountryDataDownloader.DownloadAndSaveDataAsync(downloadPath).Wait();


