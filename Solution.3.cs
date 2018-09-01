using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
 
namespace thread2316402
{
    public partial class Form1 : Form
    {
        string[] args = Environment.GetCommandLineArgs();//вытягивает параметр
        List<string> words;//Все слова
        List<DataTemp> wordsAndFrequency;//результат через клас Data
        List<string> ThereAreWords;//Уникальные слова
        public Form1()
        {
            InitializeComponent();
            words  = new List<string>();
            wordsAndFrequency = new List<DataTemp>();
            ThereAreWords = new List<string>();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            if (args.Length > 1)//если есть параметри запускаем метод
            {
                Result();
            }
            else
            {
                label1.Text = "Запуск без параметров";
            }
           
 
        }
        public void Result()
        {
           //Считываем из файла
            string AllData = System.IO.File.ReadAllText(args[1].ToString());
            //из строки розбиваем по словам и записываем их в words
            StrigToWord(AllData);
            //Каждое слово с маленькой
            ToLowerWord();
            //отбор уникальних слов
            AllWordToOneWords();
            //сортировка по алфавиту
            ThereAreWords.Sort();
            //Считаем слова
            СountWords();
            //Формируется строка отчета в методе Otvet
            //запись в файл
            // File.WriteAllLines WriteAllText(args[2].ToString(), Otvet());
            File.WriteAllLines(args[2].ToString(), Otvet());
            label1.Text = "ГОТОВО";
 
 
 
 
        }
        public string[] Otvet()//Формируется строка отчета
        {
            List<string> temp = new List<string>();
            temp.Add(Convert.ToString(wordsAndFrequency[0].Word[0]));
            
            string OneLetter = Convert.ToString(wordsAndFrequency[0].Word[0]);
 
            for (int i = 0; i < wordsAndFrequency.Count; i++)
            {
 
                string temp2 = Convert.ToString(wordsAndFrequency[i].Word[0]);
                if (OneLetter == temp2)
                {
                    temp.Add(Convert.ToString(wordsAndFrequency[i].Word + "  " + wordsAndFrequency[i].Frequency + "\n"));
                }
                else
                {
                    OneLetter = Convert.ToString(wordsAndFrequency[i].Word[0]);
                    temp.Add(Convert.ToString(OneLetter));
 
                    temp.Add(Convert.ToString(wordsAndFrequency[i].Word + "  " + wordsAndFrequency[i].Frequency + "\n"));
                }
 
 
            }
            string[] otvet = new string[temp.Count];
                for(int y=0;y<temp.Count;y++)
                {
                    otvet[y] = temp[y];
                }
             return otvet;
            
        }
        public void СountWords()//Считаем слова
        {
            for (int i = 0; i < ThereAreWords.Count; i++)
            {
                int temp = 0;
                for (int y = 0; y < words.Count; y++)
                {
                    if (ThereAreWords[i] == words[y])
                    {
                        temp = temp + 1;
                    }
                }
                wordsAndFrequency.Add(new DataTemp(ThereAreWords[i], temp));
            }
        }
        public void AllWordToOneWords()//отбор уникальних слов
        {
            ThereAreWords.Add(words[0]);
            for (int i = 0; i < words.Count; i++)
            {
 
                bool temp = true;
                for (int y = 0; y < ThereAreWords.Count; y++)
                {
                    if (words[i] == ThereAreWords[y])
                    {
                        temp = false;
                        break;
                    }
 
                }
                if (temp)
                {
                    ThereAreWords.Add(words[i]);
                }
 
            }
        }
        public void ToLowerWord()//Каждое слово с маленькой
        {
            for (int i = 0; i < words.Count; i++)
            {
                words[i] = words[i].ToLower();
            }
        }
        public void StrigToWord(string AllData)//из строки розбиваем по словам
        {
            string temp = "";
            for (int i = 0; i < AllData.Length; i++)
            {
                if (AllData[i].ToString() != " ")
                {
                    temp += AllData[i];
                }
                else
                {
                    if (temp != "")
                    {
                        words.Add(temp);
                        temp = "";
                    }
                }
            }
            words.Add(temp);//последнее слово
        }
    }
}