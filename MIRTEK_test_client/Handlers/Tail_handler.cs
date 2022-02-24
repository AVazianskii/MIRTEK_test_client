namespace MIRTEK_test_client
{
    internal class Tail_handler : Handler_template
    {
        public override object Handle(byte[] byte_array_from_server, int start_position)
        {
            int next_position = start_position;
            for (int i = start_position; i < byte_array_from_server.Length; i++)
            {
                if (byte_array_from_server[i] == 0x02)
                {
                    Console.WriteLine("Конец ответа сервера");
                    break;
                }
            }
            next_position = next_position + 1;
            return base.Handle(byte_array_from_server, next_position);
        }
    }
}
