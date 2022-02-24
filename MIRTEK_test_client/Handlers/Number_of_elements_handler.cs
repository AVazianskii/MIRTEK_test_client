using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRTEK_test_client
{
    internal class Number_of_elements_handler : Handler_template
    {
        public override object Handle(byte[] byte_array_from_server, int start_position)
        {
            int number_of_elements = 0;
            int next_position = start_position;
            List<byte> temp = new List<byte>();
            for (int i = start_position; i < byte_array_from_server.Length; i++)
            {
                if (byte_array_from_server[i] == 0x09)
                {
                    next_position = i;
                    break;
                }
                temp.Add(byte_array_from_server[i]);
            }
            number_of_elements = BitConverter.ToInt32(temp.ToArray());
            Program.Elements = number_of_elements;
            Program.Next_car_position = next_position;
            Console.WriteLine("Количество элементов в ответе сервера: " + number_of_elements.ToString());

            return base.Handle(byte_array_from_server, next_position);
        }
    }
}
