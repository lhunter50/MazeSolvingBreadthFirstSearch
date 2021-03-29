using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    class Queue<T>
    {
        public Node<T> Head { get; set; }
        public Node<T> Tail { get; set; }
        public int Size { get; set; }

        public Queue()
        {
            Clear();
        }

        public void Clear()
        {
            Head = default(Node<T>);
            Tail = default(Node<T>);
            Size = 0;
        }

        public void Enqueue(T element)
        {
            Node<T> newNode = new Node<T>(element);

            if(Head == null)
            {
                Head = newNode;
                Tail = newNode;
            } 
            else
            {
                Tail.Next = newNode;
                Tail = newNode;
            }

            Size++;
        }

        public T Front()
        {
            if (IsEmpty()) throw new ApplicationException();

            return Head.Element;
        }

        public T Dequeue()
        {
            if (IsEmpty()) throw new ApplicationException();

            T current = Front();

            if (Size == 1)
            {
                Clear();
            }
            else
            {
                Head = Head.Next;
                Size--;

            }
            return current;
        }

        public bool IsEmpty()
        {
            return Size == 0;
        }
    }
}
