using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Algoritmai_LD1
{
    class FileArray
    {
        public FileArray(string fileName, int n, int seed)
        {
            int[] data = new int[n];
            Random rand = new Random(seed);
            for (int i = 0; i < n; i++)
            {
                data[i] = rand.Next(0,50);
            }

            if (File.Exists(fileName)) 
                File.Delete(fileName);
           
            using (BinaryWriter writer = new BinaryWriter(File.Open(fileName,
            FileMode.Create)))
            {
               for (int j = 0; j < n; j++)
                 writer.Write(data[j]);
            }
        }

        public FileStream fs { get; set; }

        public int this[int index]
        {
            get
            {
                Byte[] data = new Byte[4];
                fs.Seek(4 * index, SeekOrigin.Begin);
                fs.Read(data, 0, 4);
                int result = BitConverter.ToInt32(data,0);
                return result;
            }
        }
    
}
}
