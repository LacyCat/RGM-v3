using Exiled.API.Features;
using System;

namespace RGM.API.Interfaces
{
    public class Product
    {
        public bool IsPubliced { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public Action<Player, string> Script { get; set; }
        public Func<Player, string, bool> Check { get; set; } = (player, arg) => true;
    }
}
