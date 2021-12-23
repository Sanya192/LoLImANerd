using System.Collections.Generic;

namespace LolData
{
    public class Data
    {
        public List<Champion> Champions;

        public Data()
        {
            Champions = new List<Champion>();
        }

        public Data(List<Champion> champions)
        {
            Champions = champions;
        }
    }


}
