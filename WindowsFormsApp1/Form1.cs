using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        String directionFolder;
       
        private Random random;
        private static int RUN = 32;
        private static int[] dataArray;
        private static int EXPERIMENT = 20;
        private static int sizeArrayOne = 1048576;
        private static int sizeArrayTwo = 65536;
        private static int sizeArrayThree = 131072;
        string root;
        string timeRoot;
        string[] directions;
        

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
            generateArrayInfo(sizeArrayOne, 0);
            generateArrayInfo(sizeArrayTwo, 1);
            generateArrayInfo(sizeArrayThree, 2);
        }

        public void generateArrayInfo(int size, int pos)
        {
            StringBuilder cvsContent = new StringBuilder();
            string aux = "";

            for (int i = 0; i < size - 1; i++)
            {

                aux = "" + random.Next(4000);

                cvsContent.AppendLine(aux);



            }

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
                mergeSort(ref dataArray, 0, dataArray.Length-1);
                writeOrderArray(2);
                transformInfoInArray(0);
                timeExperiment(0,2);

                transformInfoInArray(1);
               mergeSort(ref dataArray,0, dataArray.Length-1);
               writeOrderArray(2);
                transformInfoInArray(1);
                timeExperiment(1,2);

                transformInfoInArray(2);
                mergeSort(ref dataArray, 0,dataArray.Length-1);
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
                    aux = "" + (sw.Elapsed.TotalMilliseconds);
                    cvsContent.AppendLine(aux);

                }
            }
            else if (proc == 2)
            {
                string aux2 = "";
                auxRoot = timeRoot + "/MergeSort";
                if (!Directory.Exists(auxRoot))
                {
                    Directory.CreateDirectory(auxRoot);
                }
                for (int i = 1; i < EXPERIMENT; i++)
                {
                    Stopwatch sws = new Stopwatch();


                    
                    sws.Start();
                    mergeSort(ref dataArray, dataArray.Length, 0);
                   
                    

                    sws.Stop();
                    transformInfoInArray(pos);
                    aux = "" + (sws.Elapsed.TotalMilliseconds);
                    
                 
                    
                    cvsContent.AppendLine(aux);

                }
            }
            string auxiliarDir = auxRoot + "/FileSize" + dataArray.Length + ".csv";
            File.AppendAllText(auxiliarDir, cvsContent.ToString());


        }

        public void writeOrderArray(int al)
        {
            StringBuilder cvsContent = new StringBuilder();
            string aux = "";

            for (int i = 0; i < dataArray.Length - 1; i++)
            {

                aux = ""+dataArray[i];

                cvsContent.AppendLine(aux);
            }
            
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
            string line = "";

            using (StreamReader file = new StreamReader(directions[pos]))
            {
                int i = 0;
                if (pos == 0) dataArray = new int[sizeArrayOne];
                else if (pos == 1) dataArray = new int[sizeArrayTwo];
                else if (pos == 2) dataArray = new int[sizeArrayThree]; 

                while ((line = file.ReadLine()) != null)                //Leer linea por linea
                {

                    if (pos == 0)
                    {


                        dataArray[i] = int.Parse(line);
                        i++;
                    }
                    if (pos == 1)
                    {


                        dataArray[i] = int.Parse(line);
                        i++;
                    }
                    if (pos == 2)
                    {

                        dataArray[i] = int.Parse(line);
                        i++;
                    }

                }
            }

            return dataArray;
        }

        public static void insertionSort(ref int[] arr, int left, int right)
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
                insertionSort(ref arr, i, Math.Min((i + 31), (n - 1)));

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

        Boolean mergeSort(ref int[] arr, int l, int r)
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
            return true;
        }
    }
}
