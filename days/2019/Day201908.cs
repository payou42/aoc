using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201908 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public int _width;

        public int _height;

        public char[][] _layers;

        public Day201908()
        {
            Codename = "2019-08";
            Name = "Space Image Format";
        }

        public void Init()
        {
            char[] picture = Aoc.Framework.Input.GetString(this).ToCharArray();
            _width = 25;
            _height = 6;
            int count = picture.Length / (_width * _height);
            _layers = new char[count][];
            for (int i = 0; i < count ; ++i)
            {
                _layers[i] = new char[_width * _height];
                Buffer.BlockCopy(picture, i * 2 * (_width * _height), _layers[i], 0, 2 * _width * _height);
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                var layer = _layers.Select(l => (l.Count((c) => c == '0'), l)).OrderBy(l => l.Item1).Select(l => l.Item2).First();
                return (layer.Count((c) => c == '1') * layer.Count((c) => c == '2')).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                var result = new bool[_width * _height];
                for (int i = 0; i < _width * _height; ++i)
                {
                    foreach (var layer in _layers)
                    {                       
                        if (layer[i] == '2')
                        {
                            continue;
                        }

                        result[i] = (layer[i] == '0');
                        break;
                    }
                }

                // Draw the result
                StringBuilder sb = new StringBuilder();
                sb.AppendLine();
                for (int h = 0; h < _height; ++h)
                {
                    for (int w = 0; w < _width; ++w)
                    {
                        sb.Append(result[w + (h * _width)] ? " " : "#");
                    }

                    sb.AppendLine();
                }
                
                return sb.ToString();
            }

            return "";
        }
    }   
}