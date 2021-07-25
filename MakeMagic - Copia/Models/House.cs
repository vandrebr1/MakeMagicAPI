using System;

namespace MakeMagic.Models
{
    public class House
    {
        public House(string id, string name, string headOfHouse, string values, string colors, string mascot, string houseGhost, string founder)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            HeadOfHouse = headOfHouse ?? throw new ArgumentNullException(nameof(headOfHouse));
            Values = values ?? throw new ArgumentNullException(nameof(values));
            Colors = colors ?? throw new ArgumentNullException(nameof(colors));
            Mascot = mascot ?? throw new ArgumentNullException(nameof(mascot));
            HouseGhost = houseGhost ?? throw new ArgumentNullException(nameof(houseGhost));
            Founder = founder ?? throw new ArgumentNullException(nameof(founder));
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string HeadOfHouse { get; private set; }
        public string Values { get; private set; }
        public string Colors { get; private set; }
        public string Mascot { get; private set; }
        public string HouseGhost { get; private set; }
        public string Founder { get; private set; }

    }
}
