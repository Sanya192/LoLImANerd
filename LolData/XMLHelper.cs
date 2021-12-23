using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace LolData
{
    public static class XMLHelper
    {
        public static Databank ParseXML(string path)
        {
            var xmlDoc = XElement.Load(path);
            return ParseXML(xmlDoc);
        }
        public static Databank ParseXML(XElement xmlDoc)
        {
            var Databank = new Databank
            {
                Type = xmlDoc.Elements().First(x => x.Name.ToString() == "type").Value,
                Format = xmlDoc.Elements().First(x => x.Name.ToString() == "format").Value,
                Version = xmlDoc.Elements().First(x => x.Name.ToString() == "version").Value,
                Data = new Data(xmlDoc.Elements().First(x => x.Name.ToString() == "data").Elements().Select(x => ParseChampionXML(x)).ToList())
            };
            return Databank;
        }

        public static Champion ParseChampionXML(XElement xmlDoc)
        {
            var champion = new Champion
            {
                Version = xmlDoc.Elements().First(x => x.Name.ToString() == "version").Value,
                Id = xmlDoc.Elements().First(x => x.Name.ToString() == "id").Value,
                Key = xmlDoc.Elements().First(x => x.Name.ToString() == "key").Value,
                Name = xmlDoc.Elements().First(x => x.Name.ToString() == "name").Value,
                Title = xmlDoc.Elements().First(x => x.Name.ToString() == "title").Value,
                Blurb = xmlDoc.Elements().First(x => x.Name.ToString() == "blurb").Value
            };
            var info = xmlDoc.Elements().First(x => x.Name.ToString() == "info");
            champion.Info = new Info()
            {
                Attack = Convert.ToInt32(info.Elements().First(x => x.Name.ToString() == "attack").Value),
                Defense = Convert.ToInt32(info.Elements().First(x => x.Name.ToString() == "defense").Value),
                Magic = Convert.ToInt32(info.Elements().First(x => x.Name.ToString() == "magic").Value),
                Difficulty = Convert.ToInt32(info.Elements().First(x => x.Name.ToString() == "difficulty").Value),
            };
            var image = xmlDoc.Elements().First(x => x.Name.ToString() == "image");
            champion.Image = new Image()
            {
                Full = image.Elements().First(x => x.Name.ToString() == "full").Value,
                Sprite = image.Elements().First(x => x.Name.ToString() == "sprite").Value,
                Group = image.Elements().First(x => x.Name.ToString() == "group").Value,
                X = Convert.ToInt32(image.Elements().First(x => x.Name.ToString() == "x").Value),
                Y = Convert.ToInt32(image.Elements().First(x => x.Name.ToString() == "y").Value),
                W = Convert.ToInt32(image.Elements().First(x => x.Name.ToString() == "w").Value),
                H = Convert.ToInt32(image.Elements().First(x => x.Name.ToString() == "h").Value),
            };
            champion.Tags = xmlDoc.Elements().Where(x => x.Name.ToString() == "tags").Select(x => x.Value).ToList();
            champion.Partype = xmlDoc.Elements().First(x => x.Name.ToString() == "partype").Value;
            var stats = xmlDoc.Elements().First(x => x.Name.ToString() == "stats");
            champion.Stats = new Stats()
            {
                Hp = Convert.ToDouble(stats.Elements().First(x => x.Name.ToString() == "hp").Value),
                Hpperlevel = Convert.ToDouble(stats.Elements().First(x => x.Name.ToString() == "hpperlevel").Value),
                Mp = Convert.ToDouble(stats.Elements().First(x => x.Name.ToString() == "mp").Value),
                Mpperlevel = Convert.ToDouble(stats.Elements().First(x => x.Name.ToString() == "mpperlevel").Value),
                Movespeed = Convert.ToDouble(stats.Elements().First(x => x.Name.ToString() == "movespeed").Value),
                Armor = Convert.ToDouble(stats.Elements().First(x => x.Name.ToString() == "armor").Value),
                Armorperlevel = Convert.ToDouble(stats.Elements().First(x => x.Name.ToString() == "armorperlevel").Value),
                Spellblock = Convert.ToDouble(stats.Elements().First(x => x.Name.ToString() == "spellblock").Value),
                Spellblockperlevel = Convert.ToDouble(stats.Elements().First(x => x.Name.ToString() == "spellblockperlevel").Value),
                Attackrange = Convert.ToDouble(stats.Elements().First(x => x.Name.ToString() == "attackrange").Value),
                Hpregen = Convert.ToDouble(stats.Elements().First(x => x.Name.ToString() == "hpregen").Value),
                Hpregenperlevel = Convert.ToDouble(stats.Elements().First(x => x.Name.ToString() == "hpregenperlevel").Value),
                Mpregen = Convert.ToDouble(stats.Elements().First(x => x.Name.ToString() == "mpregen").Value),
                Mpregenperlevel = Convert.ToDouble(stats.Elements().First(x => x.Name.ToString() == "mpregenperlevel").Value),
                Crit = Convert.ToDouble(stats.Elements().First(x => x.Name.ToString() == "crit").Value),
                Critperlevel = Convert.ToDouble(stats.Elements().First(x => x.Name.ToString() == "critperlevel").Value),
                Attackdamage = Convert.ToDouble(stats.Elements().First(x => x.Name.ToString() == "attackdamage").Value),
                Attackdamageperlevel = Convert.ToDouble(stats.Elements().First(x => x.Name.ToString() == "attackdamageperlevel").Value),
                Attackspeedperlevel = Convert.ToDouble(stats.Elements().First(x => x.Name.ToString() == "attackspeedperlevel").Value),
                Attackspeed = Convert.ToDouble(stats.Elements().First(x => x.Name.ToString() == "attackspeed").Value),
            };
            return champion;
        }

        static HttpClient client = new();
        public static XElement FetchTheLatestAndConvertToXML()
        {
            string version = JsonConvert.DeserializeObject<string[]>(client.GetStringAsync("https://ddragon.leagueoflegends.com/api/versions.json").Result)
                .First();
            var result = XElement.Load(new XmlNodeReader(JsonConvert.DeserializeXmlNode(client.GetStringAsync($"http://ddragon.leagueoflegends.com/cdn/{version}/data/en_US/champion.json").Result, "root")));
            result.Save("latestFetch.XML");
            return result;
        }
    }

}
