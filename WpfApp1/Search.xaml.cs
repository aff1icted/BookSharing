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
    /// Логика взаимодействия для Search.xaml
    /// </summary>
    public partial class Search : Page
    {
        public Search(string user)
        {
            InitializeComponent();
            UserLable.Content = user;
            SQLGetList SQLGetList = new SQLGetList();

            AuthorBox.ItemsSource = SQLGetList.GetAuthorList();
            GenreBox.ItemsSource = SQLGetList.GetGenreList();  

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ISBNBox.Text) || !string.IsNullOrEmpty(TitleBox.Text) || !string.IsNullOrEmpty(GenreBox.Text) || !string.IsNullOrEmpty(AuthorBox.Text))
            {
                SQLGetList SQLGetList = new SQLGetList();
                NavigationService.Navigate(new BookList(SQLGetList.SearchBook(ISBNBox.Text, TitleBox.Text, GenreBox.Text, AuthorBox.Text)));
            }
            else
            {
                ErrorLable.Visibility = Visibility.Visible;
            }

        }
    }
}
