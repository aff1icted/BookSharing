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
            SQLContol SQLContol = new SQLContol();

            AuthorBox.ItemsSource = SQLContol.GetAuthorList();
            GenreBox.ItemsSource = SQLContol.GetGenreList();  

        }
    }
}
