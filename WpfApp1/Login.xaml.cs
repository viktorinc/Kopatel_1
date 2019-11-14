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
        public UserViewModel user = new UserViewModel() { };
        public KladmanViewModel kladman = new KladmanViewModel() { };
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
                        HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost:53306/api/user/add/");
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
                        UserIdFix();

                        MainWindow mw = new MainWindow(user);
                        mw.Show();
                        this.Close();

                    }
                    else

                    {
                        HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost:53306/api/user/loginUser/");
                        request.Method = "Post";
                        request.ContentType = "application/json";
                        using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                        {
                          user = new UserViewModel() { Login = login.Text, Password = Password.Password };
                            string json = JsonConvert.SerializeObject(user);
                            
                            writer.Write(json);
                        }
                        WebResponse respons = request.GetResponse();       
                        UserIdFix();

                        MainWindow mw = new MainWindow(user);
                        mw.Show();
                        this.Close();                                
                        
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ty durachok"+ex.Message);
                }


            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (newbox.IsChecked == true)
                {
                    HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost:53306/api/kladman/add/");
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    using (StreamWriter stream = new StreamWriter(request.GetRequestStream()))
                    {
                        kladman = new KladmanViewModel()
                        {
                            Login = login.Text,
                            Password = Password.Password
                        };
                        string json = JsonConvert.SerializeObject(user);
                        stream.Write(json);


                    }
                    WebResponse response = request.GetResponse();
                    KladmanIdFix();
                    KladmanUI kui = new KladmanUI(kladman);
                    kui.usser = kladman;
                    kui.Show();
                    this.Close();

                }
                else
                {
                    try
                    {
                        HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost:53306/api/kladman/loginKladman/");
                        request.Method = "POST";
                        request.ContentType = "application/json";
                        using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                        {
                            kladman = new KladmanViewModel()
                            {
                                Login = login.Text,
                                Password = Password.Password
                            };
                            string json = JsonConvert.SerializeObject(user);
                            writer.Write(json);
                        }
                        WebResponse response = request.GetResponse();                       
                        KladmanIdFix();
                        KladmanUI kui = new KladmanUI(kladman);
                        kui.usser = kladman;
                        kui.Show();
                        this.Close();
                        
                    }
                    catch
                    {
                        MessageBox.Show("Ty debil");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void KladmanIdFix()
        {
            List<KladmanViewModel> tmps = new List<KladmanViewModel>() { };
            HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost:53306/api/kladman/kladmans/");
            request.Method = "GET";
            request.ContentType = "application/json";
            WebResponse response = request.GetResponse();

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string json = reader.ReadToEnd();
                var kladmans = JsonConvert.DeserializeObject<List<KladmanViewModel>>(json);
                tmps = kladmans;

            }
            foreach (var el in tmps)
            {
                if (el.Login == kladman.Login && el.Password == kladman.Password)
                {
                    kladman.Id = el.Id;
                }
            }
        }

        public void UserIdFix()
        {
            List<UserViewModel> tmps = new List<UserViewModel>() { };
            HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost:53306/api/user/users/");
            request.Method = "GET";
            request.ContentType = "application/json";
            WebResponse response = request.GetResponse();

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string json = reader.ReadToEnd();
                var users = JsonConvert.DeserializeObject<List<UserViewModel>>(json);
                tmps = users;

            }
            foreach (var el in tmps)
            {
                if (el.Login == user.Login && el.Password == user.Password)
                {
                    user.Id = el.Id;
                }
            }
        }
    }
}
