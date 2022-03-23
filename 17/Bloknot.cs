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
        public Bloknot(RichTextBox fieldEdit)
        {
            nameFile = "";
            this.fieldEdit = fieldEdit;
            Modified = false;
        }

        //Модуль "Сохранить" блокнот в файл
        public bool ASaveBloknot()
        {
            //создаем элемент диалога и настраиваем его
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = "rtf";
            save.Filter = "Текстовый файл (*.rtf)|*.rtf|Текстовый файл (*.txt)|*.txt|Все файлы(*.*)|*.*";
            save.Title = "Сохранение файла";
            //если имя нет
            if (nameFile == "")
                //запрашиваем имя файла
                //если имя файла введено
                if (save.ShowDialog() == true)
                    nameFile = save.FileName; //запомним имя файла
                                            //иначе прерываем модуль (отмена сохранения)
                else return false; //признак успешного сохранения false
            //сохраняем блокнот в файл
            TextRange doc = new TextRange(fieldEdit.Document.ContentStart,
                fieldEdit.Document.ContentEnd);
            FileStream fs = File.Create(nameFile);
            doc.Save(fs, DataFormats.Rtf);
            fs.Close();
            Modified = false; //сбрасываем признак редактирования
            return true;// признак успешного сохранения true
        }

        //Модуль "Сохранить как" блокнот в файл
        public bool ASaveASBloknot()
        {
            nameFile = "";//очищаем имя файла
            //создаем элемент диалога и настраиваем его
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = "rtf";
            save.Filter = "Текстовый файл (*.rtf)|*.rtf|Все файлы(*.*)|*.*";
            save.Title = "Сохранение файла";
            //если имя нет
            if (nameFile == "")
                //запрашиваем имя файла
                //если имя файла введено
                if (save.ShowDialog() == true)
                    nameFile = save.FileName; //запомним имя файла
                                            //иначе прерываем модуль (отмена сохранения)
                else return false; //признак успешного сохранения false
            //сохраняем блокнот в файл
            TextRange doc = new TextRange(fieldEdit.Document.ContentStart,
                fieldEdit.Document.ContentEnd);
            FileStream fs = File.Create(save.FileName);
            doc.Save(fs, DataFormats.Rtf);
            fs.Close();
            Modified = false; //сбрасываем признак редактирования
            return true;// признак успешного сохранения true

        }

        //Модуль создать файл
        public void Create()
        {
            if (Modified == true)
            {//если документ редактировался
                MessageBoxResult result;
                //спросить о сохранении файла ДА/НЕТ/ОТМЕНА
                result = MessageBox.Show("Вы хотите сохранить изменения в файле?",
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
         
        public void Open()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = "rtf";
            open.Filter = "Текстовый файл (*.rtf)|*.rtf|Текстовый файл (*.txt)|*.txt|Все файлы(*.*)|*.*";
            open.Title = "Открытие файла";

            if (open.ShowDialog() == true)
            {
                nameFile = open.FileName;
                TextRange doc = new TextRange(fieldEdit.Document.ContentStart, fieldEdit.Document.ContentEnd);
                using (FileStream fs = new FileStream(open.FileName, FileMode.Open))
                {
                    if (Path.GetExtension(open.FileName).ToLower() == ".rtf")
                        doc.Load(fs, DataFormats.Rtf);
                    else if (Path.GetExtension(open.FileName).ToLower() == ".txt")
                        doc.Load(fs, DataFormats.Text);
                }
            } 
        }
       
    }
}
