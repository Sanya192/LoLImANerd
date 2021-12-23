using System.Collections.Generic;

namespace LolData
{
    public class Champion
    {
        public string Version { get; set; }
        public string Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Blurb { get; set; }
        public Info Info { get; set; }
        public Image Image { get; set; }
        public List<string> Tags { get; set; }
        public string Partype { get; set; }
        public Stats Stats { get; set; }

        public override string ToString()
        {
            return $"{Name}: {Title}";
        }
    }


}
