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

namespace BookSharing
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SQLContol SQLControl = new SQLContol();
            if (!string.IsNullOrEmpty(LoginTextBox.Text)&& !string.IsNullOrEmpty(PasswordTextBox.Text) && !string.IsNullOrEmpty(ConfirmationTextBox.Text) && !string.IsNullOrEmpty(EmailTextBox.Text))
            {            
                if (SQLControl.UniqLogin(LoginTextBox.Text))
                {
                    if (PasswordTextBox.Text==ConfirmationTextBox.Text)
                    {
                        if (SQLControl.UniqEmail(EmailTextBox.Text))
                        {
                            SQLControl.Registration(LoginTextBox.Text, PasswordTextBox.Text, EmailTextBox.Text);
                            NavigationService.Navigate(new Uri("/EntryPage.xaml", UriKind.Relative));
                        }
                        else
                        {
                            ErrorLable.Content = "Этот Email уже используется";
                        }
                    }
                    else
                    {
                        ErrorLable.Content = "Пароли не совпадают";
                    }
                }
                else
                {
                    ErrorLable.Content = "Этот логин уже используется";
                }
            }
            else
            {
                ErrorLable.Content = "Заполните пустые поля";
            }
        }
    }
}
