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
   
    public partial class EntryPage : Page
    {
                
        public EntryPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SQLlogin SQLlogin = new SQLlogin(); 
            
            if (SQLlogin.LoginPasswordCheck(LoginTextBox.Text, PasswordTextBox.Text))
            {
                if (SQLlogin.RightsCheck(LoginTextBox.Text)=="admin")
                {
                    NavigationService.Navigate(new AdminPage(LoginTextBox.Text));
                }
                else
                {
                    NavigationService.Navigate(new UserPage(LoginTextBox.Text));
                }

            }
            else
            {
                errorlable.Visibility = Visibility.Visible;
            }
            
        }
    }
}
