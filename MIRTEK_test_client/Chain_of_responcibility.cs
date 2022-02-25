using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRTEK_test_client
{
    internal class Chain_of_responcibility
    {
        static Handler_template Head = new Head_handler();
        static Handler_template Number_of_elements = new Number_of_elements_handler();
        static Handler_template Model_name = new Model_name_handler();
        static Handler_template Production_Year = new Production_year_handler();
        static Handler_template Engine_volume = new Engine_volume_handler();
        static Handler_template Number_of_doors = new Number_of_doors_handler();
        static Handler_template Tail = new Tail_handler();
        internal static void Setup_chain()
        {
            Head.SetNext(Number_of_elements).SetNext(Model_name).SetNext(Production_Year).SetNext(Engine_volume).SetNext(Number_of_doors);
        }
        internal static void Run_chain_from_head(byte[] byte_array_from_server, ref int start_position)
        {
            Head.Handle(byte_array_from_server, start_position);
        }
        internal static void Run_chain_from_main_body(byte[] byte_array_from_server, ref int start_position)
        {
            Model_name.Handle(byte_array_from_server, start_position);
        }
        internal static void Run_chain_from_tail(byte[] byte_array_from_server, ref int start_position)
        {
            Tail.Handle(byte_array_from_server, start_position);
        }
    }
}
