using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using LolData;
using Newtonsoft.Json;

namespace LoLImANerd
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = XMLHelper.ParseXML(XMLHelper.FetchTheLatestAndConvertToXML());
            foreach (var item in data.Data.Champions)
            {
                Console.WriteLine(item);
            }
            XMLHelper.FetchTheLatestAndConvertToXML();
            CreateXMLFromDatabank("Good.XML", data);
            CreateJSONFromDatabank("Good.json", data);
            Console.ReadLine();
        }
        static void CreateXMLFromDatabank(string output,Databank databank)
        {
            var ser = new XmlSerializer(typeof(Databank));
            var sw = new StreamWriter(output);
            ser.Serialize(sw, databank);
        }
        static void CreateJSONFromDatabank(string output,Databank databank)
        {
            string json = JsonConvert.SerializeObject(databank,Formatting.Indented);
            File.WriteAllText(output,json);
        }

    }
}
