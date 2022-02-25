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
            // Конфигурируем цепь обработчиков байтовой последовательности
            // для цепи обработчиков крайне важен порядок обрабатываемой последовательность
            // в задании последовательность принята такая: 
            // начало ответа -> вычисление количества передаваемых элементов -> дешифровка названия марки -> дешифровка года выпуска авто ->
            // -> дешифровка объема двигателя авто -> дешифровка количества дверей -> конец ответа сервера
            // Цепочка обработчиков реализует как раз такую последовательность
            Chain_of_responcibility.Setup_chain();

            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // подключаемся к удаленному хосту
            socket.Connect(ipPoint);

            data = new byte []{ 0, 0, 0 }; // массив на 3 байта для формирования запросов серверу

            // запрос серверу состоит из 3 байт:     0 байт - тип команды: 1 - считать все (реализовано),
            //                                                             2 - считать конкретную машину (реализовано),
            //                                                             3 - считать конкретную запись конкретной машины (реализовано)
            //                                       1 байт - номер считываемой машины, если 0 байт равен 2 или 3 (реализовано)
            //                                       2 байт - номер записи в выбранной машине, если 0 байт равен 2 или 3, а 1 байт отличен от 0 (реализовано)
            // собираем выбор юзера в запрос
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
            int start_position = 0;
            Chain_of_responcibility.Run_chain_from_head(data,ref start_position);
            // Определяем количество переданных элементов, и определяем количество переданных машин
            for (int i = 0; i < Elements / 4 - 1; i++)
            {
                Console.WriteLine($"Машина №{i + 2}:");
                // конец считывания первой машины равно началу считывания второй машины
                Chain_of_responcibility.Run_chain_from_main_body(data, ref Next_car_position);
                if(i == Elements / 4)
                {
                    Chain_of_responcibility.Run_chain_from_tail(data, ref Next_car_position);
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