using TestAssignmentDesktop.CoinCap.Client;
using TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoinById.Request;
using TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoins.Request;

var coinClient = new CoinCapClient(new HttpClient());
Console.Write("Input name crypto coin: ");
string search = Console.ReadLine();
Console.Write("Input limit: ");
int limit = Convert.ToInt32(Console.ReadLine());
var request = new GetCoinsRequest(search, limit, 0);
var response = await coinClient.GetAssetsAsync(request);

foreach (var coin in response)
{
    Console.WriteLine($"{coin.Rank} {coin.Name} ({coin.Symbol})");
}


//Console.Write("Input name crypto coin: ");
//string search = Console.ReadLine();
//var request = new GetCoinByIdRequest(search);
//var response = await coinClient.GetAssetByIdAsync(request);
//Console.WriteLine($"{response.Data.Rank} - {response.Data.Name} - ({response.Data.Symbol})");