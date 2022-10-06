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
using Newtonsoft.Json;

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

        static async Task<string> GetStudentGrade()
        {
            Singleton si = Singleton.Instance;
            string userName = si.Username;
            var passwd = si.Password;
            var authData = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
            var response = string.Empty;
            var url = MyEnvironment.GetBaseUrl() + "/studentdata/grade/" + userName;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authData));
            HttpResponseMessage result = await client.GetAsync(url);
            response = await result.Content.ReadAsStringAsync();
            //Console.WriteLine(response);
            return response;
        }

        private void btnGrade_Click(object sender, RoutedEventArgs e)
        {
            var data = Task.Run(() => GetStudentGrade());
            data.Wait();
            Console.WriteLine(data.Result);
            if (data.Result.Length > 3) //Result is not []
            {
                dynamic student_data = JsonConvert.DeserializeObject(data.Result);

                gridGrades.ItemsSource = student_data;//writes the data to DataGrid

                string student_grades = "";
                foreach (var grade in student_data)
                {
                    student_grades += grade.coursename + " | " + grade.cderit + " | " +  "\n";
                }
                //txtBooks.Text = book_data;*/
                Console.WriteLine(student_grades);

            }
            else
            {
                MessageBox.Show("There is no books");
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            btnData_Click(this,null);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            btnGrade_Click(this, null);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            btnGrade_Click(this, null);
        }
    }
}
