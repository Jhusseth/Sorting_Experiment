using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.modelo
{
   public class PrincipalProcess
    {
        /**This static attribute determines the division into groups within the timsort sorting method.
         * Therefore, this will determine the size of the arrays to evaluate, example: If the value of RUN is 32, 
         * the arrays must have a size whose value is divisible by 32 and 2.
         */

        private static int RUN = 32;

        /*Representation of the fixed size of the first arrangement, whose value is divisible by 32 and 2.
         */

        private static int sizeArrayOne = 256;

        /*Representation of the fixed size of the second arrangement, whose value is divisible by 32 and 2.
        */

        private static int sizeArrayTwo = 16384;

        /*Representation of the fixed size of the third arrangement, whose value is divisible by 32 and 2.
        */

        private static int sizeArrayThree = 131072;

        /*This value represents the number of times the sorting methods will be repeated.
         *Therefore it symbolizes the repetitions within the experiment.
         */

        private static int EXPERIMENT = 1000;

        /*This attribute symbolizes the arrangement currently being evaluated.
         */

        private static int[] dataArray;

        /*String attribute representing the address of the folder where the experiment values are saved.
         */

        String directionFolder;

        /*Represents the address of the order folder.
         */

        string root;

        /*Represents the address of the time experiment folder.
         */

        string timeRoot;

        /*Represents an array of address where the records of the different arrangements will be created.
         */

        string[] directions;

        /*Represents the random attribute that will be used in the randomization of the process.
         */

        private Random random;


        public PrincipalProcess()
        {

        }

        /*This method allows the user in the first instance to choose where the results of the experiment will be saved.
         * He will then call the method that creates the random arrangements.
         */

        public void generateData()
        {
            FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();





            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                directionFolder = openFileDialog1.SelectedPath;

                random = new Random();
                directions = new string[3];
                generateArrayInfo(sizeArrayOne, 0);
                generateArrayInfo(sizeArrayTwo, 1);
                generateArrayInfo(sizeArrayThree, 2);
               
            }

        }

        /*Method that creates arrays with random values.
         */
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

        /*This method is responsible for searching the files where the random values are stored 
         *and converts them into an array of integers.
         */

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

        /*This method creates a folder within the path chosen by the user to save the results. 
         * This will be called time experiment. Then StopWatch is used to take the time of the sorting methods,
         * whose values will be saved in a csv file.
         */

        public void timeExperiment(int pos, int proc)
        {

            timeRoot = directionFolder + "/TimeExperiment";
            if (!Directory.Exists(timeRoot))
            {
                Directory.CreateDirectory(timeRoot);
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
                for (int i = 0; i <= EXPERIMENT; i++)
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
               
                auxRoot = timeRoot + "/MergeSort";
                if (!Directory.Exists(auxRoot))
                {
                    Directory.CreateDirectory(auxRoot);
                }
                for (int i = 0; i <= EXPERIMENT; i++)
                {
                    Stopwatch sws = new Stopwatch();



                    sws.Start();
                    mergeSort(ref dataArray, 0, dataArray.Length - 1);



                    sws.Stop();
                    transformInfoInArray(pos);
                    aux = "" + (sws.Elapsed.TotalMilliseconds);



                    cvsContent.AppendLine(aux);

                }
            }
            string auxiliarDir = auxRoot + "/FileSize" + dataArray.Length + ".csv";
            File.AppendAllText(auxiliarDir, cvsContent.ToString());


        }

        /*Method that writes the organized arrangement to a file.
         */

        public void writeOrderArray(int al)
        {
            StringBuilder cvsContent = new StringBuilder();
            string aux = "";

            for (int i = 0; i < dataArray.Length - 1; i++)
            {

                aux = "" + dataArray[i];

                cvsContent.AppendLine(aux);
            }

            if (al == 1)
            {
                string auxiliarDir = root + "/TimsortFileSize" + dataArray.Length + ".csv";
                File.AppendAllText(auxiliarDir, cvsContent.ToString());
            }
            if (al == 2)
            {
                string auxiliarDir = root + "/MergeFileSize" + dataArray.Length + ".csv";
                File.AppendAllText(auxiliarDir, cvsContent.ToString());
            }


        }

        /*Sorting method that uses the insertionSort and merge methods to sort an array.
         */

        public void timSort(ref int[] arr, int n)
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

        /*Sorting method that consists in comparing the following value with all the values of the arrangement. 
         * Depending on the comparison criteria, the other values will be shifted if this is met.
         */

        public void insertionSort(ref int[] arr, int left, int right)
        {
            for (int i = left + 1; i <= right; i++)
            {
                int temp = arr[i];
                int j = i - 1;
                while (j >= left && arr[j] > temp)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = temp;
            }
        }

        /*Recursive method that compares previously divided arrangements.
         */

        public void merge(int[] arr, int l, int m, int r)
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

        /*Recursive method that divides the arrangements into analysis into two parts. 
         *Then you will make the comparison within the merge method.
         */

        public Boolean mergeSort(ref int[] arr, int l, int r)
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

        /*Within this method a call is made to all the necessary methods within the experiment. 
         *First, it will transform the generated values into arrangements that you will use later to sort them with the methods. 
         *Finally, he calls the experiment time method to take the data.
         */

        public void experimentEvent()
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
            timeExperiment(0, 1);

            transformInfoInArray(1);
            timSort(ref dataArray, dataArray.Length);
            writeOrderArray(1);
            transformInfoInArray(1);
            timeExperiment(1, 1);

            transformInfoInArray(2);
            timSort(ref dataArray, dataArray.Length);
            writeOrderArray(1);
            transformInfoInArray(2);
            timeExperiment(2, 1);

            transformInfoInArray(0);
            mergeSort(ref dataArray, 0, dataArray.Length - 1);
            writeOrderArray(2);
            transformInfoInArray(0);
            timeExperiment(0, 2);

            transformInfoInArray(1);
            mergeSort(ref dataArray, 0, dataArray.Length - 1);
            writeOrderArray(2);
            transformInfoInArray(1);
            timeExperiment(1, 2);

            transformInfoInArray(2);
            mergeSort(ref dataArray, 0, dataArray.Length - 1);
            writeOrderArray(2);
            transformInfoInArray(2);
            timeExperiment(2, 2);

            System.Diagnostics.Process.Start(directionFolder);
        }

    }
}
