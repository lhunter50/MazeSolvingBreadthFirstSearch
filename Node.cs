using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    class Node<T>
    {
        public T Element { get; set; }

        public Node<T> Next { get; set; }

        public Node()
        {

        }

        public Node(T element)
        {
            this.Element = element;
        }
        
        public Node(T element, Node<T> nextNode)
        {
            this.Element = element;
            this.Next = nextNode;
        }
    }
}
