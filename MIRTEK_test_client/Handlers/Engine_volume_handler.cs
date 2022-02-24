namespace MIRTEK_test_client
{
    internal class Engine_volume_handler : Handler_template
    {
        public override object Handle(byte[] byte_array_from_server, int start_position)
        {
            int next_position = start_position;
            List<byte> temp_byte_list = new List<byte>();
            for (int i = start_position; i < byte_array_from_server.Length; i++)
            {
                if (byte_array_from_server[i] == 0x13)
                {
                    Console.Write("Объем двигателя авто: ");
                    if (byte_array_from_server[i + 1] == 0x15)
                    {
                        Console.WriteLine("Не установлено");
                        next_position = next_position + 2;
                    }
                    else
                    {
                        for (int j = i + 1; j < byte_array_from_server.Length; j++)
                        {
                            temp_byte_list.Add(byte_array_from_server[j]);
                            if (byte_array_from_server[j] == 0x12)
                            {
                                next_position = j;
                                break;
                            }
                        }
                        Console.WriteLine(BitConverter.ToDouble(temp_byte_list.ToArray()).ToString());
                    }
                    break;
                }
            }
            return base.Handle(byte_array_from_server, next_position);
        }
    }
}
