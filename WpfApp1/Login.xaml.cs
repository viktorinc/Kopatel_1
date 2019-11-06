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
                        UserViewModel user = new UserViewModel();
                        HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost:49576/api/user/loginUser/");
                        request.Method = "Post";
                        request.ContentType = "application/json";
                        using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                        {
                          user = new UserViewModel() { Login = login.Text, Password = Password.Password };
                            string json = JsonConvert.SerializeObject(user);
                            
                            writer.Write(json);
                        }
                        WebResponse respons = request.GetResponse();
                        MainWindow mw = new MainWindow();
                        mw.user = user;
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
                    KladmanViewModel user;
                    HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost:49576/api/kladman/add/");
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    using (StreamWriter stream = new StreamWriter(request.GetRequestStream()))
                    {
                        user = new KladmanViewModel()
                        {
                            Login = login.Text,
                            Password = Password.Password
                        };
                        string json = JsonConvert.SerializeObject(user);
                        stream.Write(json);


                    }
                    WebResponse response = request.GetResponse();
                    KladmanUI kui = new KladmanUI();
                    kui.usser = user;
                    kui.Show();
                    this.Close();

                }
                else
                {
                    try
                    {
                        HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost:49576/api/user/loginKladman/");
                        request.Method = "POST";
                        KladmanViewModel user = new KladmanViewModel();
                        request.ContentType = "application/json";
                        using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                        {
                           user = new KladmanViewModel() { Login = login.Text, Password = Password.Password };
                            string json = JsonConvert.SerializeObject(user);
                            writer.Write(json);
                        }
                        WebResponse response = request.GetResponse();
                            KladmanUI kui = new KladmanUI();
                            kui.usser = user;
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
    }
}
