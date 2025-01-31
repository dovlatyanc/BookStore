using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext context;
        public MainWindow()
        {
            InitializeComponent();
            context = new ApplicationContext();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            string username = Login.Text;

            string password = Password.Text;


            if (rememberMe.IsChecked == true)
            {
                UserCredentials credentials = new UserCredentials
                {
                    Username = Login.Text,
                    Password = Password.Text
                };

                SaveUserCredentials(credentials);
            }

            if (context.CheckCredentials(username, password))
            {

                Window window = new Window1();
                Close();
                window.Show();
            }

            else
            {
                MessageBox.Show("Пользователь не найден. Попробуйте снова.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }



        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserCredentials? credentials = LoadUserCredentials();
            if (credentials != null)
            {
                Login.Text = credentials.Username;
                Password.Text = credentials.Password;
                rememberMe.IsChecked = true;
            }
        }
        public void SaveUserCredentials(UserCredentials credentials)
        {
            string json = JsonConvert.SerializeObject(credentials);
            File.WriteAllText("userCredentials.json", json);
        }
        public UserCredentials? LoadUserCredentials()
        {
            if (File.Exists("userCredentials.json"))
            {
                string json = File.ReadAllText("userCredentials.json");
                return JsonConvert.DeserializeObject<UserCredentials>(json);
            }
            return null;
        }





    }
    public class UserCredentials
    {
        public string? Username { get; set; }
        public string? Password { get; set; }


    }
}