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
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace UniversityWpf
{
    /// <summary>
    /// Interaction logic for StudentMenu.xaml
    /// </summary>
    public partial class StudentMenu : Window
    {
        public StudentMenu()
        {
            InitializeComponent();
        }

        private void btnData_Click(object sender, RoutedEventArgs e)
        {
                var data = Task.Run(() => GetStudentData());
                data.Wait();
                if (data.Result.Length > 0)
                {
                    JObject j = JObject.Parse(data.Result);
                    textFn.Text = "firstname : "+j["firstname"].ToString();
                    textLn.Text = "lastname : " + j["lastname"].ToString();
                    textUn.Text = "username : " + j["username"].ToString();
                    textStart.Text = "start_date : " + j["start_date"].ToString();
                    textGraduate.Text = "graduate date : " + j["graduate_date"].ToString();
                }
                else
                {
                    MessageBox.Show("You don't have a data");
                }
            }
        

        static async Task<string> GetStudentData()
        {
            Singleton si = Singleton.Instance;
            string userName = si.Username;
            var passwd = si.Password;
            var authData = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
            var response = string.Empty;
            var url = MyEnvironment.GetBaseUrl() + "/studentdata/student/" + userName;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authData));
            HttpResponseMessage result = await client.GetAsync(url);
            response = await result.Content.ReadAsStringAsync();
            //Console.WriteLine(response);
            return response;
        }
    }
}
