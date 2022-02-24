using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRTEK_test_client
{
    internal class Model_name_handler : Handler_template
    {
        public override object Handle(byte[] byte_array_from_server, int start_position)
        {
            int Model_name_length = 0;
            for (int i = start_position; i < byte_array_from_server.Length; i++ )
            {
                if (byte_array_from_server[i] == 0x09)
                {
                    Model_name_length = Convert.ToInt32(byte_array_from_server[i + 1]);
                    Console.WriteLine("Длина названия марки авто: " + Model_name_length.ToString());
                    Console.Write("Название марки авто: ");
                    for (int j = i + 2; j < i + Model_name_length + 2; j++ )
                    {
                        Console.Write(Convert.ToChar(byte_array_from_server[j]));
                    }
                    Console.WriteLine();
                    break;
                }
            }
            
            return base.Handle(byte_array_from_server, start_position + 2 + Model_name_length);
        }
    }
}
