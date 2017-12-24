using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Day201711 : Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Int64 _distance_alltime;
        
        private Int64 _distance_final;

        public Day201711()
        {            
            Codename = "2017-11";
            Name = "Hex Ed";
            _distance_alltime = 0;
            _distance_final = 0;
            RunGrid();
        }

        public string Run(Part part)
        {
            if (part == Part.Part1)
            {
                return _distance_final.ToString();
            }

            if (part == Part.Part2)
            {
                return _distance_alltime.ToString();
            }

            return "";
        }

        private void RunGrid()
        {
            HexCoordinate current = new HexCoordinate();
            foreach( string direction in Input.GetStringVector(this, ","))
            {
                current.Move(direction);
                _distance_alltime = Math.Max(_distance_alltime, current.GetDistance());
            }
            _distance_final = current.GetDistance();
        }
    }   
}