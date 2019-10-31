using Kopatel.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string log = login.Text;
            string pass = Password.Password;

            if (Password.Password == "" || login.Text == "")
            {
                MessageBox.Show("Please fill in the empty fields!");
            }
            else
            {
                try
                {
                    if (newbox.IsChecked == true)
                    {
                        UserViewModel user;
                        HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost:49576/api/user/add/");
                        request.Method = "POST";
                        request.ContentType = "application/json";
                        using (StreamWriter stream = new StreamWriter(request.GetRequestStream()))
                        {
                            user = new UserViewModel()
                            {
                                Login = login.Text,
                                Password = Password.Password
                            };
                            string json = JsonConvert.SerializeObject(user);
                            stream.Write(json);

                            
                        }
                        WebResponse response = request.GetResponse();
                        MainWindow mw = new MainWindow();
                        mw.user = user;
                        mw.Show();
                        this.Close();

                    }
                    else
                    {
                        HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost:49576/api/user/users/");
                        request.Method = "GET";
                        request.ContentType = "application/json";
                        using (WebResponse response = request.GetResponse())
                        {
                            using (StreamReader reader = new StreamReader(request.GetRequestStream()))
                            {
                                string json = reader.ReadToEnd();
                                List<UserModel> users = JsonConvert.DeserializeObject<List<UserModel>>(json);
                                foreach (var el in users)
                                {
                                    if (el.Login == login.Text)
                                    {
                                        if (el.Password == Password.Password)
                                        {
                                            MainWindow mw = new MainWindow();
                                            mw.usser = el;
                                            mw.Show();
                                            this.Close();
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
