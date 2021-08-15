using System;
using System.Collections.Generic;
using System.Text;

namespace Algoritmai_LD1
{
    public class BinarySearchTree
    {
        private class Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Data { get; set; }
            public Node() { }
            public Node(int Data)
            {
                this.Data = Data;
                this.Left = null;
                this.Right = null;
            }

        }
        private Node Root { get; set; }
        private int size;

        public BinarySearchTree()
        {}
        public bool Add(int value)
        {
            Node before = null, after = this.Root;

            while (after != null)
            {
                before = after;
                if (value < after.Data) //Is new node in left tree? 
                    after = after.Left;
                else if (value > after.Data) //Is new node in right tree?
                    after = after.Right;
                else
                {
                    //Exist same value
                    return false;
                }
            }

            Node newNode = new Node();
            newNode.Data = value;

            if (this.Root == null)//Tree ise empty
                this.Root = newNode;
            else
            {
                if (value < before.Data)
                    before.Left = newNode;
                else
                    before.Right = newNode;
            }

            return true;
        }

     
        public void PrintTreeInOrder()
        {
            TraverseInOrder(Root);
        }
        private void TraverseInOrder(Node parent)
        {
            if (parent != null)
            {
                TraverseInOrder(parent.Left);
                Console.Write(parent.Data + " ");
                TraverseInOrder(parent.Right);
            }
        }

        public bool Find(int data)
        {
            return this.FindNode(data, this.Root) != null;
        }

        private Node FindNode (int data, Node parent)
        {
            if(parent != null)
            {
                if (data == parent.Data) return parent;
                if (data < parent.Data)
                    return FindNode(data, parent.Left);
                if (data > parent.Data)
                    return FindNode(data, parent.Right);
            }
            return null;
        }

        public void Remove(int data)
        {
            this.Root = removeRecursive(data, Root);
        }
        private Node removeRecursive(int data, Node node)
        {
            //jei medis tuscias
            if (node == null) return node;

            //einam per medi
            if (data < node.Data)  //jei mazesnis i kaire
                node.Left = removeRecursive(data, node.Left);
            else if (data > node.Data) //jei didesnis i desine
                node.Right = removeRecursive(data, node.Right);

            // jei radom elementa
            else
            {
                //jei turi viena vaika kairej arba desinej arba isvis neturi vaiku
                if (node.Left == null)
                    return node.Right;
                else if (node.Right == null)
                    return node.Left;

                //jei turi du vaikus, surandamas maziausias desinej ir pakeiciamas vietom
                node.Data = minValue(node.Right);

                //pasalinamas pasikartojantis maziausias elementas
                node.Right = removeRecursive(node.Data, node.Right);
            }

            return node;
        }
        private int  minValue(Node root)
        {
            int minv = root.Data;
            while (root.Left != null)
            {
                minv = root.Left.Data;
                root = root.Left;
            }
            return minv;
        }
       
        

    }
}
