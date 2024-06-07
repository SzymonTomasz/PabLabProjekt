using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientApplication.Pages
{
    public class GrpcClientPageModel : PageModel
    {
        [BindProperty]
        public string EntitiesCount { get; set; } = "Brak ³¹cznoœci z serwerem gRPC!"; // Domyœlna zawartoœæ napisu

        public void OnGet()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7101"); // Utworzenie kana³u gRPC
            var client = new Statistics.StatisticsClient(channel); // Utworzenie klienta gRPC
            var response = client.GetStatistics(new Google.Protobuf.WellKnownTypes.Empty()); // Wywo³anie metody GetStatistics (metoda jest tworzona automatycznie na podstawie pliku .proto)
            EntitiesCount = "Liczba zapytañ zadanych API przez autoryzowanych u¿ytkowników: " + response.Counter.ToString(); // Wstawienie wyniku do zmiennej EntitiesCount
        }
    }
}
