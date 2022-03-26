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
            fieldEdit.Focus();
        }

        //Кнопка "Выход"
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Кнопка "Сохранить"
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (bloknot.ASaveBloknot() == false) return;
            this.Title = bloknot.NameFile;//очищаем заголовок
        }

        //Кнопка "Создать"
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            bloknot.Create();
            this.Title = bloknot.NameFile;//очищаем заголовок
        }

        private void fieldEdit_TextChanged(object sender, TextChangedEventArgs e)
        {
            bloknot.Modified = true;
        }

        //Окно информации
        private void Information_Click(object sender, RoutedEventArgs e)
        {
            //Открываем окно информации
            Information info = new Information();
            info.Owner = this; //Получение ссылки на родителя
            info.ShowDialog();//Открываем диалоговое окномне нужна ьа

        }

        //Закрытие программы
        private void Windows_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (bloknot.Modified == true)
            {
                MessageBoxResult rezult;
                rezult = MessageBox.Show("Вы хотите сохранить изменения в этом файле?", "Выход из программы", MessageBoxButton.YesNoCancel);
                if (rezult == MessageBoxResult.Yes)
                    if (bloknot.ASaveBloknot() == false) return;
                if (rezult == MessageBoxResult.Cancel) e.Cancel = true;
            }
        }

        //Кнопка "Открыть"
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if (bloknot.Modified == true)
            {
                MessageBoxResult rezult;
                rezult = MessageBox.Show("Вы хотите сохранить изменения в этом файле?", "Блокнот", MessageBoxButton.YesNoCancel);
                if (rezult == MessageBoxResult.Yes)
                    if (bloknot.ASaveBloknot() == false) return;
                if (rezult == MessageBoxResult.Cancel) return;
            }
             if(bloknot.Open() == false) return;
            this.Title = bloknot.NameFile;
        }

        //Кнопка "Сохранить как"
        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            if (bloknot.ASaveASBloknot() == false) return;
            this.Title  = bloknot.NameFile;
        }

        //Кнопка "Удалить"
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            fieldEdit.Selection.Text = "";
        }

        //Кнопка "Переставить строки"
        private void RearrangeLines_Click(object sender, RoutedEventArgs e)
        {
            //Открываем окно 
            Task task = new Task(fieldEdit);
            task.Owner = this; //Получение ссылки на родителя
            task.ShowDialog();//Открываем диалоговое окно
        }

        //Кнопка "Очистить текст"
        private void ClearText_Click(object sender, RoutedEventArgs e)
        {
            fieldEdit.Document.Blocks.Clear(); //очищаем блокнот
        }
    }
}
