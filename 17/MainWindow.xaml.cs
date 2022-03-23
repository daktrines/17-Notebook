﻿using System;
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
            if (bloknot.Modified == true)
            {
                MessageBoxResult rezult;
                rezult = MessageBox.Show("Вы хотите сохранить изменения в файле?", "Выход из программы", MessageBoxButton.YesNoCancel);
                if (rezult == MessageBoxResult.Yes)
                    if (bloknot.ASaveBloknot() == false) return;
                if (rezult == MessageBoxResult.Cancel) return;
            }
            else Close();
        }

        //Кнопка "Сохранить"
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            bloknot.ASaveBloknot();
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
                rezult = MessageBox.Show("Вы хотите сохранить изменения в файле?", "Выход из программы", MessageBoxButton.YesNoCancel);
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
                rezult = MessageBox.Show("Вы хотите сохранить изменения в файле?", "Блокнот", MessageBoxButton.YesNoCancel);
                if (rezult == MessageBoxResult.Yes)
                    if (bloknot.ASaveBloknot() == false) return;
                if (rezult == MessageBoxResult.Cancel) return;
            }
            bloknot.Open();
            this.Title = bloknot.NameFile;
        }

        //Кнопка "Сохранить как"
        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            bloknot.ASaveASBloknot();
            this.Title  = bloknot.NameFile;
        }

        //Кнопка "Отменить"
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            fieldEdit.Undo();
        }

        //Кнопка "Вырезать"
        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            fieldEdit.Cut();
        }

        //Кнопка "Копировать"
        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            fieldEdit.Copy();
        }

        //Копка "Вставить"
        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            fieldEdit.Paste();
        }

        //Кнопка "Удалить"
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            fieldEdit.Selection.Text = "";
        }

        //Кнопка "Выделить все"
        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            fieldEdit.SelectAll();
        }

        //Кнопка "Переставить строки"
        private void RearrangeLines_Click(object sender, RoutedEventArgs e)
        {
            //Открываем окно 
            Task task = new Task(fieldEdit);
            task.Owner = this; //Получение ссылки на родителя
            task.ShowDialog();//Открываем диалоговое окно
        }
    }
}