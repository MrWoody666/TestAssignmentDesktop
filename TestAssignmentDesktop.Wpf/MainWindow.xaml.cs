using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TestAssignmentDesktop.CoinCap.Client;
using TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoinById.Request;
using TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoins.Request;

namespace TestAssignmentDesktop.Wpf;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        CryptoCoinsList();
        CryptocurrencyListView.SelectionChanged +=  ViewDetailsCoin;
        SearchBox.TextChanged += SearchBox_TextChanged;
    }

    public async Task CryptoCoinsList()
    {
        var coinClient = new CoinCapClient(new HttpClient());
        var request = new GetCoinsRequest("", 10, 0);
        var coinsListResponse = await coinClient.GetAssetsAsync(request);
        foreach (var coins in coinsListResponse)
        {
            var listItem = new ListViewItem();
            listItem.Content = $"{coins.Rank} - {coins.Name} - ({coins.Symbol})";
            listItem.Tag = coins.Id;
            CryptocurrencyListView.Items.Add(listItem);
        }
    }

    private async void ViewDetailsCoin(object sender, SelectionChangedEventArgs e)
    {
        var coinClient = new CoinCapClient(new HttpClient());
        if (CryptocurrencyListView.SelectedItem != null)
        {
            ListViewItem item = (CryptocurrencyListView.SelectedItem as ListViewItem);
            string coinId = item.Tag.ToString();
            var request = new GetCoinByIdRequest(coinId);
            var coinByIdResponse = await coinClient.GetAssetByIdAsync(request);
            CryptocurrencyDetails.DataContext = coinByIdResponse.Data;
        }
    }

    private async void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var coinClient = new CoinCapClient(new HttpClient());
        string searchText = SearchBox.Text.Trim();
        var request = new GetCoinsRequest(searchText, 2000, 0);
        var cryptocurrencies = await coinClient.GetAssetsAsync(request);

        if (searchText != "")
        {
            CryptocurrencyListView.Items.Clear();
            foreach (var coins in cryptocurrencies)
            {
                var listItem = new ListViewItem();
                listItem.Content = $"{coins.Rank} - {coins.Name} - ({coins.Symbol})";
                listItem.Tag = coins.Id;
                CryptocurrencyListView.Items.Add(listItem);
            }
        }
    }
}