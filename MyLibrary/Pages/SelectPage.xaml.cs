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
using MyLibrary.Pages;

namespace MyLibrary.Pages
{
    /// <summary>
    /// Логика взаимодействия для SelectPage.xaml
    /// </summary>
    public partial class SelectPage : Page
    {
        public SelectPage()
        {
            InitializeComponent();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation visabilityAnim = new DoubleAnimation();
            visabilityAnim.From = 0.0;
            visabilityAnim.To = 1.0;
            visabilityAnim.Duration = TimeSpan.FromSeconds(1);
            MainGrid.BeginAnimation(Grid.OpacityProperty, visabilityAnim);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SubFrame.Content = new BookListPage();
        }
        private void ListReader(object sender, RoutedEventArgs e)
        {
            SubFrame.Content = new ReaderListPage();
        }


        private void ListDeliver(object sender, RoutedEventArgs e)
        {
            SubFrame.Content = new DeliverListPage();
        }
    }
}
