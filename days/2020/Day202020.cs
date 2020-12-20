using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Grid;

namespace Aoc
{
    public class Day202020 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public class Tile
        {
            public long Id;

            public string[] Data;

            public string[] Edges;

            public int Size;

            public int Disposition;

            public Tile(string input)
            {         
                // Read the input       
                string[] items = input.Split("\r\n");
                Id = long.Parse(items[0].Split(" ")[1][..^1]);
                Data = items.Skip(1).ToArray();

                // Set variables
                Size = Data.Length;
                Disposition = 0;

                // Prexompute the edges
                this.Edges = new[]
                {
                    GetEdge(0, 0, 0, 1),
                    GetEdge(0, 0, 1, 0),
                    GetEdge(Size - 1, 0, 0, 1),
                    GetEdge(Size - 1, 0, -1, 0),
                    GetEdge(0, Size - 1, 0, -1),
                    GetEdge(0, Size - 1, 1, 0),
                    GetEdge(Size - 1, Size - 1, 0, -1),
                    GetEdge(Size - 1, Size - 1, -1, 0),
                };
            }

            public void NextDisposition()
            {
                Disposition = (Disposition + 1) % 8;
            }

            public string GetRow(int irow) => GetEdge(irow, 0, 0, 1);

            public string GetTop() => GetEdge(0, 0, 0, 1);
        
            public string GetBottom() => GetEdge(Size - 1, 0, 0, 1);
        
            public string GetLeft() => GetEdge(0, 0, 1, 0);
        
            public string GetRight() => GetEdge(0, Size - 1, 1, 0);

            public char GetChar(int irow, int icol)
            {
                for (var i = 0; i < Disposition % 4; i++)
                {
                    (irow, icol) = (icol, Size - 1 - irow);
                }

                if (Disposition % 8 >= 4)
                {
                    icol = Size - 1 - icol;
                }

                return this.Data[irow][icol];
            }

            public string GetEdge(int irow, int icol, int drow, int dcol)
            {
                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < Size; i++)
                {
                    sb.Append(GetChar(irow, icol));
                    irow += drow;
                    icol += dcol;
                }

                return sb.ToString();
            }
        }

        private List<Tile> _tiles;

        private int _length;

        private Board2D<Tile> _grid;

        public Day202020()
        {
            Codename = "2020-20";
            Name = "Jurassic Jigsaw";
        }

        public void Init()
        {
            _tiles = new List<Tile>();
            var input = Aoc.Framework.Input.GetStringVector(this, "\r\n\r\n");
            foreach (string t in input)
            {
                _tiles.Add(new Tile(t));
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // SOlve the grid
                _length = (int)Math.Sqrt(_tiles.Count);
                _grid = new Board2D<Tile>();
                Solve(_grid, _length);
                return (_grid[0, 0].Id * _grid[0, _length - 1].Id * _grid[_length - 1, 0].Id * _grid[_length - 1, _length - 1].Id).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Extract the image from the grid
                int tileSize = _tiles[0].Size;
                List<string> pictureList = new List<string>();
                for (var irow = 0; irow < _length; irow++)
                {
                    for (var i = 1; i < tileSize -1; i++)
                    {
                        var st = "";
                        for (var icol = 0; icol < _length; icol++)
                        {
                            st += _grid[irow, icol].GetRow(i)[1..^1];
                        }

                        pictureList.Add(st);
                    }
                }

                var picture = new Tile("Tile 0:\r\n" + string.Join("\r\n", pictureList));
                var seaMonster = new string[]
                {
                    "                  # ",
                    "#    ##    ##    ###",
                    " #  #  #  #  #  #   "
                };

                for (int disposition = 0; disposition < 9; disposition++)
                {                
                    var cmatch = CountMonster(picture, seaMonster);
                    if (cmatch > 0)
                    {
                        var hashCount = 0;
                        for (var irow = 0; irow < picture.Size; irow++)
                        {
                            for (var icol = 0; icol < picture.Size; icol++)
                            {
                                if (picture.GetChar(irow, icol) == '#')
                                {
                                    hashCount++;
                                }
                            }
                        }

                        return (hashCount - cmatch * 15).ToString();
                    }

                    picture.NextDisposition();
                }
            }

            return "";
        }

        private void Solve(Board2D<Tile> grid, int length)
        {
            List<Tile> available = new List<Tile>(_tiles);
            for (var irow = 0; irow < length; irow++)
            {
                for (var icol = 0; icol < length; icol++)
                {
                    var topPattern = irow == 0 ? null : grid[irow - 1, icol].GetBottom();
                    var leftPattern = icol == 0 ? null : grid[irow, icol - 1].GetRight();
                    var tile = FindMatchingTile(available, topPattern, leftPattern);
                    grid[irow, icol] = tile;
                    available.Remove(tile);
                }
            }
        }

        Tile FindMatchingTile(List<Tile> available, string top, string left)
        {
            foreach (var tile in available)
            {
                for (var i = 0; i < 8; i++)
                {
                    var topMatch = top != null ? tile.GetTop() == top : !available.Any(other => other.Id != tile.Id && other.Edges.Contains(tile.GetTop()));
                    var leftMatch = left != null ? tile.GetLeft() == left : !available.Any(other => other.Id != tile.Id && other.Edges.Contains(tile.GetLeft()));
                    if (topMatch && leftMatch)
                    {
                        return tile;
                    }

                    tile.NextDisposition();
                }
            }

            return null;
        }

        int CountMonster(Tile picture, string[] monster)
        {
            var res = 0;
            for (var irow = 0; irow < picture.Size; irow++)
            {
                for (var icol = 0; icol < picture.Size; icol++)
                {
                    if (IsSeaMonster(picture, icol, irow, monster))
                    {
                        res++;
                    }
                }
            }

            return res;
        }

        bool IsSeaMonster(Tile picture, int x, int y, string[] monster)
        {
            var ccolM = monster[0].Length;
            var crowM = monster.Length;
            if (x + ccolM >= picture.Size)
            {
                return false;
            }

            if (y + crowM >= picture.Size)
            {
                return false;
            }

            for (var icolM = 0; icolM < ccolM; icolM++)
            {
                for (var irowM = 0; irowM < crowM; irowM++)
                {
                    if (monster[irowM][icolM] == '#' && picture.GetChar(y + irowM, x + icolM) != '#')
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }   
}