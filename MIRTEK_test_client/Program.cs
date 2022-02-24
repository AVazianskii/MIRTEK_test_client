using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace MIRTEK_test_client
{
    class Program
    {
        // адрес и порт сервера, к которому будем подключаться
        static int port = 8005; // порт сервера
        static string address = "127.0.0.1"; // адрес сервера
        static internal byte[] data;
        static internal int Elements;
        static internal int Next_car_position;
        static void Main(string[] args)
        {
            Handler_template Head = new Head_handler();
            Handler_template Number_of_elements = new Number_of_elements_handler();
            Handler_template Model_name = new Model_name_handler();
            Handler_template Production_Year = new Production_year_handler();
            Handler_template Engine_volume = new Engine_volume_handler();
            Handler_template Number_of_doors = new Number_of_doors_handler();
            Handler_template Tail = new Tail_handler();
            Head.SetNext(Number_of_elements).SetNext(Model_name).SetNext(Production_Year).SetNext(Engine_volume).SetNext(Number_of_doors);
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // подключаемся к удаленному хосту
            socket.Connect(ipPoint);
            data = new byte []{ 0, 0, 0 }; // массив на 3 байта для формирования запросов серверу

            // скорректировать под запрос в 2 байта: 0 байт - тип команды: 1 - считать все (реализовано),
            //                                                             2 - считать конкретную машину (реализовано),
            //                                                             3 - считать конкретную запись конкретной машины (реализовано)
            //                                       1 байт - номер считываемой машины, если 0 байт равен 2 или 3 (реализовано)
            //                                       2 байт - номер записи в выбранной машине, если 0 байт равен 2 или 3, а 1 байт отличен от 0 (реализовано)
            Dialog_with_user.Run_dialog(ref data);

            socket.Send(data);

            // получаем ответ
            data = new byte[256]; // буфер для ответа
                
            int bytes = 0; // количество полученных байт
            string answer;
            do
            {
                bytes = socket.Receive(data, data.Length, 0);
                answer = BitConverter.ToString(data);
            }
            while (socket.Available > 0);
            Console.WriteLine("ответ сервера: " + answer);

            // Запускаем цепочку обработчиков для восстановления данных для вывода на экран
            Head.Handle(data,0);
            for (int i = 0; i < Elements / 4 - 1; i++)
            {
                Console.WriteLine($"Машина №{i + 2}:");
                Model_name.Handle(data, Next_car_position);
                if(i == Elements / 4 - 1)
                {
                    Tail.Handle(data, Next_car_position);
                }
            }
            for(int i = 0; i < data.Length; i++)
            {
                data[i] = 0;
            }
            // закрываем сокет
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

            Console.Read();
        }
    }
}