using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

namespace Aoc.Common.Graphes
{
    /// <summary>
    /// Square board
    /// </summary>
    /// <typeparam name="Cell">The content of the cell of the board</typeparam>
    public class Graph<N, L>
    {
        public class Node
        {
            public Node(N data)
            {
                Data = data;
            }

            public N Data { get; set; }
        }

        public class Edge
        {
            public Edge(Node n1, Node n2, L data, long weight)
            {
                Endpoints = new Node[2] { n1, n2 };
                Weight = weight;
                Data = data;
            }

            public bool IsDirect(Node n1, Node n2, bool directed)
            {
                if (Endpoints[0] == n1 && Endpoints[1] == n2)
                    return true;
                
                if (!directed && Endpoints[0] == n2 && Endpoints[1] == n1)
                    return true;

                return false;
            }

            public bool IsConnected(Node n, bool directed)
            {
                if (Endpoints[1] == n)
                    return true;

                if (!directed && Endpoints[0] == n)
                    return true;

                return false;
            }

            public Node OtherEndpoint(Node n)
            {
                return (Endpoints[0] == n) ? Endpoints[1] : Endpoints[0];
            }
            
            public Node[] Endpoints { get; private set; }

            public long Weight { get; set; }
            
            public L Data { get; set; }
        }
        
        protected List<Node> _nodes;

        protected List<Edge> _edges;

        protected bool _directed;

       
        public Graph(bool isDirected = false)
        {
            _directed = isDirected;
            _nodes = new List<Node>();
            _edges = new List<Edge>();
        }

        public Node AddNode(N data)
        {
            Node n = new Node(data);
            _nodes.Add(n);
            return n;
        }

        public Edge AddEdge(Node n1, Node n2, L data, long weight)
        {
            // Check for an existing edge first
            var matches = _edges.Where(e => e.IsDirect(n1, n2, _directed));
            if (matches.Count() > 0)
            {
                Edge e = matches.First();
                e.Weight = Math.Min(e.Weight, weight);
                return e;
            }

            // Create a new edge
            Edge l = new Edge(n1, n2, data, weight);
            _edges.Add(l);
            return l;
        }

        public IEnumerable<Edge> EdgesFrom(Node origin)
        {
            return from e in _edges where e.IsConnected(origin, _directed) select e;
        }

        public Dictionary<Node, (long Cost, Node From)> Dijkstra(Node src)
        {
            HashSet<Node> unvisited = new HashSet<Node>(_nodes);
            Dictionary<Node, (long Cost, Node From)> costs = new Dictionary<Node, (long, Node)>();

            foreach (Node n in _nodes)
            {
                costs[n] = (long.MaxValue, null);
            }
            
            Node current = src;
            costs[current] = (0, null);
            while (unvisited.Count > 0)
            {
                var edges = EdgesFrom(current).Where(e => unvisited.Contains(e.OtherEndpoint(current)));
                foreach (var e in edges)
                {
                    Node neighbour = e.OtherEndpoint(current);
                    if (costs[neighbour].Cost > costs[current].Cost + e.Weight)
                    {
                        costs[neighbour] = (costs[current].Cost + e.Weight, current);
                    }
                }

                unvisited.Remove(current);
                current = unvisited.OrderBy(u => costs[u].Item1).FirstOrDefault();
                if (current == null || costs[current].Item1 == long.MaxValue)
                {
                    // Unreachable nodes or end of algo
                    return costs;
                }
            }

            return costs;
        }
    }
}
