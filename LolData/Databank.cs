using System;
using System.Linq;

namespace LolData
{

    public class Databank
    {
        public string Type { get; set; }
        public string Format { get; set; }
        public string Version { get; set; }
        public Data Data { get; set; }
    }

}
