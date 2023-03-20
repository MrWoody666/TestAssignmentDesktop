using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
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
        CryptocurrencyListView.SelectionChanged += ViewDetailsCoin;
        SearchBox.TextChanged += SearchBoxTextChanged;
    }

    public async Task CryptoCoinsList()
    {
        var coinClient = new CoinCapClient(new HttpClient());
        var request = new GetCoinsRequest("", 10, 0);
        var coinsListResponse = await coinClient.GetAssetsAsync(request);
        foreach (var coins in coinsListResponse)
        {
            var listItem = new ListViewItem
            {
                Content = $"{coins.Rank} - {coins.Name} - ({coins.Symbol})",
                Tag = coins.Id
            };
            CryptocurrencyListView.Items.Add(listItem);
        }
    }

    private async void ViewDetailsCoin(object sender, SelectionChangedEventArgs e)
    {
        var coinClient = new CoinCapClient(new HttpClient());
        if (CryptocurrencyListView.SelectedItem != null)
        {
            ListViewItem? item = (CryptocurrencyListView.SelectedItem as ListViewItem);
            string coinId = item.Tag.ToString();
            var request = new GetCoinByIdRequest(coinId);
            var coinByIdResponse = await coinClient.GetAssetByIdAsync(request);

            var detailsPage = new DetailsPage
            {
                DataContext = coinByIdResponse.Data
            };

            // Открываем страницу DetailsPage
            Navig.NavigationService.Navigate(detailsPage);
        }
    }

    public async void SearchBoxTextChanged(object sender, TextChangedEventArgs e)
   {
        var coinClient = new CoinCapClient(new HttpClient());
        string searchText = SearchBox.Text.Trim();
        int limit = Convert.ToInt32(LimitComboBox.Text);
        var request = new GetCoinsRequest(searchText, limit, 0);
        var coinsListResponse = await coinClient.GetAssetsAsync(request);

        if (searchText != "")
        {
            CryptocurrencyListView.Items.Clear();
            foreach (var coins in coinsListResponse)
            {
                var listItem = new ListViewItem
                {
                    Content = $"{coins.Rank} - {coins.Name} - ({coins.Symbol})",
                    Tag = coins.Id
                };
                CryptocurrencyListView.Items.Add(listItem);
            }
        }
   }
}