using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Data.SqlClient;



namespace BookSharing
{
    class Entry
    {
        
        
        public static bool LoginPasswordCheck(string login, string password) 
        {
            return true;
            //return false;
        }
        
        public static bool Admincheck(string login)
        {
            //return true;
            return false;
        }
    }

    public partial class EntryPage : Page
    {
                
        public EntryPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SQLContol SQLControl = new SQLContol(); 
            if (SQLControl.LoginPasswordCheck(LoginTextBox.Text, PasswordTextBox.Text))
            {
                if (SQLControl.RightsCheck(LoginTextBox.Text)=="admin")
                {
                    NavigationService.Navigate(new Uri("/AdminPage.xaml", UriKind.Relative));
                }
                else
                {
                    NavigationService.Navigate(new Uri("/UserPage.xaml", UriKind.Relative));
                }

            }
            else
            {
                errorlable.Visibility = Visibility.Visible;
            }
            
        }
    }
}
