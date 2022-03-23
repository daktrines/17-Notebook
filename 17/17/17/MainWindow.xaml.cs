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
using _17;

namespace _17
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bloknot bloknot;
        public MainWindow()
        {
            InitializeComponent();
            bloknot = new Bloknot(fieldEdit);
        }

        
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        //Создать
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            bloknot.Create();
            this.Title = bloknot.NameFile;//очищаем заголовок
        }

        private void fieldEdit_TextChanged(object sender, TextChangedEventArgs e)
        {
            bloknot.Modified = true;
        }


        private void Information_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Калион Екатерина ");
                //"jk"
              // "Информация", MessageBoxButton.OK, MessageBoxImage.Question) ;
        }
    }
}
