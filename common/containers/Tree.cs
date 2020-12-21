using System;
using System.Collections.Generic;

namespace Aoc.Common.Containers
{
    public class Tree<T>
    {
        public delegate void Visitor(T nodeData);

        public class Node
        {
            public Node Parent { get; set; }

            public T Data { get; set; }

            public List<Node> Children { get; private set; }

            public long Weight;

            public long TotalWeight;

            public bool IsRoot
            {
                get
                {
                    return Parent == null;
                }
            }

            public Node(Node parent, T data, long weight = 0)
            {
                Parent = parent;
                Data = data;
                Children = new List<Node>();
                Weight = weight;
                TotalWeight = 0;
            }

            public Node AddChild(T data, long weight = 0)
            {
                Node node = new Node(this, data, weight);
                Children.Add(node);
                return node;
            }

            public void Traverse(Visitor visitor)
            {
                visitor(Data);
                foreach (Node child in Children)
                {
                    child.Traverse(visitor);
                }
            }

            public Node CommonParent(Node child1, Node child2)
            {
                Node current = child1;
                while (current != this && !current.IsParentOf(child2))
                {
                    current = current.Parent;
                }

                return current;
            }

            public bool IsParentOf(Node child)
            {
                Node current = child;
                while (!current.IsRoot && current != this)
                {
                    current = current.Parent;
                }

                return current == this;
            }

            public int DistanceToParent(Node parent)
            {
                Node current = this;
                int dist = 0;
                while (current != parent)
                {
                    dist++;
                    current = current.Parent;
                }

                return dist;
            }
        }

        public Node Root { get; }

        public Tree(T data)
        {
            Root = new Node(null, data, 0);
        }

        public Tree(Node root)
        {
            Root = root;
        }

        public long GetWeight(Node node)
        {
            long weight = node.Weight;
            foreach (var child in node.Children)
            {
                weight += GetWeight(child);
            }

            node.TotalWeight = weight;
            return weight;
        }
    }
}