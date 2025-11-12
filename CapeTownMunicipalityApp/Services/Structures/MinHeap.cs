using System;
using System.Collections.Generic;

namespace CapeTownMunicipalityApp.Services
{
    public class MinHeap<T>
    {
        private readonly List<T> _items = new();
        private readonly Comparison<T> _comparison;

        public MinHeap(Comparison<T> comparison)
        {
            _comparison = comparison;
        }

        public int Count => _items.Count;

        public void Push(T item)
        {
            _items.Add(item);
            SiftUp(_items.Count - 1);
        }

        public T Pop()
        {
            if (_items.Count == 0) throw new InvalidOperationException("Heap is empty");
            var root = _items[0];
            var last = _items[^1];
            _items.RemoveAt(_items.Count - 1);
            if (_items.Count > 0)
            {
                _items[0] = last;
                SiftDown(0);
            }
            return root;
        }

        public T Peek()
        {
            if (_items.Count == 0) throw new InvalidOperationException("Heap is empty");
            return _items[0];
        }

        public IEnumerable<T> PopAll()
        {
            while (Count > 0) yield return Pop();
        }

        private void SiftUp(int index)
        {
            while (index > 0)
            {
                var parent = (index - 1) / 2;
                if (_comparison(_items[index], _items[parent]) >= 0) break;
                (_items[index], _items[parent]) = (_items[parent], _items[index]);
                index = parent;
            }
        }

        private void SiftDown(int index)
        {
            while (true)
            {
                var left = index * 2 + 1;
                var right = index * 2 + 2;
                var smallest = index;
                if (left < _items.Count && _comparison(_items[left], _items[smallest]) < 0) smallest = left;
                if (right < _items.Count && _comparison(_items[right], _items[smallest]) < 0) smallest = right;
                if (smallest == index) break;
                (_items[index], _items[smallest]) = (_items[smallest], _items[index]);
                index = smallest;
            }
        }
    }
}


