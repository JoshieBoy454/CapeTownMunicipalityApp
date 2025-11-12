using System;
using System.Collections.Generic;

namespace CapeTownMunicipalityApp.Services
{
    public class BinarySearchTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        private class Node
        {
            public TKey Key { get; }
            public TValue Value { get; set; }
            public Node? Left { get; set; }
            public Node? Right { get; set; }
            public Node(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }

        private Node? _root;
        private readonly IComparer<TKey> _comparer;

        public BinarySearchTree() : this(Comparer<TKey>.Default) {}
        public BinarySearchTree(IComparer<TKey> comparer)
        {
            _comparer = comparer;
        }

        public void Insert(TKey key, TValue value)
        {
            if (_root == null)
            {
                _root = new Node(key, value);
                return;
            }
            var current = _root;
            while (true)
            {
                var cmp = _comparer.Compare(key, current.Key);
                if (cmp == 0)
                {
                    current.Value = value;
                    return;
                }
                if (cmp < 0)
                {
                    if (current.Left == null)
                    {
                        current.Left = new Node(key, value);
                        return;
                    }
                    current = current.Left;
                }
                else
                {
                    if (current.Right == null)
                    {
                        current.Right = new Node(key, value);
                        return;
                    }
                    current = current.Right;
                }
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var current = _root;
            while (current != null)
            {
                var cmp = _comparer.Compare(key, current.Key);
                if (cmp == 0)
                {
                    value = current.Value;
                    return true;
                }
                current = cmp < 0 ? current.Left : current.Right;
            }
            value = default!;
            return false;
        }

        public IEnumerable<TValue> InOrder()
        {
            var stack = new Stack<Node>();
            var current = _root;
            while (stack.Count > 0 || current != null)
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.Left;
                }
                current = stack.Pop();
                yield return current.Value;
                current = current.Right;
            }
        }
    }
}


