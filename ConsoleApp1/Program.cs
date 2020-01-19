using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuestForJob
{
    class Program
    {
        static void Main(string[] args)
        {
            string path1 = @"C:\Users\Public\Documents\File1.txt";
            string path2 = @"C:\Users\Public\Documents\File2.txt";

            //Add(path1);
            //Add(path2);

            Div(path1);
            Div(path2);



            Join(@"C:\Users\Public\Documents\");
        }
        //Объеденение и сортиировка файлов
        public static void Join(string resaultPath)
        {

            List<StreamReader> streams = new List<StreamReader>();
            List<long> longs = new List<long>();
            foreach (string path in Directory.GetFiles(resaultPath, "*_*.temp"))
            {
                streams.Add(new StreamReader(path));
                
            }
            foreach(StreamReader reader in streams)
            {
               
                string line = reader.ReadLine();
                longs.Add(Convert.ToInt64(line));
                
            }
           
            using (StreamWriter sw = new StreamWriter(resaultPath+"result.txt"))
            {
                while (streams.Count != 0)
                {
                    int pos = longs.IndexOf(longs.Min());
                    long min = longs[pos];
                    longs.RemoveAt(pos);
                    sw.WriteLine(min);
                    string line =streams[pos].ReadLine();
                    if(line == null)
                    {
                        streams[pos].Close();
                        streams.RemoveAt(pos);
                    }
                    else
                    {
                        longs.Insert(pos, Convert.ToInt64(line));
                    }
                }
            }
            DeleteTempFile(@"C:\Users\Public\Documents\");
        }
        public static void Div(string path)
        {
            //Разбиение 1 файла на небольшие файлы
            using (StreamReader sr = new StreamReader(path))
            {

                int filecounter = 1;
                List<long> longs = new List<long>();
                string line;
                while ((line = sr.ReadLine()) != null)
                {


                    long a = Convert.ToInt64(line);
                    if (longs.Count == 200000)
                    {
                        longs.Sort();
                        using (StreamWriter sw = new StreamWriter($"{path.Replace(".txt", String.Empty)}_{filecounter}.temp"))
                        {
                            longs.ForEach(o => sw.WriteLine(o));

                        }
                        longs.Clear();
                        longs.Add(a);
                        filecounter++;



                    }
                    else
                    {
                        longs.Add(a);
                    }
                }
                if (longs.Count != 0)
                {
                    filecounter++;
                    longs.Sort();
                    using (StreamWriter sw = new StreamWriter($"{path.Replace(".txt", String.Empty)}_{filecounter}.temp"))
                    {
                        longs.ForEach(o => sw.WriteLine(o));

                    }
                    longs.Clear();
                }
            }
        }
        //Заполнение файлов
        static void Add(string path)
        {
            using (StreamWriter sw = new StreamWriter(path, append: false))
            {
                Random random = new Random();
                int value;
                for (int i = 0; i < 10000000; i++)
                {
                    value = random.Next(Int32.MinValue, Int32.MaxValue);
                    sw.WriteLine(value);
                }
            }
        }
        //Очищение временных файлов
        public static void DeleteTempFile(string path)
        {
            foreach (string pathFile in Directory.GetFiles(path , "*_*.temp"))
            {
                File.Delete(pathFile);
            }


        }
    }
}

