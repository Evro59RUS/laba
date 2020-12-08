using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace перебор_перестановок
{
    class Program
    {

        public static double factorial_WhileLoop(int number)
        {
            double result = 1;
            while (number != 1)
            {
                result = result * number;
                number = number - 1;
            }
            return result;
        }
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch time = new Stopwatch();
            time.Start();
            StreamWriter sw = new StreamWriter("OUTPUT.txt");
            string[] lines = File.ReadAllLines("input.txt");
            double[,] WheightMatr = new double[lines.Length, lines[0].Split(' ').Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                for (int j = 0; j < temp.Length; j++)
                {
                    WheightMatr[i, j] = Convert.ToDouble(temp[j]);
                }
            }
            int n = lines.Length;//количество вершин
            //ввод обработали

            string[] shuffle = new string[n];//массив по которому будем считать перестановки и не забыть вернуться в изначальный город
            for (int i = 0; i < n; i++)
            {
                shuffle[i] = Convert.ToString(i);
            }
  

            string[] bestshuffle = shuffle;
            double bestsum = 1000000000000000;

            //теперь генерим перестановки

            // и считаем их шатаясь по матрице связей

            Console.WriteLine("стартуем!");

            List<string> s = new List<string>(shuffle);
            
            for (int k = 0; k < (factorial_WhileLoop(n) - 1); k++)//перебираем все 
            {
                double sum = 0;
                bool flag = true;
                //шатание по матрице
                for (int i = 0; i < n-1; i++)
                {
                    if (WheightMatr[Convert.ToInt32(shuffle[i]), Convert.ToInt32(shuffle[i+1])] == 0)
                    {
                        flag = false;
                        break;
                    }
                    else
                    {
                        sum += WheightMatr[Convert.ToInt32(shuffle[i]), Convert.ToInt32(shuffle[i + 1])];
                    }

                }
                if (WheightMatr[Convert.ToInt32(shuffle[n - 1]), Convert.ToInt32(shuffle[0])] != 0)
                {
                    sum += WheightMatr[Convert.ToInt32(shuffle[n - 1]), Convert.ToInt32(shuffle[0])];
                }
                else flag = false;

                //проверяем сумму на существование
                if (flag == true && sum < bestsum) 
                {
                    bestsum = sum;
                    bestshuffle = shuffle;
                    for (int m = 0; m < shuffle.Length; m++)
                    {
                        sw.Write(bestshuffle[m] + " ");
                    }
                    sw.Write("новая лучшая сумма " + sum + "\n");
                }
                
                //генерим следующую перестановку не трогая единицу вначале и единицу вконце
                string answer="";
                
                for (int i = n - 2; i >= 0; i--)//Двигаясь справа налево
                    {
                        if (s.IndexOf(shuffle[i]) < s.IndexOf(shuffle[i + 1]))//находим элемент, нарушающий убывающую последовательность
                        {
                            for (int j = n - 1; j > i; j--)
                            {
                                if (s.IndexOf(shuffle[j]) > s.IndexOf(shuffle[i]))//Меняем его с минимальным элементом, большим нашего, стоящим правее
                                {
                                    string a = shuffle[j];//swap
                                    shuffle[j] = shuffle[i];
                                    shuffle[i] = a;
                                    Array.Reverse(shuffle, i + 1, n - i - 1);//Перевернем правую часть
                                    break;
                                }
                            }
                            break;
                        }
                    }

                    for (int i = 0; i < n; i++)
                    {                    
                        answer += shuffle[i]+" ";                        
                    }          
                    Console.WriteLine(answer);
                
            }
            sw.Write("последняя вершина совпадает с первой для завершения гамильтонова цикла, нумерация вершин с нуля.\n");
            time.Stop();
            sw.Write("программа выполняется за "+(time.ElapsedMilliseconds / 100.0).ToString()+ " секунд");
            sw.Close();
        }
    }
}
