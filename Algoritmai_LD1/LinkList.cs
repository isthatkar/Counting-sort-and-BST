using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algoritmai_LD1
{
    public class LinkList
    {
        private class Node
        {
            public int Data { get; set; }
            public Node Previous { get; set; }
            public Node Next { get; set; }
            public Node() { }
            public Node(int Data)
            {
                this.Data = Data;
            }
        }

        private Node Head;
        private Node Tail;
        private Node Current;

        public LinkList()
        {
            Tail = new Node();
            Tail.Next = null;

            Head = new Node();
            Head.Previous = null;

            Tail.Previous = Head;
            Head.Next = Tail;

            Current = Head;
        }

        public void Add(int data)
        {
            Node newNode = new Node(data);
            Head.Next.Previous = newNode;
            newNode.Next = Head.Next;
            newNode.Previous = Head;
            Head.Next = newNode;

        }

        public int Length()
        {
            int count = 0;
            for (Node d = Head.Next; d != Tail; d = d.Next)
                count++;
            return count; 
        }
        public IEnumerator GetEnumerator()
        {
            for (Node dd = Head.Next; dd.Next != null; dd = dd.Next)
            {
                yield return dd.Data;
            }
        }

        public LinkList CountingSortLinkList(int k)
        {
            int[] A = new int[k + 1];
            int[] Res = new int[Length()];

            foreach (var item in this)
                A[Convert.ToInt32(item)]++;

            for (int i = 1; i <= k; i++)
                A[i] += A[i - 1];

            for (Node dd = Tail.Previous; dd.Previous != null; dd = dd.Previous)
            {
                Res[A[Convert.ToInt32(dd.Data)] - 1] = Convert.ToInt32(dd.Data);
                A[Convert.ToInt32(dd.Data)]--;
            }

            LinkList Result = new LinkList();

            for (int i = Res.Length -1; i >= 0; i--)
                Result.Add(Res[i]);

            return Result;
        }

        public void PrintLinkList(string Header)
        {
            Console.WriteLine(Header);
            foreach (var item in this)
                Console.Write(item + " ");
            Console.WriteLine();
        }


    }

    
}
