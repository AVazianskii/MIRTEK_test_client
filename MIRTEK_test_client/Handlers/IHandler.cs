using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRTEK_test_client
{
    internal interface IHandler
    {
        // объявляем метод для построения цепочки обработчиков
        IHandler SetNext(IHandler handler);
        // объявляем метод для обработки операции
        object Handle(byte[] byte_array_from_server, int start_position);
    }
}
