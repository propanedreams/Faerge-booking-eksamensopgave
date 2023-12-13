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
using System.Net.Http.Json;
using System.Windows.Interop;
using System.DirectoryServices;

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
        private void ResetFields()
        {
            // Clear the values of your textboxes and other input fields
            tboxNameID.Text = "";
            tboxName.Text = "";
            tboxPrisPrBil.Text = "";
            tboxMinAntalBiler.Text = "";
            tboxMaxAntalBiler.Text = "";
            tboxMinAntalGaester.Text = "";
            tboxMaxAntalGaester.Text = "";
            tboxPrisPrGaest.Text = "";

            // Clear other fields in the UI
            datePick.Text = "";
            afrejseIdBooking.Text = "";
            faergeIdBooking.Text = "";
            nummerplade.Text = "";
            laengde.Text = "";
            model.Text = "";
            bookingIdBooking.Text = "";

            // Add more fields if needed
        }

        //FaergeBLL bll = new FaergeBLL();
        Faerge faerge = new Faerge();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ccbFerry.SelectedIndex == -1)
            {
                MessageBox.Show("vælg færge");
            }
            Faerge f = (Faerge)ccbFerry.SelectedItem;
            
            if (f!= null)
            {
                int id = f.id;
                GetAntalGaester(id);
                GetSummasumarum(id);
                GetAntalBiler(id);
            }

        }
        private async void GetAntalGaester(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                
                Task<string> task = client.GetStringAsync($"https://localhost:7177/api/Faerge/getantalgaesterforfaerge?id={id}");
                var msg = task.Result;

                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                int x = JsonSerializer.Deserialize<int>(msg, option);
                antalGaesterOverview.Text = x.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void GetAntalBiler(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                Task<string> task = client.GetStringAsync($"https://localhost:7177/api/Faerge/getantalbilerforfaerge?id={id}");
                var msg = task.Result;

                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                int x = JsonSerializer.Deserialize<int>(msg, option);
                antalBilerOverview.Text = x.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void GetSummasumarum(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                Task<string> task = client.GetStringAsync($"https://localhost:7177/api/Faerge/getCalculateTotalSumForFaerge?id={id}");
                var msg = task.Result;

                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                int x = JsonSerializer.Deserialize<int>(msg, option);
                omsætningOverview.Text = x.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //loader vinduets ccbboxe med data
        private void Window_LoadedOverview()
        {
            //WindowBil();
            //WindowBooking();
            //Window();
            HttpClient client = new HttpClient();
            Task<string> task = client.GetStringAsync("https://localhost:7177/api/Faerge/getallfaerges");
            var msg = task.Result;

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            ccbFerry.Items.Clear();
            ccbFerryInfo.Items.Clear();
            ccboxFaergeBooking.Items.Clear();
            List<Faerge> faerge = JsonSerializer.Deserialize<List<Faerge>>(msg, option);
            listboxFaerger.ItemsSource = faerge;
            
            foreach (Faerge fa in faerge)
            {
                ccbFerry.Items.Add(fa);
                ccbFerryInfo.Items.Add(fa);
                ccboxFaergeBooking.Items.Add(fa);
                
            }
        }
        private void WindowBil()
        {
            HttpClient client = new HttpClient();
            Task<string> task = client.GetStringAsync($"https://localhost:7177/api/Booking/getbilerbybookingid?id={bookingIdBooking.Text}");
            var msg = task.Result;

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Bil> biler = JsonSerializer.Deserialize<List<Bil>>(msg, option);
            ccboxBil.Items.Clear();
            foreach (Bil b in biler)
            {

                ccboxBil.Items.Add(b);
            }
        }
        private void WindowBooking()
        {
            HttpClient client = new HttpClient();
            Task<string> task = client.GetStringAsync($"https://localhost:7177/api/Afrejse/getbookingbyafrejseid?id={afrejseIdBooking.Text}");
            var msg = task.Result;

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Booking> bookings = JsonSerializer.Deserialize<List<Booking>>(msg, option);
            ccboxBooking.Items.Clear();
            foreach (Booking b in bookings)
            {

                ccboxBooking.Items.Add(b);
            }
        }
        private void sletId()
        {
            omsætningOverview.Text = "";
            antalBilerOverview.Text = "";
            antalGaesterOverview.Text = "";
            faergeIdOverview.Text = "";
            afrejseIdOverview.Text = "";
            bookingIdOverview.Text = "";
            bilIdOverview.Text = "";
            gaestIdOverview.Text = "";
        }
        private void clearLbs()
        {
            lbBooking.ItemsSource = null;
            lbBil.ItemsSource = null;
            lbGaest.ItemsSource = null;
        }
        private void ccbFerry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sletId();
            clearLbs();
            lbAfrejse.ItemsSource = null;
            //vis afrejse for valgte færge
            Faerge fa = (Faerge)ccbFerry.SelectedItem;
            if (fa != null)
            {
                faergeIdOverview.Text = fa.id.ToString();
                OpdaterLbFaerge();
            }
        }
        private void lbAfrejse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbBil.ItemsSource = null;
            lbGaest.ItemsSource = null;
            Afrejse af = (Afrejse)lbAfrejse.SelectedItem;
            if (af != null)
            {
                afrejseIdOverview.Text = af.afrejseId.ToString();
                OpdaterLbAfrejse();
            }
        }
        private void lbBooking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbGaest.ItemsSource = null;
            Booking b = (Booking)lbBooking.SelectedItem;
            if (b != null)
            {
                bookingIdOverview.Text = b.id.ToString();
                OpdaterLbBooking();


            }
        }

        private void lbBil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bil b = (Bil)lbBil.SelectedItem;
            if (b != null)
            {
                bilIdOverview.Text = b.nummerplade.ToString();
                OpdaterLbBil();
            }
        }

        private void lbGaest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Gaest g = (Gaest)lbGaest.SelectedItem;
            if (g != null)
            {
                gaestIdOverview.Text = g.gaestId.ToString();
                OpdaterLbBil();
            }
        }

        private void Window()
        {
            HttpClient client = new HttpClient();
            Task<string> task = client.GetStringAsync($"https://localhost:7177/api/Faerge/getallafrejse?id={faergeIdBooking.Text}");
            var msg = task.Result;

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Afrejse> afrejse = JsonSerializer.Deserialize<List<Afrejse>>(msg, option);
            ccboxAfrejseBooking.Items.Clear();
            foreach (Afrejse fa in afrejse)
            {

                ccboxAfrejseBooking.Items.Add(fa);
            }
        }

        private async void OpdaterLbBil()
        {
            try
            {
                HttpClient client = new HttpClient();
                string id = bilIdOverview.Text;
                Task<string> task = client.GetStringAsync($"https://localhost:7177/api/Bil/getgaesterOnBilById?id={id}");
                var msg = task.Result;

                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<Gaest> x = JsonSerializer.Deserialize<List<Gaest>>(msg, option);
                lbGaest.ItemsSource = x;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void OpdaterLbBooking()
        {
            try
            {
                HttpClient client = new HttpClient();
                string id = bookingIdOverview.Text;
                Task<string> task = client.GetStringAsync($"https://localhost:7177/api/Booking/getbilerbybookingid?id={id}");
                var msg = task.Result;

                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<Bil> afrejse = JsonSerializer.Deserialize<List<Bil>>(msg, option);
                lbBil.ItemsSource = afrejse;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void OpdaterLbFaerge()
        {
            try
            {
                HttpClient client = new HttpClient();
                string id = faergeIdOverview.Text;
                    Task<string> task = client.GetStringAsync($"https://localhost:7177/api/Faerge/getallafrejse?id={id}");
                    var msg = task.Result;

                    var option = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Afrejse> afrejse = JsonSerializer.Deserialize<List<Afrejse>>(msg, option);
                    lbAfrejse.ItemsSource = afrejse; 
                    
                
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        private async void OpdaterLbAfrejse()
        {
            try
            {
                HttpClient client = new HttpClient();
                string id = afrejseIdOverview.Text;
                Task<string> task = client.GetStringAsync($"https://localhost:7177/api/Afrejse/getbookingbyafrejseid?id={id}");
                var msg = task.Result;

                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<Booking> bookings = JsonSerializer.Deserialize<List<Booking>>(msg, option);
                lbBooking.ItemsSource = bookings;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                    Window_LoadedOverview();
                    ResetFields();
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
                    Window_LoadedOverview();
                    ResetFields();
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
                //string jsonData = JsonSerializer.Serialize(faerge);
                //StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7177/api/Faerge/deletefaerge/{int.Parse(tboxNameID.Text)}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Færge er blevet slettet");
                    Window_LoadedOverview();
                    ResetFields();
                }
                else
                {
                    MessageBox.Show($"Fejl: {response.StatusCode.ToString()}");
                }
            }
        }


        //--------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------
        private async void Button_Click_5(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                //string jsonData = JsonSerializer.Serialize(faerge);
                //StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7177/api/Afrejse/deleteAfrejse/{int.Parse(afrejseIdOverview.Text)}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("afrejse er blevet slettet");
                    
                    OpdaterLbFaerge();
                    sletId();
                    clearLbs();
                    Window_LoadedOverview();
                }
                else
                {
                    MessageBox.Show($"Fejl: {response.StatusCode.ToString()}");
                }
            }
        }
        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                //string jsonData = JsonSerializer.Serialize(faerge);
                //StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7177/api/Booking/deleteBooking/{int.Parse(bookingIdOverview.Text)}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("booking er blevet slettet");

                    OpdaterLbAfrejse();
                    sletId();
                    lbBil.ItemsSource = null;
                    lbGaest.ItemsSource = null;
                    Window_LoadedOverview();

                }
                else
                {
                    MessageBox.Show($"Fejl: {response.StatusCode.ToString()}");
                }
            }
        }

        private async void Button_Click_6(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                //string jsonData = JsonSerializer.Serialize(faerge);
                //StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7177/api/Bil/deleteBil/{bilIdOverview.Text}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("bil er blevet slettet");
                    OpdaterLbBooking();
                    sletId();
                    
                    lbGaest.ItemsSource = null;
                    Window_LoadedOverview();

                }
                else
                {
                    MessageBox.Show($"Fejl: {response.StatusCode.ToString()}");
                }
            }
        }
        private async void Button_Click_7(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                //string jsonData = JsonSerializer.Serialize(faerge);
                //StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7177/api/Gaest/deleteGaest/{int.Parse(gaestIdOverview.Text)}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Gaest er blevet slettet");
                    OpdaterLbBil();
                    sletId();
                    Window_LoadedOverview();



                }
                else
                {
                    MessageBox.Show($"Fejl: {response.StatusCode.ToString()}");
                }
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (tboxNameID.Text == null)
            {
                MessageBox.Show("Fejl: Vælg en faerge i dropdown");

            }
            if (datePick.Text == "")
            {
                MessageBox.Show("Fejl: Vælg Afrejsedato");

            }
            using (HttpClient client = new HttpClient())
            {
                String url = $"https://localhost:7177/api/Afrejse/opretAfrejse?dato={datePick}&faergeId={tboxNameID.Text}";
                HttpResponseMessage response = await client.PostAsync(url, null);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Afrejse er blevet oprettet");
                    ResetFields();
                    Window_LoadedOverview();
                }
                else
                {
                    MessageBox.Show($"Fejl: {response.StatusCode.ToString()}");
                }

            }
        }

        private void ccboxFaergeBooking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Faerge fa = (Faerge)ccboxFaergeBooking.SelectedItem;
            if (fa != null)
            {
                ResetFields();
                ClearFieldsNoReload();
                faergeIdBooking.Text = fa.id.ToString();
                Window();

            }
        }

        private void ccboxAfrejseBooking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Afrejse fa = (Afrejse)ccboxAfrejseBooking.SelectedItem;
            if (fa != null)
            {
                afrejseIdBooking.Text = fa.afrejseId.ToString();
                WindowBooking();
            }
        }
        private async void OpretBooking()
        {
            using (HttpClient client = new HttpClient())
            {
                String url = $"https://localhost:7177/api/Booking/opretBooking?dato={DateTime.Now}&afrejseId={afrejseIdBooking.Text}";
           
                HttpResponseMessage response = await client.PostAsync(url, null);
                if (response.IsSuccessStatusCode)
                {
                    Booking createdBooking = await response.Content.ReadFromJsonAsync<Booking>();

                    int createdBookingId = createdBooking.id;

                    MessageBox.Show($"Booking med ID {createdBookingId} er blevet oprettet");
                    ClearFields();
                }
                else
                {
                    MessageBox.Show($"Fejl: {response.StatusCode.ToString()}");
                }

            }
        }
        private void OpretBil()
        {

        }
        private void OpretPerson()
        {

        }
        private void Button_Click_OpretBooking(object sender, RoutedEventArgs e)
        {
            OpretBooking();
            Window_LoadedOverview();
        }

        private void ccboxBooking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Booking x = (Booking)ccboxBooking.SelectedItem;
            if (x != null)
            {
                bookingIdBooking.Text = x.id.ToString();
                WindowBil();
            }
        }
       
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                String url = $"https://localhost:7177/api/Bil/opretBil?nummerplade={nummerplade.Text}&laengde={laengde.Text}&model={model.Text}&bookingId={bookingIdBooking.Text}";

                HttpResponseMessage response = await client.PostAsync(url, null);
                if (response.IsSuccessStatusCode)
                {
                   

                    MessageBox.Show($"bil er nu  blevet tilføjet booking");
                    ClearFields();
                    Window_LoadedOverview();
                }
                else
                {
                    MessageBox.Show($"Fejl: {response.StatusCode.ToString()}");
                }

            }
        }

        private void ccboxBil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bil x = (Bil)ccboxBil.SelectedItem;
            if (x != null)
            {
                bilIdBooking.Text = x.nummerplade;
            }
        }
        private void ClearFields()
        {
            // Clear TextBoxes
            faergeIdBooking.Text = "";
            afrejseIdBooking.Text = "";
            bookingIdBooking.Text = "";
            bilIdBooking.Text = "";
            nummerplade.Text = "";
            model.Text = "";
            laengde.Text = "";
            gaesteNavn.Text = "";

            // Clear CheckBox
            cboxKøn.IsChecked = false;

            // Reset ComboBoxes
          
            ccboxAfrejseBooking.SelectedIndex = -1;
            ccboxBooking.SelectedIndex = -1;
            ccboxBil.SelectedIndex = -1;
            
            Window_LoadedOverview();
        }
        private void ClearFieldsNoReload()
        {
            // Clear TextBoxes
            faergeIdBooking.Text = "";
            afrejseIdBooking.Text = "";
            bookingIdBooking.Text = "";
            bilIdBooking.Text = "";
            nummerplade.Text = "";
            model.Text = "";
            laengde.Text = "";
            gaesteNavn.Text = "";

            // Clear CheckBox
            cboxKøn.IsChecked = false;

            // Reset ComboBoxes

            ccboxAfrejseBooking.SelectedIndex = -1;
            ccboxBooking.SelectedIndex = -1;
            ccboxBil.SelectedIndex = -1;

            
        }
        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                String url = $"https://localhost:7177/api/Gaest/opretGaest?navn={gaesteNavn.Text}&koen={cboxKøn.IsChecked}&bilId={bilIdBooking.Text}";
           
                HttpResponseMessage response = await client.PostAsync(url, null);
                if (response.IsSuccessStatusCode)
                {


                    MessageBox.Show($"Gaest er nu tilføjet en booking");
                    ClearFields();
                    Window_LoadedOverview();
                }
                else
                {
                    MessageBox.Show($"Fejl: {response.StatusCode.ToString()}");
                }

            }
        }

        
    }
}
