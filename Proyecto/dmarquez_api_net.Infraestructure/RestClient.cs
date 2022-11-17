using System.Text.Json;
using dmarquez_api_net.Domain;
using RestSharp;
namespace dmarquez_api_net.Infraestructure;

public class ClienteRest:IClienteRest
{

    public RickAndMorty GetData()
    {
        var client = new RestClient("https://rickandmortyapi.com/api/character");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        IRestResponse response = client.Execute(request);
        Console.WriteLine(response.Content);

        var RickAndMorty = JsonSerializer.Deserialize<RickAndMorty>(response.Content);

        return RickAndMorty;
    }
}
