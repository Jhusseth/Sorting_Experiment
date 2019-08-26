﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        String directionFolder;
        String directionFile;
        private Random random;
        private static int RUN = 10;
        private static int[] dataArray;
        private static int EXPERIMENT = 100;
        string root;
        string timeRoot;
        string[] directions;
        Clock clk;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();

        
            
            

            if (openFileDialog1.ShowDialog() == DialogResult.OK )
            {
                directionFolder = openFileDialog1.SelectedPath;
                
                generateData();
                //...
            }
        }

        public void generateData()
        {
            random = new Random();
            directions = new string[3];
            generateArrayInfo(1000,0);
            generateArrayInfo(40,1);
            generateArrayInfo(80,2);
        }

        public void generateArrayInfo(int size,int pos)
        {
            StringBuilder cvsContent = new StringBuilder();
            string aux = "";

            for (int i = 0; i < size-1; i++)
            {
                if (i == 0)
                {
                    aux = "" + random.Next(4000);
                }
                aux += "," + random.Next(4000);

               

                

            }
            cvsContent.AppendLine(aux);
            directions[pos] = directionFolder + "/FileSize" + size + ".csv";
            File.AppendAllText(directions[pos], cvsContent.ToString());
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            root = directionFolder + "/order";
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }


           
                transformInfoInArray(0);
                timSort(ref dataArray, dataArray.Length);
                writeOrderArray(1);
                transformInfoInArray(0);
                timeExperiment(0,1);

                transformInfoInArray(1);
                timSort(ref dataArray, dataArray.Length);
                writeOrderArray(1);
                transformInfoInArray(1);
                timeExperiment(1,1);

                transformInfoInArray(2);
                timSort(ref dataArray, dataArray.Length);
                writeOrderArray(1);
                transformInfoInArray(2);
                timeExperiment(2,1);

                transformInfoInArray(0);
                timSort(ref dataArray, dataArray.Length);
                writeOrderArray(2);
                transformInfoInArray(0);
                timeExperiment(0,2);

                transformInfoInArray(1);
                timSort(ref dataArray, dataArray.Length);
                writeOrderArray(2);
                transformInfoInArray(1);
                timeExperiment(1,2);

                transformInfoInArray(2);
                timSort(ref dataArray, dataArray.Length);
                writeOrderArray(2);
                transformInfoInArray(2);
                timeExperiment(2,2);


                //...
            
        }

        public void timeExperiment(int pos,int proc)
        {

            timeRoot = directionFolder + "/TimeExperiment";
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            string auxRoot = "";
            StringBuilder cvsContent = new StringBuilder();
            string aux = "";
            if (proc == 1)
            {


                auxRoot = timeRoot + "/TimSort";
                if (!Directory.Exists(auxRoot))
                {
                    Directory.CreateDirectory(auxRoot);
                }
                for (int i = 1; i < EXPERIMENT; i++)
                {
                    Stopwatch sw = new Stopwatch();


                    
                    sw.Start();
                    timSort(ref dataArray, dataArray.Length);
                    sw.Stop();
                    transformInfoInArray(pos);
                    aux = "" + (sw.Elapsed.TotalMilliseconds * 1000000);
                    cvsContent.AppendLine(aux);

                }
            }
            else if (proc == 2)
            {
               
                auxRoot = timeRoot + "/MergeSort";
                if (!Directory.Exists(auxRoot))
                {
                    Directory.CreateDirectory(auxRoot);
                }
                for (int i = 1; i < EXPERIMENT; i++)
                {
                    Stopwatch sw = new Stopwatch();


                    long initTime = nanoTime();
                    //sw.Start();
                    mergeSort(ref dataArray, dataArray.Length,0);
                    //sw.Stop();
                    aux = "" + (nanoTime() - initTime);
                    Console.WriteLine(aux);
                    transformInfoInArray(pos);
                    
                    cvsContent.AppendLine(aux);

                }
            }
            string auxiliarDir = auxRoot + "/FileSize" + dataArray.Length + ".csv";
            File.AppendAllText(auxiliarDir, cvsContent.ToString());


        }
        private static long nanoTime()
        {
            long nano = 10000L * Stopwatch.GetTimestamp();
            nano /= TimeSpan.TicksPerMillisecond;
            nano *= 100L;
            return nano;
        }
        public void writeOrderArray(int al)
        {
            StringBuilder cvsContent = new StringBuilder();
            string aux = "";

            for (int i = 0; i < dataArray.Length - 1; i++)
            {
                if (i == 0)
                {
                    aux = "" + dataArray[i];
                }
                aux += "," + dataArray[i];
            }
            cvsContent.AppendLine(aux);
            if(al == 1)
            {
                string auxiliarDir = root + "/TimsortFileSize" +dataArray.Length  + ".csv";
                File.AppendAllText(auxiliarDir, cvsContent.ToString());
            }
            if (al == 2)
            {
                string auxiliarDir = root + "/MergeFileSize" + dataArray.Length + ".csv";
                File.AppendAllText(auxiliarDir, cvsContent.ToString());
            }


        }

        public int[] transformInfoInArray(int pos)
        {
            char[] aux = directions[pos].ToCharArray();
            string par = "";
            Boolean stop = false;
            for(int i = aux.Length-5; i> 0 && !stop; i--)
            {
                if ((int) aux[i] >= 48 && (int) aux[i] <= 57)
                {
                    par += aux[i];
                }
                else if((int) aux[i] == 101)
                {
                    stop = true;
                    char[] charArray = par.ToCharArray();
                    Array.Reverse(charArray);
                    par = new string(charArray);
                }
               

                
            }
           dataArray = null;
            string[] auxArray;
            string line = "";

            using (StreamReader file = new StreamReader(directions[pos]))
            {
                while ((line = file.ReadLine()) != null)                //Leer linea por linea
                {
                    auxArray = line.Split(',');
                    dataArray = new int[auxArray.Length];
                    for(int i = 0; i < auxArray.Length; i++)
                    {
                       dataArray[i] =  int.Parse(auxArray[i]);
                    }
                }
                }

                return dataArray;
        }

        public static void insertionSort(int[] arr, int left, int right)
        {
            for (int i = left + 1; i <= right; i++)
            {
                int temp = arr[i];
                int j = i - 1;
                while (j >= left &&  arr[j] > temp  )
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = temp;
            }
        }

        public static void merge(int[] arr, int l, int m, int r)
        {
            // original array is broken in two parts  
            // left and right array  
            int len1 = m - l + 1, len2 = r - m;
            int[] left = new int[len1];
            int[] right = new int[len2];
            for (int x = 0; x < len1; x++)
                left[x] = arr[l + x];
            for (int x = 0; x < len2; x++)
                right[x] = arr[m + 1 + x];

            int i = 0;
            int j = 0;
            int k = l;

            // after comparing, we merge those two array  
            // in larger sub array  
            while (i < len1 && j < len2)
            {
                if (left[i] <= right[j])
                {
                    arr[k] = left[i];
                    i++;
                }
                else
                {
                    arr[k] = right[j];
                    j++;
                }
                k++;
            }

            // copy remaining elements of left, if any  
            while (i < len1)
            {
                arr[k] = left[i];
                k++;
                i++;
            }

            // copy remaining element of right, if any  
            while (j < len2)
            {
                arr[k] = right[j];
                k++;
                j++;
            }
        }

        public static void timSort(ref int[] arr, int n)
        {
            // Sort individual subarrays of size RUN  
            for (int i = 0; i < n; i += RUN)
                insertionSort(arr, i, Math.Min((i + 31), (n - 1)));

            // start merging from size RUN (or 32). It will merge  
            // to form size 64, then 128, 256 and so on ....  
            for (int size = RUN; size < n; size = 2 * size)
            {
                // pick starting point of left sub array. We  
                // are going to merge arr[left..left+size-1]  
                // and arr[left+size, left+2*size-1]  
                // After every merge, we increase left by 2*size  
                for (int left = 0; left < n; left += 2 * size)
                {
                    // find ending point of left sub array  
                    // mid+1 is starting point of right sub array  
                    int mid = left + size - 1;
                    int right = Math.Min((left + 2 * size - 1), (n - 1));

                    // merge sub array arr[left.....mid] &  
                    // arr[mid+1....right]  
                    merge(arr, left, mid, right);
                }
            }
            


        }

        void mergeSort(ref int[] arr, int l, int r)
        {
            if (l < r)
            {
                // Same as (l+r)/2, but avoids overflow for 
                // large l and h 
                int m = l + (r - l) / 2;

                // Sort first and second halves 
                mergeSort(ref arr, l, m);
                mergeSort(ref arr, m + 1, r);

                merge(arr, l, m, r);
            }
        }
    }
}
