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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyLibrary.ClassHelper;
using MyLibrary.DBModel;

namespace MyLibrary.Pages
{
    /// <summary>
    /// Логика взаимодействия для DeliverListPage.xaml
    /// </summary>
    public partial class DeliverListPage : Page
    {
        List<BookRental> rentBookList = new List<BookRental>();
        List<string> listSort = new List<string>() { "По умолчанию", "По фамилии читателя", "По имени читателя", "По названию книги" };
        public DeliverListPage()
        {
            InitializeComponent();

            cmbSort.ItemsSource = listSort;
            cmbSort.SelectedIndex = 0;
            Filter();
        }
         private void Filter()
        {
            rentBookList = AppDate.Context.BookRental.ToList();
            rentBookList = rentBookList.
                            Where(i => i.Reader.LastName.ToLower().Contains(txtSearch.Text.ToLower()) ||
                            i.Reader.FirstName.ToLower().Contains(txtSearch.Text.ToLower()) ||
                            i.Book.Title.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    rentBookList = rentBookList.OrderBy(i => i.IDReader).ToList();
                    break;
                case 1:
                    rentBookList = rentBookList.OrderBy(i => i.Reader.LastName).ToList();
                    break;
                case 2:
                    rentBookList = rentBookList.OrderBy(i => i.Reader.FirstName).ToList();
                    break;
                case 3:
                    rentBookList = rentBookList.OrderBy(i => i.Book.Title).ToList();
                    break;
                default:
                    rentBookList = rentBookList.OrderBy(i => i.IDReader).ToList();
                    break;
            }
            listDeliver.ItemsSource = rentBookList;
            
        }
        private void UpdateDept()
        {
                var item = listDeliver.SelectedItem as DBModel.BookRental;
                item.Dept = (decimal)RentClass.Debt(item.IDBook,item.StartDate,DateTime.Now.Date);
                AppDate.Context.SaveChanges();
            
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();

        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation visabilityAnim = new DoubleAnimation();
            visabilityAnim.From = 0.0;
            visabilityAnim.To = 1.0;
            visabilityAnim.Duration = TimeSpan.FromSeconds(2);
            MainGrid.BeginAnimation(Grid.OpacityProperty, visabilityAnim);
        }

        private void btnAddDeliver(object sender, RoutedEventArgs e)
        {
            AddDeliverWindow addDeliver = new AddDeliverWindow();
            addDeliver.ShowDialog();
            Filter();
        }

   

      

        private void btnDeliverBook(object sender, RoutedEventArgs e)
        {
            var item = listDeliver.SelectedItem as DBModel.BookRental;
            item.EndDate = DateTime.Now.Date;
            AppDate.Context.SaveChanges();
            MessageBox.Show("Книга сдана");
            UpdateDept();
            Filter();
        }
    }
}
