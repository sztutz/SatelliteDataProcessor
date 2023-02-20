using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Galileo6;

namespace SatelliteDataProcessor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // 4.1 Create two data structures using the LinkedList<T> class from the C# Systems.Collections.Generic
        // namespace. The data must be of type “double”; you are not permitted to use any additional classes, nodes,
        // pointers or data structures (array, list, etc) in the implementation of this application. The two
        // LinkedLists of type double are to be declared as global within the “public partial class”.

        public LinkedList<Double> LLSensorA = new LinkedList<Double>();
        public LinkedList<Double> LLSensorB = new LinkedList<Double>();

        private Stopwatch stopwatch = new Stopwatch();
        private StreamWriter w = new StreamWriter("log.txt");

        public MainWindow()
        {
            InitializeComponent();
            PopulateComboBoxes();  
        }

        private void Log(string logMessage, TextWriter w)
        {
            w.WriteLine($"{logMessage}");
        }

        // 4.2 Copy the Galileo.DLL file into the root directory of your solution folder and add the appropriate
        // reference in the solution explorer. Create a method called “LoadData” which will populate both LinkedLists.
        // Declare an instance of the Galileo library in the method and create the appropriate loop construct to
        // populate the two LinkedList; the data from Sensor A will populate the first LinkedList, while the data from
        // Sensor B will populate the second LinkedList. The LinkedList size will be hardcoded inside the method and
        // must be equal to 400. The input parameters are empty, and the return type is void.

        private void LoadData()
        {
            // Galileo library instance declaration.
            ReadData galileo6 = new ReadData();
            Double.TryParse(ComboBoxSigma.Text, out double sigma);
            Double.TryParse(ComboBoxMu.Text,out double mu);
            int size = 400;
            // Clear previous sensor data.
            LLSensorA.Clear();
            LLSensorB.Clear();
            // Populate the linked lists.
            for (int i = 0; i < size; i++)
            {
                LLSensorA.AddLast(galileo6.SensorA(mu, sigma));
                LLSensorB.AddLast(galileo6.SensorB(mu, sigma));
            }
        }

        // 4.3 Create a custom method called “ShowAllSensorData” which will display both LinkedLists in a ListView. Add
        // column titles “Sensor A” and “Sensor B” to the ListView. The input parameters are empty, and the return type
        // is void.

        private void ShowAllSensorData()
        {
            // Clear the List View.
            ListViewSensorData.Items.Clear();
            // Populate the List View.
            for (int i = 0; i < 400; i++)
            {
                ListViewSensorData.Items.Add(new { GVCSensorA = LLSensorA.ElementAt(i).ToString(),
                    GVCSensorB = LLSensorB.ElementAt(i).ToString() });
            }
        }

        // 4.4	Create a button and associated click method that will call the LoadData and ShowAllSensorData methods.
        // The input parameters are empty, and the return type is void.
        private void ButtonLoadSensorData_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            ShowAllSensorData();
            DisplayListBoxData(LLSensorA, ListBoxSensorA);
            DisplayListBoxData(LLSensorB, ListBoxSensorB);
        }

        // 4.5 Create a method called “NumberOfNodes” that will return an integer which is the number of nodes(elements)
        // in a LinkedList.The method signature will have an input parameter of type LinkedList, and the calling code
        // argument is the linkedlist name.

        private int NumberOfNodes(LinkedList<Double> linkedList)
        {
            int numberOfNodes = linkedList.Count;
            return numberOfNodes;
        }

        // 4.6 Create a method called “DisplayListboxData” that will display the content of a LinkedList inside the
        // appropriate ListBox.The method signature will have two input parameters; a LinkedList, and the ListBox name.
        // The calling code argument is the linkedlist name and the listbox name.

        private void DisplayListBoxData(LinkedList<Double> linkedList, ListBox listBox)
        {
            // Clear the List Box.
            listBox.Items.Clear();
            // Populate the List Box.
            for (int i = 0; i < NumberOfNodes(linkedList); i++)
            {
                listBox.Items.Add(linkedList.ElementAt(i).ToString());
            }
        }

        // 4.7 Create a method called “SelectionSort” which has a single input parameter of type LinkedList, while the
        // calling code argument is the linkedlist name.The method code must follow the pseudo code supplied below in
        // the Appendix.The return type is Boolean.

        private bool SelectionSort(LinkedList<double> linkedList)
        {
            int min = 0;
            int max = NumberOfNodes(linkedList);
            for (int i = 0; i < max - 1; i++)
            {
                min = i;
                for (int j = i + 1; j < max; j++)
                {
                    if (linkedList.ElementAt(j) < linkedList.ElementAt(min))
                    {
                        min = j;
                    }
                }
                LinkedListNode<double> currentMin = linkedList.FindLast(linkedList.ElementAt(min));
                LinkedListNode<double> currentI = linkedList.Find(linkedList.ElementAt(i));
                var temp = currentMin.Value;
                currentMin.Value = currentI.Value;
                currentI.Value = temp;
            }
            //for (int i = 0; i < max - 2; i++)
            //{
            //    if (linkedList.ElementAt(i) > linkedList.ElementAt(i + 1))
            //    {
            //        Log("Linked List Contents:\n", w);
            //        for ( int l = 0; l < max; l++)
            //        {
            //            Log("Index: " + l + "   Value: " + linkedList.ElementAt(l), w);
            //        }
            //        Log("\nBeginning of sorting: ", w);
            //        for ( int j = 0; j < max - 1; j++)
            //        {
            //            min = j;
            //            Log("j index: " + j + " min: " + min, w);
            //            for (int k = j + 1; k < max; k++)
            //            {
            //                //Log("if " + linkedList.ElementAt(k) + " is greater than " + linkedList.ElementAt(min) + " ? ", w);
            //                if (linkedList.ElementAt(k) < linkedList.ElementAt(min))
            //                {
            //                    min = k;
            //                    Log("True,   new min: " + linkedList.ElementAt(k), w);
            //                }
            //            }
            //            LinkedListNode<double> currentMin = linkedList.Find(linkedList.ElementAt(min));
            //            LinkedListNode<double> currentJ = linkedList.Find(linkedList.ElementAt(j));
            //            Log("Current Min: " + currentMin.Value + " at index " + min, w);
            //            Log("Swaps with current j: " + currentJ.Value + " at index " + j + "\n", w);
            //            var temp = currentMin.Value;
            //            currentMin.Value = currentJ.Value;
            //            currentJ.Value = temp;
            //        }
            //        Log("Final linked list: ", w);
            //        for (int l = 0; l < max; l++)
            //        {
            //            Log("Index: " + l + "   Value: " + linkedList.ElementAt(l), w);
            //        }
            //        break;
            //    }
            ////}
            return true;
        }

        // Selection Sort pseudo code
        // integer min => 0
        // integer max => numberOfNodes(list)
        // for ( i = 0 to max )
        //     min => i
        //     for ( j = i + 1 to max )
        //         if (list element(j) < list element(min))
        //             min => j
        //     END for
        //     Supplied C# code
        //     LinkedListNode<double> currentMin = list.Find(list.ElementAt(min))
        //     LinkedListNode<double> currentI = list.Find(list.ElementAt(i))
        //     End of supplied c# code
        //     var temp = currentMin.Value
        //     currentMin.Value = currentI.Value
        //     currentI.Value = temp
        // END for

        // 4.8 Create a method called “InsertionSort” which has a single parameter of type LinkedList, while the
        // calling code argument is the linkedlist name. The method code must follow the pseudo code supplied below in
        // the Appendix. The return type is Boolean.

        private bool InsertionSort(LinkedList<double> linkedList)
        {
            int max = NumberOfNodes(linkedList);
            for (int i = 0; i < max - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (linkedList.ElementAt(j - 1) > linkedList.ElementAt(j))
                    {
                        LinkedListNode<double> current = linkedList.FindLast(linkedList.ElementAt(j));
                        LinkedListNode<double> currentPrevious = linkedList.Find(linkedList.ElementAt(j - 1));
                        var temp = currentPrevious.Value;
                        currentPrevious.Value = current.Value;
                        current.Value = temp;
                    }
                }
            }
            return true;
        }

        // Insertion Sort pseudo code
        // integer max = numberOfNodes(list)
        // for ( i = 0 to max - 1 )
        //     for ( j = i + 1 to j > 0, j-- )
        //         if (list element(j - 1) > list element(j))
        //             Supplied C# code
        //             LinkedListNode<double> current = list.Find(list.ElementAt(j))
        //             End of supplied C# code
        //             Add swap code here by swapping
        //             current previous value with current value.
        //         END if
        //     END for
        // END for

        // 4.9	Create a method called “BinarySearchIterative” which has the following four parameters: LinkedList,
        // SearchValue, Minimum and Maximum. This method will return an integer of the linkedlist element from a
        // successful search or the nearest neighbour value. The calling code argument is the linkedlist name, search
        // value, minimum list size and the number of nodes in the list. The method code must follow the pseudo code
        // supplied below in the Appendix.

        // 4.10	Create a method called “BinarySearchRecursive” which has the following four parameters: LinkedList,
        // SearchValue, Minimum and Maximum. This method will return an integer of the linkedlist element from a
        // successful search or the nearest neighbour value. The calling code argument is the linkedlist name, search
        // value, minimum list size and the number of nodes in the list. The method code must follow the pseudo code
        // supplied below in the Appendix.

        // 4.11	Create four button click methods that will search the LinkedList for an integer value entered into a
        // textbox on the form. The four methods are:
        // 1. Method for Sensor A and Binary Search Iterative
        // 2. Method for Sensor A and Binary Search Recursive
        // 3. Method for Sensor B and Binary Search Iterative
        // 4. Method for Sensor B and Binary Search Recursive
        // The search code must check to ensure the data is sorted, then start a stopwatch before calling the search
        // method.Once the search is complete the stopwatch will stop, and the number of ticks will be displayed in a
        // read only textbox.Finally, the code/method will call the “DisplayListboxData” method and highlight the
        // search target number and two values on each side.

        // 4.12	Create four button click methods that will sort the LinkedList using the Selection and Insertion
        // methods. The four methods are:
        // 1. Method for Sensor A and Selection Sort
        // 2. Method for Sensor A and Insertion Sort
        // 3. Method for Sensor B and Selection Sort
        // 4. Method for Sensor B and Insertion Sort
        // The button method must start a stopwatch before calling the sort method.Once the sort is complete the
        // stopwatch will stop, and the number of milliseconds will be displayed in a read only textbox.Finally, the
        // code/method will call the “ShowAllSensorData” method and “DisplayListboxData” for the appropriate sensor.

        private void ButtonSensorASelectionSort_Click(object sender, RoutedEventArgs e)
        {
            stopwatch.Restart();
            SelectionSort(LLSensorA);
            stopwatch.Stop();
            TextBoxSensorASelectionSortTime.Text = stopwatch.ElapsedMilliseconds.ToString() + " milliseconds";
            DisplayListBoxData(LLSensorA, ListBoxSensorA);
            CheckSort(LLSensorA);
        }

        private void ButtonSensorAInsertionSort_Click(object sender, RoutedEventArgs e)
        {
            stopwatch.Restart();
            InsertionSort(LLSensorA);
            stopwatch.Stop();
            TextBoxSensorAInsertionSortTime.Text = stopwatch.ElapsedMilliseconds.ToString() + " milliseconds";
            DisplayListBoxData(LLSensorA, ListBoxSensorA);
            CheckSort(LLSensorA);
        }
        private void ButtonSensorBSelectionSort_Click(object sender, RoutedEventArgs e)
        {
            stopwatch.Restart();
            SelectionSort(LLSensorB);
            stopwatch.Stop();
            TextBoxSensorBSelectionSortTime.Text = stopwatch.ElapsedMilliseconds.ToString() + " milliseconds";
            DisplayListBoxData(LLSensorB, ListBoxSensorB);
            CheckSort(LLSensorB);
        }

        private void ButtonSensorBInsertionSort_Click(object sender, RoutedEventArgs e)
        {
            stopwatch.Restart();
            InsertionSort(LLSensorB);
            stopwatch.Stop();
            TextBoxSensorBInsertionSortTime.Text = stopwatch.ElapsedMilliseconds.ToString() + " milliseconds";
            DisplayListBoxData(LLSensorB, ListBoxSensorB);
            CheckSort(LLSensorB);
        }

        // 4.13	Add two numeric input controls for Sigma and Mu. The value for Sigma must be limited with a minimum of
        // 10 and a maximum of 20. Set the default value to 10. The value for Mu must be limited with a minimum of 35
        // and a maximum of 75. Set the default value to 50.

        // 4.14	Add two textboxes for the search value; one for each sensor, ensure only numeric integer values can be
        // entered.

        // 4.15	All code is required to be adequately commented. Map the programming criteria and features to your
        // code/methods by adding comments/regions above the method signatures. Ensure your code is compliant with the
        // CITEMS coding standards (refer http://www.citems.com.au/).

        private void PopulateComboBoxes()
        {
            List<double> SigmaList = new List<double>();
            for (double i = 10.0; i <= 20.0; i++)
            {
                SigmaList.Add(i);
            }
            ComboBoxSigma.ItemsSource = SigmaList;
            ComboBoxSigma.SelectedItem = 10.0;
            List<double> MuList = new List<double>();
            for (double i = 35.0; i <= 70.0; i++)
            {
                MuList.Add(i);
            }
            ComboBoxMu.ItemsSource = MuList;
            ComboBoxMu.SelectedItem = 35.0;
        }

        private void CheckSort(LinkedList<double> linkedList)
        {
            bool sorted = false;
            for (int i = 0; i < 398; i++)
            {
                if (linkedList.ElementAt(i) > linkedList.ElementAt(i + 1))
                {
                    goto endSortedCheck;
                }
            }
            sorted = true;
            endSortedCheck:;
            Trace.WriteLine("\nSorted: " + sorted);
            for (int i = 0; i < 398; i++)
            {
                for (int j = i + 1; j < 399; j++) 
                {
                    if (linkedList.ElementAt(i) == linkedList.ElementAt(j))
                    {
                        Trace.WriteLine(String.Format("Duplicate found. Value {0} at index {1}, value {2} at index {3}", linkedList.ElementAt(i), i, linkedList.ElementAt(j), j));
                        goto endDuplicateCheck;
                    }
                }
            }
        Trace.WriteLine("No duplicate exists.");
        endDuplicateCheck:;
        }
    }
}
