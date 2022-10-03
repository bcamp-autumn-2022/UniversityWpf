using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Diagnostics;

namespace UniversityWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            textUsername.Focus();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username=textUsername.Text;
            string password=textPassword.Text;
            var data = Task.Run(() => LoginToUni(username, password));
            data.Wait();

            if (data.Result.Length > 0)
            {
                string response = data.Result;
                Debug.WriteLine(response);
                if (string.Compare(response, "true") == 0)
                {
                    Console.WriteLine("Login OK");
                    MessageBox.Show("Login OK");
                }
                else
                {
                    MessageBox.Show("Wrong username/password");
                }
            }
            else
            {
                MessageBox.Show("Something went wrong");
                textUsername.Clear();
                textPassword.Clear();
            }
        }
        static async Task<string> LoginToUni(string u, string p)
        {
            var userName = "admin";
            var passwd = "1234";
            var authData = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
            string base_url = MyEnvironment.GetBaseUrl();
            var response = string.Empty;
            var url = base_url + "/login";
            User objectUser = new User(u, p);

            var json = JsonConvert.SerializeObject(objectUser);
            var postData = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authData));
            HttpResponseMessage result = await client.PostAsync(url, postData);
            response = await result.Content.ReadAsStringAsync();
            return response;
        }
    }
}
