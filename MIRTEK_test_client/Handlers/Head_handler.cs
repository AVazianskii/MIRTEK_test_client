namespace MIRTEK_test_client
{
    internal class Head_handler : Handler_template
    {
        public override object Handle(byte[] byte_array_from_server, int start_position)
        {
            for (int i = start_position; i < byte_array_from_server.Length; i++)
            {
                if (byte_array_from_server[i] == 0x02)
                {
                    Console.WriteLine("Начало ответа сервера");
                    break;
                }
            }
            return base.Handle(byte_array_from_server, start_position + 1);
        }
    }
}
