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

            public bool IsRoot
            {
                get
                {
                    return Parent == null;
                }
            }

            public Node(Node parent, T data)
            {
                Parent = parent;
                Data = data;
                Children = new List<Node>();
            }

            public Node AddChild(T data)
            {
                Node node = new Node(this, data);
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
        }

        public Node Root { get; }

        public Tree(T data)
        {
            Root = new Node(null, data);
        }

        public Tree(Node root)
        {
            Root = root;
        }
    }
}