using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Algoritmai_LD1
{
    class FileList
    {
        public int next;
        public int prev;
        public int current;
        public int N;
        public FileList(string fileName, int n, int seed)
        {
            N = n;
            Random rand = new Random(seed);

            if (File.Exists(fileName))
                File.Delete(fileName);

            using(BinaryWriter wr = new BinaryWriter(File.Open(fileName, FileMode.Create)))
            {
                wr.Write(4); //first node index
                for(int i = 0; i < n; i++)
                {
                    wr.Write(rand.Next(0,50)); //data
                    wr.Write((i + 1) * 8 + 4); //next node index
                }
            }

        }

        public FileStream fs { get; set; }

        public int Head()
        {
            Byte[] data = new Byte[8];     
            fs.Seek(0, SeekOrigin.Begin);
            fs.Read(data, 0, 4);
            current = BitConverter.ToInt32(data, 0);
            prev = -1;
            fs.Seek(current, SeekOrigin.Begin);
            fs.Read(data, 0, 8);
            int result = BitConverter.ToInt32(data, 0);
            next = BitConverter.ToInt32(data, 4);

            return result;
        }

        public int Tail()
        {
            Byte[] data = new Byte[8];
            fs.Seek(N * 8 - 16, SeekOrigin.Begin);  //second from last
            fs.Read(data, 0, 4); // Reading previous node
            prev = BitConverter.ToInt32(data, 0);

            fs.Seek(N * 8 - 8, SeekOrigin.Begin);
            fs.Read(data, 0, 4); // Reading last node
            current = BitConverter.ToInt32(data, 0);
            next = -1;
            fs.Seek(current, SeekOrigin.Begin);
            fs.Read(data, 0, 8);
            int result = BitConverter.ToInt32(data, 0);

            return result;
        }

        public int Previous()
        {
            Byte[] data = new Byte[8];
            fs.Seek(prev, SeekOrigin.Begin);
            fs.Read(data, 0, 8);
            next = current;
            current = prev;
            int result = BitConverter.ToInt32(data, 0);

            if (prev > 4)
            {
                fs.Seek(prev - 12, SeekOrigin.Begin);
                fs.Read(data, 0, 4);
                prev = BitConverter.ToInt32(data, 0);
            }
            else
                prev = -1;

            return result;
        }

        public int Next()
        {
            Byte[] data = new Byte[8];
            fs.Seek(next, SeekOrigin.Begin);
            fs.Read(data,0,8);
            prev = current;
            current = next;
            int result = BitConverter.ToInt32(data, 0);
            next = BitConverter.ToInt32(data,4);

            return result;
        }

        public int Current()
        {
            Byte[] data = new Byte[8];
            fs.Seek(current, SeekOrigin.Begin);
            fs.Read(data, 0, 8);
            int result = BitConverter.ToInt32(data, 0);

            return result;
        }
        public void print()
        {
            Console.Write( Head() + " ");
            for (int i = 1; i < N; i++)
            {
                Console.Write( Next() + " ");
            }
            Console.WriteLine();
        }
    }
}
