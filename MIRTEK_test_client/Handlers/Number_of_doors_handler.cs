namespace MIRTEK_test_client
{
    internal class Number_of_doors_handler : Handler_template
    {
        public override object Handle(byte[] byte_array_from_server, int start_position)
        {
            int next_position = start_position;
            List<byte> temp_byte_list = new List<byte>();
            for (int i = start_position; i < byte_array_from_server.Length; i++)
            {
                if (byte_array_from_server[i] == 0x12)
                {
                    Console.Write("Количество дверей авто: ");
                    if (byte_array_from_server[i + 1] == 0x15)
                    {
                        Console.WriteLine("Не установлено");
                        next_position = next_position + 2;
                        Program.Next_car_position = next_position;
                    }
                    else
                    {
                        for (int j = i + 1; j < i + 5; j++) // byte_array_from_server.Length
                        {
                            temp_byte_list.Add(byte_array_from_server[j]);
                            if (j == i + 4) // byte_array_from_server[j] == 0x02
                            {
                                next_position = j;
                                Program.Next_car_position = next_position;
                                break;
                            }
                        }
                        Console.WriteLine(BitConverter.ToInt32(temp_byte_list.ToArray()).ToString());
                    }
                    break;
                }
            }
            return base.Handle(byte_array_from_server, next_position);
        }
    }
}
