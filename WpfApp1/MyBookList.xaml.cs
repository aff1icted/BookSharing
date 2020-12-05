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
    /// Логика взаимодействия для MyBookList.xaml
    /// </summary>
    /// 

    

    public partial class MyBookList : Page
    {
        public ObservableCollection<Book> Books;
        public MyBookList(string user)
        {
            InitializeComponent();
            UserLable.Content = user;


            SQLContol SQLContol = new SQLContol();


            BooksList.ItemsSource = SQLContol.GetUserBookList(user);

        }

        private void BooksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book p = (Book)BooksList.SelectedItem;
            NavigationService.Navigate(new BookPage(p.id, Convert.ToString(UserLable.Content)));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            NavigationService.Navigate(new BookCreate(Convert.ToString(UserLable.Content)));
        }
    }
}
