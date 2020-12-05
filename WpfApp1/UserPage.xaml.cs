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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public UserPage(string user)
        {
            InitializeComponent();
            UserNameLable.Content = user;
            PageFrame.Navigate(new Uri("/BookList.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(new Search(Convert.ToString(UserNameLable.Content)));
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {            
            PageFrame.Navigate(new MyBookList(Convert.ToString(UserNameLable.Content)));
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PageFrame.Navigate(new Uri("/BookList.xaml", UriKind.Relative));
        }
    }
}
