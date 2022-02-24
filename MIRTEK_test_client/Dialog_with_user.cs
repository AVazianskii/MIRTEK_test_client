using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRTEK_test_client
{
    internal class Dialog_with_user
    {
        internal static void Run_dialog(ref byte[] data)
        {
            Console.WriteLine("Меню доступных команд к серверу");
            Console.WriteLine("1.\t" + "Считать всю информацию обо всех машинах дилера");
            Console.WriteLine("2.\t" + "Считать всю информацию о конкретной машине дилера");
            Console.WriteLine("3.\t" + "Считать конкретную информацию о конкретной машине дилера");
            while (true)
            {
                Console.Write("Введите команду: ");
                string message = Console.ReadLine();

                if (message == "1")
                {
                    Console.WriteLine("Отправлен запрос серверу на считывание информации обо всех машинах \n");
                    data[0] = 1;
                    data[1] = 0;
                    data[2] = 0;
                    break;
                }
                else if(message == "2")
                {
                    data[0] = 2;
                    data[2] = 0;
                    while (true)
                    {
                        Console.WriteLine("Выберите машину из списка:");
                        Console.WriteLine("1.\t" + "Nissan");
                        Console.WriteLine("2.\t" + "Ford");
                        Console.WriteLine("3.\t" + "Opel");
                        message = Console.ReadLine();
                        if (message == "1")
                        {
                            data[1] = 1;
                            break;
                        }
                        else if (message == "2")
                        {
                            data[1] = 2;
                            break;
                        }
                        else if (message == "3")
                        {
                            data[1] = 3;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Некорректный выбор. Попробуйте еще раз");
                        }
                    }
                    break;
                }
                else if (message == "3")
                {
                    data[0] = 3;
                    while (true)
                    {
                        Console.WriteLine("Выберите машину из списка:");
                        Console.WriteLine("1.\t" + "Nissan");
                        Console.WriteLine("2.\t" + "Ford");
                        Console.WriteLine("3.\t" + "Opel");
                        message = Console.ReadLine();
                        if (message == "1")
                        {
                            data[1] = 1;
                            break;
                        }
                        else if (message == "2")
                        {
                            data[1] = 2;
                            break;
                        }
                        else if (message == "3")
                        {
                            data[1] = 3;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Некорректный выбор. Попробуйте еще раз");
                        }
                    }
                    while (true)
                    {
                        Console.WriteLine("Выберите характеристику из списка:");
                        Console.WriteLine("1.\t" + "Название марки");
                        Console.WriteLine("2.\t" + "Год выпуска");
                        Console.WriteLine("3.\t" + "Объем двигателя");
                        Console.WriteLine("4.\t" + "Количество дверей");
                        message = Console.ReadLine();
                        if (message == "1")
                        {
                            data[2] = 1;
                            break;
                        }
                        else if (message == "2")
                        {
                            data[2] = 2;
                            break;
                        }
                        else if (message == "3")
                        {
                            data[2] = 3;
                            break;
                        }
                        else if (message == "4")
                        {
                            data[2] = 4;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Некорректный выбор. Попробуйте еще раз");
                        }
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Некорректный выбор. Попробуйте еще раз");
                }
            }
        }
    }
}
