using System;
using LolData;

namespace LoLImANerd
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = XMLHelper.ParseXML("Source.xml");
            foreach (var item in data.Data.Champions)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
}
