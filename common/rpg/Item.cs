using System;

namespace Aoc.Common.Rpg
{       
    public class Item
    {
        public enum GearType
        {
            Weapon = 0,

            Armor = 1,

            Ring = 2
        }

        public string Name { get; set; }

        public GearType Type { get; set; }

        public int Cost { get; set; }

        public int Damage { get; set; }

        public int Armor { get; set; }

        public Item(GearType type, string name, int cost, int damage, int armor)
        {
            Type = type;
            Name = name;
            Cost = cost;
            Damage = damage;
            Armor = armor;
        }
    }
}
