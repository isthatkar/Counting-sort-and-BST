using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace Algoritmai_LD1
{
    class Program
    {
        static void Main(string[] args)
        {
            testToSeeIfWorking();
            int[] SampleSizes = new int[] {2000, 4000, 16000, 32000, 64000, 128000, 256000};
           
           

            for (int i = 0; i < SampleSizes.Length; i++)
            {
                Stopwatch sw1 = new Stopwatch();
                Stopwatch sw2 = new Stopwatch();
                Stopwatch sw3 = new Stopwatch();
                Stopwatch sw4 = new Stopwatch();
                Stopwatch sw5 = new Stopwatch();
                Console.WriteLine("Sorting with " + SampleSizes[i] + " elements");
                Console.WriteLine();
                Console.Write("Counting Sort array: ");
                sw1.Start();
                ArraySort(SampleSizes[i], SampleSizes[i]);
                sw1.Stop();
                Console.Write(sw1.Elapsed + "\n");

                Console.Write("Counting Sort file array: ");
                sw2.Start();
                FileArraySort(SampleSizes[i], SampleSizes[i]);
                sw2.Stop();
                Console.Write(sw2.Elapsed + "\n");

                Console.Write("Counting Sort linked list: ");
                sw3.Start();
                LinkListSort(SampleSizes[i], SampleSizes[i]);
                sw3.Stop();
                Console.Write(sw3.Elapsed + "\n");

                Console.Write("Counting Sort list test (Storage): ");
                sw4.Start();
                FileListSort(SampleSizes[i], SampleSizes[i]);
                sw4.Stop();
                Console.Write(sw4.Elapsed + "\n");

                TestFindBST(SampleSizes[i], 2, SampleSizes[i], ref sw5);
                Console.Write("Binary Search Tree Find test: " + sw5.Elapsed + "\n");
                Console.WriteLine();
                Console.WriteLine("------------------------------------------------------");

            }
           


        }

        public static void ArraySort(int n, int seed)
        {
            int[] A = new int[n];
            int[] B = new int[n];
            Random rand = new Random(seed);

            for (int i = 0; i < n; i++)
                A[i] = rand.Next(0,1000); 

            Array.CountingSortArray(A, B, 1000);
        }

        public static void LinkListSort(int n, int seed )
        {
            LinkList list = new LinkList();
            Random rand = new Random(seed);

            for(int i =0; i <n;i++)
            {
                list.Add(rand.Next(0, 100));
            }
            list.CountingSortLinkList(100);

        }
        public static void FileArraySort(int n, int seed)
        {
            FileArray FileArray = new FileArray("dataArray.txt", n, seed);
            FileArray FileArray2 = new FileArray("dataArray2.txt", n, seed);

            using (FileArray.fs = new FileStream("dataArray.txt", FileMode.Open, FileAccess.Read))
            {
                FileArray2.fs = new FileStream("dataArray2.txt", FileMode.Open, FileAccess.ReadWrite);
                BinaryWriter bw = new BinaryWriter(FileArray2.fs);

                int[] C = new int[51]; //values are less than 50
                for (int i = 0; i < n; i++)
                    C[FileArray[i]]++;

                for (int i = 1; i <= 50; i++)
                    C[i] += C[i - 1];

                for (int i = n - 1; i >= 0; i--)
                {
                    setNumber(bw, C[FileArray[i]] - 1, FileArray[i]);
                    C[FileArray[i]]--;
                }
                //BinaryReader rd = new BinaryReader(FileArray.fs);
                //BinaryReader rd2 = new BinaryReader(FileArray2.fs);
                //Console.WriteLine("NESURIKIUOTI FILE ARRAY");
                //for (int i = 0; i < 50; i++)
                //    Console.Write(getNumber(rd, i) + " ");
                //Console.WriteLine();
                //Console.WriteLine("SURIKIUOTI FILE ARRAY");
                //for (int i = 0; i < 50; i++)
                //    Console.Write(getNumber(rd2, i) + " ");
                //Console.WriteLine();
                bw.Close();
            }
        }
        
        public static void FileListSort(int BinaryDataSize, int seed)
        {
            FileList myListFile = new FileList("listFile.txt", BinaryDataSize, seed);
            FileList myListFile2 = new FileList("listFile2.txt", BinaryDataSize, seed);

            using (myListFile.fs = new FileStream("listFile.txt", FileMode.Open, FileAccess.Read))
            {
                myListFile2.fs = new FileStream("listFile2.txt", FileMode.Open, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(myListFile2.fs);

                int[] C = new int[51]; //values are less than 50

                for (int i = 0; i < myListFile.N; i++)
                {
                    if (i == 0)
                        C[myListFile.Head()]++;
                    else
                        C[myListFile.Next()]++;
                }
                for (int i = 1; i <= 50; i++)
                    C[i] += C[i - 1];

                for (int i = myListFile.N; i > 0; i--)
                {
                    if (i == myListFile.N)
                    {
                        setNumberList(bw, C[myListFile.Tail()] - 1, myListFile.Current());
                        C[myListFile.Current()]--;
                    }
                    else
                    {
                        setNumberList(bw, C[myListFile.Previous()] - 1, myListFile.Current());
                        C[myListFile.Current()]--;
                    }
                }
                //Console.WriteLine("NESURIKIUOTI LIST FILE");
                //myListFile.print();
                bw.Close();
            }
            //using (myListFile2.fs = new FileStream("listFile2.txt", FileMode.Open, FileAccess.Read))
            //{
            //    Console.WriteLine("SURIKIUOTI LIST FILE");
            //    myListFile2.print();
            //}
        }

        public static void TestFindBST(int n, int m, int seed,  ref Stopwatch sw)
        {
            int found = 0;
            int notfound = 0;
            int count = 0;
            List<int> A = new List<int>();
            Random rand = new Random();
            while(count < n)
            {
                int temp = rand.Next();
                if (!A.Contains(temp))
                {
                    A.Add(temp);
                    count++;
                }
            }
            BinarySearchTree tree = new BinarySearchTree();
            for (int i = 0; i < n; i = i + m)
            {
                tree.Add(A[i]);
            }
            sw.Start();
            for (int i = 0; i < n; i++)
            {
                if (tree.Find(A[i])) found++;
                else notfound++;
            }
            sw.Stop();
            Console.WriteLine("Viso elementu medyje: " + count);
            Console.WriteLine("Rasti elementai: " + found);
            Console.WriteLine("Nerasti elementai: " + notfound);

        }

        public static void setNumber(BinaryWriter bw, int i, int value)
        {
            bw.BaseStream.Seek(i * 4, SeekOrigin.Begin);
            bw.Write(value);
        }
        public static int getNumber(BinaryReader br, int i)
        {
            int k = (i) * 4;
            br.BaseStream.Seek(k, SeekOrigin.Begin);
            return br.ReadInt32();
        }

        public static void setNumberList(BinaryWriter bw, int i, int value)
        {
            int k = (i) * 8 + 4;
            bw.BaseStream.Seek(k, SeekOrigin.Begin);
            bw.Write(value);
        }


        
        public static void testToSeeIfWorking()
        {
            int k = 50;
            int[] A = new int[k]; //not sorted
            int[] B = new int[k]; //sorted
            LinkList Alist = new LinkList();

            Random rand = new Random();
            for (int i = 0; i < k; i++)
            {
                int Numb = rand.Next(0, 49);
                A[i] = Numb;
                Alist.Add(Numb);
            }

            //testing array
            Array.PrintArray(A, "Not sorted array:");
            Array.CountingSortArray(A, B, k);
            Array.PrintArray(B, "Sorted Array:");

            //testing linked list
            Alist.PrintLinkList("Not Sorted List");
            LinkList Blist = Alist.CountingSortLinkList(k);
            Blist.PrintLinkList("Sorted List");

            //adding elements from array to bst by step 2 and seraching for all 50 elements
            BinarySearchTree tree = new BinarySearchTree();
            for (int i = 0; i < k; i = i + 2)
            {
                tree.Add(A[i]);
            }
            Console.WriteLine();
            Console.WriteLine("Medis:");
            tree.PrintTreeInOrder();
            Console.WriteLine("Kiekvieno masyvo elemento paieska medyje:");
            Console.WriteLine();
            for (int i = 0; i < k; i++)
                Console.WriteLine(A[i] + " " + tree.Find(A[i]) + " ");

            //------------------------------------- FILE ARRAY TEST
            FileArraySort(50, 50);
            //------------------------------------- FILE LINK LIST TEST
            FileListSort(50, 50);
        }
    }
}
