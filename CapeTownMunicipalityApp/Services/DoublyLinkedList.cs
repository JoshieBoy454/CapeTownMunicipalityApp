using System.Collections;
using System.Collections.Generic;

namespace CapeTownMunicipalityApp.Services
{
    public class DoublyLinkedListNode<T>
    {
        public T Value;
        public DoublyLinkedListNode<T> Next;
        public DoublyLinkedListNode<T> Previous;
        public DoublyLinkedListNode(T value) => Value = value;
    }
    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        public DoublyLinkedListNode<T>? Head { get; private set; }
        public DoublyLinkedListNode<T>? Tail { get; private set; }
        public int Count { get; private set; }

        public void AddLast(T value)
        {
            var node = new DoublyLinkedListNode<T>(value);
            if (Tail == null) Head = Tail = node;
            else
            {
                Tail.Next = node;
                node.Previous = Tail;
                Tail = node;
            }
            Count++;
        }
        public void Remove(DoublyLinkedListNode<T> node)
        {
            if (node.Previous != null) node.Previous.Next = node.Next;
            else Head = node.Next;
            if (node.Next != null) node.Next.Previous = node.Previous;
            else Tail = node.Previous;
            node.Next = node.Previous = null;
            Count--;
        }
        public IEnumerator<T> GetEnumerator()
        {
            var late = Head;
            while (late != null)
            {
                yield return late.Value;
                late = late.Next;
            }
        }
        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
