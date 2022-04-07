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
    /// Логика взаимодействия для ReaderListPage.xaml
    /// </summary>
    public partial class ReaderListPage : Page
    {
        List<Reader> readerList = new List<Reader>();
        List<string> listSort = new List<string>() { "По умолчанию", "По фамилии", "По имени", "По адресу" };
        public ReaderListPage()
        {
            InitializeComponent();
            cmbSort.ItemsSource = listSort;
            cmbSort.SelectedIndex = 0;

            Filter();
        }
        private void Filter()
        {
            readerList = AppDate.Context.Reader.ToList();
            readerList = readerList.
                            Where(i => i.LastName.ToLower().Contains(txtSearch.Text.ToLower()) ||
                            i.FirstName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();

            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    readerList = readerList.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    readerList = readerList.OrderBy(i => i.LastName).ToList();
                    break;
                case 2:
                    readerList = readerList.OrderBy(i => i.FirstName).ToList();
                    break;
                case 3:
                    readerList = readerList.OrderBy(i => i.Address).ToList();
                    break;
                default:
                    readerList = readerList.OrderBy(i => i.ID).ToList();
                    break;
            }

            listReader.ItemsSource = readerList;
        }

        private void btnAddReader_Click(object sender, RoutedEventArgs e)
        {
            AddReaderWindow addReaderWindow = new AddReaderWindow();
            addReaderWindow.ShowDialog();
            Filter();
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

        private void Click_Remove(object sender, RoutedEventArgs e)
        {
            if (listReader.SelectedItem is DBModel.Reader)
            {
                try
                {
                    var item = listReader.SelectedItem as DBModel.Reader;
                    var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultClick == MessageBoxResult.Yes)
                    {
                        AppDate.Context.Reader.Remove(item);
                        AppDate.Context.SaveChanges();
                        MessageBox.Show("Пользователь успешно удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        Filter();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Click_Change(object sender, RoutedEventArgs e)
        {
            var item = listReader.SelectedItem as DBModel.Reader;
            ChangeReaderWindow changereaderwindow = new ChangeReaderWindow(item);
            changereaderwindow.ShowDialog();
            Filter();
        }
    }
}
