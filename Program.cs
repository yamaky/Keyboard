using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyboard
{
    class Program
    {
        static char wall = EncodingChar(178);

        static void Main(string[] args)
        {
            string startScreen = "\n###################" +
                                 "\n#                 #" +
                                 "\n#     Keyboard    #" +
                                 "\n#                 #" +
                                 "\n###################\n\n";
            Console.Write(startScreen);
            Console.WriteLine("Нажмите Enter");
            Console.ReadLine();
            Console.Clear();

            bool rightEnter = false;
            Console.WriteLine("Введите уровень (1-7):");

            string inputLevel = Console.ReadLine();
            int level;

            rightEnter = int.TryParse(inputLevel, out level);

            while (!rightEnter || level < 1 || level > 7)
            {
                Console.WriteLine("Вы ввели недопустимое значение. Попробуйте еще раз!");
                inputLevel = Console.ReadLine();
                rightEnter = int.TryParse(inputLevel, out level);
            }

            Console.Clear();

            int maxMistake = 7,
                symbols = 1,
                maxTime = 0;

            switch (level)
            {
                case 1:
                    maxMistake = 5;
                    symbols = 3;
                    //maxTime = 15;
                    break;
                case 2:
                    maxMistake = 5;
                    symbols = 5;
                    // maxTime = 15;
                    break;
                case 3:
                    maxMistake = 4;
                    symbols = 7;
                    //maxTime = 15;
                    break;
                case 4:
                    maxMistake = 3;
                    symbols = 7;
                    //maxTime = 15;
                    break;
                case 5:
                    maxMistake = 3;
                    symbols = 9;
                    maxTime = 13;
                    break;
                case 6:
                    maxMistake = 2;
                    symbols = 9;
                    maxTime = 10;
                    break;
                case 7:
                    maxMistake = 2;
                    symbols = 11;
                    maxTime = 10;
                    break;
            }

            char[] a = new char[symbols];
            Random rnd = new Random();
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = EncodingChar((byte)rnd.Next(97, 122));
            }

            int count = 0;
            int countMic = 0;
            bool loose = false;
            DateTime timeForGame = DateTime.Now;
            DateTime timeForKey;
            TimeSpan ellap = new TimeSpan();
            TimeSpan game;

            while (true)
            {
                Console.Write($"Количество ошибок: {countMic}. Время на клавишу: {ellap}");
                Console.WriteLine("\n\n\t");
                for (int i = 0; i < a.Length; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(a[i] + "\t");
                }

                //Console.WriteLine(a[0]);
                timeForKey = DateTime.Now;
                game = DateTime.Now - timeForGame;

                if (count == a.Length || loose || (level > 4 && game.TotalSeconds > maxTime))
                {
                    // Console.WriteLine(game.TotalSeconds);
                    break;
                }

                ConsoleKeyInfo key = Console.ReadKey(true);
                Console.SetCursorPosition(0, 0);
                if (key.KeyChar == a[count])
                {
                    a[count] = wall;
                    count++;
                    ellap = DateTime.Now - timeForKey;
                    //break;
                }
                else
                {
                    countMic++;
                    if (countMic == maxMistake)
                        loose = true;
                }

                if (count == a.Length)
                    break;
                Console.ResetColor();
            }


            Console.ForegroundColor = ConsoleColor.Red;
            if (loose)
            {
                Console.WriteLine("\n\n\t\tYou LOSE!");
            }
            else
            {
                Console.WriteLine("\n\n\t\tYou WIN!");
            }
            Console.WriteLine($"\n\n\t\tYour Time {game.TotalSeconds}");

            Console.Read();
            Console.Read();
        }

        static char EncodingChar(byte numberSym)
        {
            Encoding encoder = Encoding.GetEncoding(437);
            byte[] sym = { numberSym };
            var symbol = encoder.GetString(sym)[0];
            return symbol;
        }
    }
}
