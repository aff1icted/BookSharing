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
using System.Collections.ObjectModel;

namespace BookSharing
{
    /// <summary>
    /// Логика взаимодействия для ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        public ObservableCollection<Book> Books;
        public ReportPage(string user)
        {
            InitializeComponent();
            SQLContol SQLContol = new SQLContol();
            UserLable.Content = user;

            BooksList.ItemsSource = SQLContol.GetReportList();
        }

        private void BooksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book p = (Book)BooksList.SelectedItem;
            NavigationService.Navigate(new BookPage(p.id, Convert.ToString(UserLable.Content)));
        }
    }
}
