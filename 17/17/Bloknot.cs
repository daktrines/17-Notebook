using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace _17
{
    class Bloknot
    {
        string nameFile; //Имя файла
        RichTextBox fieldEdit; //поле редактирования
        public bool Modified { get; set; } //признак редактирования
        public string NameFile //имя файла
        {
            get { return nameFile; }
        }
        public Bloknot (RichTextBox fieldEdit)
        {
            nameFile = "";
            this.fieldEdit = fieldEdit;
            Modified = false;
        }

        //Модуль сохранить блокнот в файл
        private bool ASaveBloknot()
        {
            //создаем элемент диалога и настраиваем его
            SaveFileDialog sd = new SaveFileDialog();
            sd.DefaultExt = "rtf";
            sd.Filter = "Текстовый файл (*.rtf)|*.rtf|Все файлы(*.*)|*.*";
            sd.Title = "Сохранение файла";
            //если имя нет
            if (nameFile == "")
                //запрашиваем имя файла
                //если имя файла введено
                if (sd.ShowDialog() == true)
                    nameFile = sd.FileName; //запомним имя файла
            //иначе прерываем модуль (отмена сохранения)
                else return false; //признак успешного сохранения false
            //сохраняем блокнот в файл
            TextRange doc = new TextRange(fieldEdit.Document.ContentStart,
                fieldEdit.Document.ContentEnd);
            FileStream fs = File.Create(sd.FileName);
            doc.Save(fs, DataFormats.Rtf);
            fs.Close();
            Modified = false; //сбрасываем признак редактирования
            return true;// признак успешного сохранения true
        }

        //Модуль создать файл
        public void Create()
        {
            if(Modified == true )
            {//если документ редактировался
                MessageBoxResult result;
                //спросить о сохранении файла ДА/НЕТ/ОТМЕНА
                result = MessageBox.Show("Вы хотите сохранить изменения в файле",
                    "Блокнот", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)//если ДА сохраняем файл 
                    //вызываем модуль сохранения и если сохранение не прошло
                    //прерываем событие (отмена текущего действия)
                    if (ASaveBloknot() == false) return;
                //если ОТМЕНА
                //прерываем событие (отмена текущего действия)
                if (result == MessageBoxResult.Cancel) return;
            }
            //при успешном развитии событий
            fieldEdit.Document.Blocks.Clear(); //очищаем блокнот
            nameFile = "";//очищаем имя файла
            Modified = false;//сброс признака редактирования
        }
        //private bool AOpenBloknot()
        //{
        //    OpenFileDialog open = new OpenFileDialog();
        //    open.DefaultExt = "rtf";
        //    open.Filter = "Текстовый файл (*.rtf)|*.rtf|Все файлы(*.*)|*.*";
        //    open.Title = "Открытие файла";
        //    if (nameFile == "")


        //        if (open.ShowDialog() == true)
        //            nameFile = open.FileName;
        //        else return false;
        //    TextRange doc = new TextRange(fieldEdit.Document.ContentStart,
        //        fieldEdit.Document.ContentEnd);
        //    FileStream fs = File.Create(open.FileName);
        //    doc.Open(fs, DataFormats.Rtf);
        //    fs.Close();
        //    Modified = false;
        //    return true;
        //}
    }
}
