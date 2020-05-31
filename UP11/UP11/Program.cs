using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UP11
{
    class Program
    {
        static void Main(string[] args)
        {
            int k;
            k = CheckInt("Введите k");
            int[] transp = new int[k];
            transp = CreateTransposition(k, transp);
            Console.Write("Шифр: ");
            for (int i = 0; i < transp.Length; i++)
            {
                Console.Write(transp[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Введите текст для шифрования");
            char[] symbols = (Console.ReadLine()).ToCharArray();
            Console.WriteLine();
            Console.WriteLine("а) Зашифрованный текст:");
            symbols = GetCipheredText(k, transp, symbols);
            for (int i = 0; i < symbols.Length; i++)
            {
                Console.Write(symbols[i]);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("б) Расшифрованный текст:");
            symbols = GetRecipheredText(k, transp, symbols);
            for (int i = 0; i < symbols.Length; i++)
            {
                Console.Write(symbols[i]);
            }
            Console.WriteLine();
        }
        public static int CheckInt(string s)
        {
            Console.WriteLine(s);
            bool ok; int n;
            do
            {
                ok = int.TryParse(Console.ReadLine(), out n);
                if (!ok || n < 0) Console.WriteLine("Необходимо ввести натуральное число");
            } while (!ok || n < 0);
            return n;
        }
        public static int[] CreateTransposition(int k, int[] transp)
        {
            Random rnd = new Random();
            int tmp = 0;
            for (int i = 0; i < k; i++)
            {
                transp[i] = rnd.Next(1, k+1);
                tmp = transp[i];
                for (int j = 0; j < i; j++)
                {
                    while (transp[i] == transp[j])
                    {
                        transp[i] = rnd.Next(1, k+1);
                        j = 0;
                        tmp = transp[i];
                    }
                    tmp = transp[i];
                }
            }
            return transp;
        }
        public static char[] GetCipheredText(int k, int[] transp, char[] symbols)
        {
            char[] tmp = new char[k];
            int length = symbols.Length;
            if (length % k != 0)
            {
                char[] newSymbols = new char[symbols.Length + (k - (length % k))];
                symbols.CopyTo(newSymbols, 0);
                symbols = new char[newSymbols.Length];
                newSymbols.CopyTo(symbols, 0); //выделение памяти под недостающие элементы
                for (int i = length; i < symbols.Length; i++)
                {
                    symbols[i] = ' '; //заполнение пустых элементов пробелами
                }
            }
            int count = symbols.Length / k; // количество наборов по k символов в исходном тексте
            for (int i = 0; i < count; i++)
            {
                //за каждый проход
                for (int j = 0; j < k; j++)
                {
                    tmp[j] = symbols[j + k*i]; //заполнение массива tmp символами из введенной строки
                }

                for (int j = 0; j < k; j++)
                {
                    symbols[j + k*i] = tmp[transp[j] - 1];
                }
            }          
            return symbols;
        }
        public static char[] GetRecipheredText(int k, int[] transp, char[] symbols)
        {
            char[] tmp = new char[k];
            int count = symbols.Length / k; // количество наборов по k символов в исходном тексте
            for (int i = 0; i < count; i++)
            {
                //за каждый проход
                for (int j = 0; j < k; j++)
                {
                    tmp[j] = symbols[j + k * i]; //заполнение массива tmp символами из введенной строки
                }

                for (int j = 0; j < k; j++)
                {
                    symbols[j + k * i] = tmp[Array.IndexOf(transp, (j+1))];
                }
            }
            return symbols;
        }
    }
}
