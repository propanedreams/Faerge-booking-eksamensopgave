using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using BusinessLogic.FaergeBLL;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Text.Json;

namespace FærgeEksamensOpgave
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Window_LoadedOverview();


        }

        FaergeBLL bll = new FaergeBLL();
        Faerge faerge = new Faerge();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //opret en utility generic metode til dette
            HttpClient client = new HttpClient();
            //skift -3 ud med input fra et wpf felt
            Task<string> task = client.GetStringAsync("https://localhost:7177/api/Faerge/getfaergebyid?id=" + "-3");
            var msg = task.Result;
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Faerge faerge = JsonSerializer.Deserialize<Faerge>(msg, option);
            textBox.Text = faerge.navn;
        }

        //loader vinduets ccbboxe med data
        private void Window_LoadedOverview()
        {
            HttpClient client = new HttpClient();
            Task<string> task = client.GetStringAsync("https://localhost:7177/api/Faerge/getallfaerges");
            var msg = task.Result;

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Faerge> faerge = JsonSerializer.Deserialize<List<Faerge>>(msg, option);
            listboxFaerger.ItemsSource = faerge;
            lbFaerge.ItemsSource = faerge;
            foreach (Faerge fa in faerge)
            {
                ccbFerry.Items.Add(fa);
                ccbFerryInfo.Items.Add(fa);
            }
        }

        private void ccbFerry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Faerge fa = (Faerge)ccbFerry.SelectedItem;
            textBox.Text = fa.prisPrGaest.ToString();
        }
        //sætter det valgte objekts data til de forskellige felter
        private void ccbFerryInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Faerge fa = (Faerge)ccbFerryInfo.SelectedItem;
            if (fa != null)
            {
                tboxNameID.Text = fa.id.ToString();
                tboxName.Text = fa.navn;
                tboxPrisPrBil.Text = fa.prisPrBil.ToString();
                tboxMinAntalBiler.Text = fa.minAntalBiler.ToString();
                tboxMaxAntalBiler.Text = fa.maxAntalBiler.ToString();
                tboxMinAntalGaester.Text = fa.minAntalGaester.ToString();
                tboxMaxAntalGaester.Text = fa.maxAntalGaester.ToString();
                tboxPrisPrGaest.Text = fa.prisPrGaest.ToString();
            }
        }

        //--------------------------------------------------------------------------------------------------
        //CRUD for færge
        //--------------------------------------------------------------------------------------------------
        //opretter færge
        private async void Button_Click_OpretFaerge(object sender, RoutedEventArgs e)
        {
            DTO.Model.Faerge faerge = new DTO.Model.Faerge
            {
                navn = tboxName.Text,
                minAntalGaester = int.Parse(tboxMinAntalGaester.Text),
                maxAntalGaester = int.Parse(tboxMaxAntalGaester.Text),
                prisPrGaest = int.Parse(tboxPrisPrGaest.Text),
                maxAntalBiler = int.Parse(tboxMaxAntalBiler.Text),
                minAntalBiler = int.Parse(tboxMinAntalBiler.Text),
                prisPrBil = int.Parse(tboxPrisPrBil.Text),

            };

            using (HttpClient client = new HttpClient())
            {
                string jsonData = JsonSerializer.Serialize(faerge);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("https://localhost:7177/api/Faerge/opretfaerge", content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Ny Færge blev oprettet");
                }
                else
                {
                    MessageBox.Show($"Fejl: {response.StatusCode.ToString()}");
                }
            }
        }

        //opdaterer færge
        private async void Button_Click_opdaterFaerge(object sender, RoutedEventArgs e)
        {
            DTO.Model.Faerge faerge = new DTO.Model.Faerge
            {
                id = int.Parse(tboxNameID.Text), //usynligt felt
                navn = tboxName.Text,
                minAntalGaester = int.Parse(tboxMinAntalGaester.Text),
                maxAntalGaester = int.Parse(tboxMaxAntalGaester.Text),
                prisPrGaest = int.Parse(tboxPrisPrGaest.Text),
                maxAntalBiler = int.Parse(tboxMaxAntalBiler.Text),
                minAntalBiler = int.Parse(tboxMinAntalBiler.Text),
                prisPrBil = int.Parse(tboxPrisPrBil.Text),
            };
            using (HttpClient client = new HttpClient())
            {
                string jsonData = JsonSerializer.Serialize(faerge);              
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"https://localhost:7177/api/Faerge/updatefaerge/{faerge.id}", content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Færge er blevet opdateret");
                }
                else
                {
                    MessageBox.Show($"Fejl: {response.StatusCode.ToString()}");
                }
            }
        }

        //sletter færge
        private async void Button_Click_DeleteFaerge(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonData = JsonSerializer.Serialize(faerge);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7177/api/Faerge/deletefaerge/{int.Parse(tboxNameID.Text)}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Færge er blevet slettet");
                }
                else
                {
                    MessageBox.Show($"Fejl: {response.StatusCode.ToString()}");
                }
            }
        }


        //--------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {

            using (HttpClient client = new HttpClient())
            {
                String url = $"https://localhost:7177/api/Afrejse/opretAfrejse?dato={datePick}&faergeId={tboxNameID.Text}";
                await client.PostAsync(url, null);

            }
        }

    }
}
