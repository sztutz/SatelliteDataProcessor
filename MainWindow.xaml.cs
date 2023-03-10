using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Galileo6;
using System.Text.RegularExpressions;

// Stuart Anderson, 30056472, 27/02/2023
// Complex Data Structures, Assessment One
// A .NET Multi-platform App UI. Functionality includes Reading data from Galileo6 DLL into Linked Lists, sorts the
// data using selection sort and insertion sort, displays the data in List View and List Boxes, searches the data 
// using recursive binary search and iterative binary search.

namespace SatelliteDataProcessor
{
    public partial class MainWindow : Window
    {
        #region 4.1 Data Structures
        // 4.1 Create two data structures using the LinkedList<T> class from the C# Systems.Collections. Generic
        // namespace. The data must be of type “double”; you are not permitted to use any additional classes, nodes,
        // pointers or data structures (array, list, etc) in the implementation of this application. The two
        // LinkedLists of type double are to be declared as global within the “public partial class”.

        private LinkedList<Double> LLSensorA = new LinkedList<Double>();
        private LinkedList<Double> LLSensorB = new LinkedList<Double>();
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            // 4.13 Sigma and Mu Combo Box
            LoadComboBox(ComboBoxSigma, 10, 20, 10);
            LoadComboBox(ComboBoxMu, 35, 75, 50);
        }

        #region 4.2 Load Data
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
            Double.TryParse(ComboBoxSigma.Text, out Double sigma);
            Double.TryParse(ComboBoxMu.Text, out Double mu);
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
            // Clear previous search and sort times.
            TextBoxSensorAInsertionSortTime.Clear();
            TextBoxSensorASelectionSortTime.Clear();
            TextBoxSensorARecursiveSearchTime.Clear();
            TextBoxSensorAIterativeSearchTime.Clear();
            TextBoxSensorBInsertionSortTime.Clear();
            TextBoxSensorBSelectionSortTime.Clear();
            TextBoxSensorBRecursiveSearchTime.Clear();
            TextBoxSensorBIterativeSearchTime.Clear();
        }
        #endregion

        #region 4.3 Show All Sensor Data
        // 4.3 Create a custom method called “ShowAllSensorData” which will display both LinkedLists in a ListView. Add
        // column titles “Sensor A” and “Sensor B” to the ListView. The input parameters are empty, and the return type
        // is void.

        private void ShowAllSensorData()
        {
            // Clear the List View.
            ListViewSensorData.Items.Clear();
            // Populate the List View.
            for (int i = 0; i < NumberOfNodes(LLSensorA); i++)
            {
                ListViewSensorData.Items.Add(new { GVCSensorA = LLSensorA.ElementAt(i).ToString(),
                    GVCSensorB = LLSensorB.ElementAt(i).ToString() });
            }
        }
        #endregion

        #region 4.4 Button Load and Show
        // 4.4	Create a button and associated click method that will call the LoadData and ShowAllSensorData methods.
        // The input parameters are empty, and the return type is void.
        private void ButtonLoadSensorData_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            ShowAllSensorData();
            DisplayListBoxData(LLSensorA, ListBoxSensorA);
            DisplayListBoxData(LLSensorB, ListBoxSensorB);
        }
        #endregion

        #region 4.5 Number of Nodes
        // 4.5 Create a method called “NumberOfNodes” that will return an integer which is the number of nodes(elements)
        // in a LinkedList.The method signature will have an input parameter of type LinkedList, and the calling code
        // argument is the linkedlist name.

        private int NumberOfNodes(LinkedList<Double> linkedList)
        {
            int numberOfNodes = linkedList.Count;
            return numberOfNodes;
        }
        #endregion

        #region 4.6 Display List Box Data
        // 4.6 Create a method called “DisplayListBoxData” that will display the content of a LinkedList inside the
        // appropriate ListBox. The method signature will have two input parameters; a LinkedList, and the ListBox
        // name. The calling code argument is the linkedlist name and the listbox name.

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
        #endregion

        #region 4.7 Selection Sort
        // 4.7 Create a method called “SelectionSort” which has a single input parameter of type LinkedList, while the
        // calling code argument is the linkedlist name. The method code must follow the pseudo code supplied below in
        // the Appendix.The return type is Boolean.

        private bool SelectionSort(LinkedList<Double> linkedList)
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
                LinkedListNode<Double> currentMin = linkedList.Find(linkedList.ElementAt(min));
                LinkedListNode<Double> currentI = linkedList.Find(linkedList.ElementAt(i));
                var temp = currentMin.Value;
                currentMin.Value = currentI.Value;
                currentI.Value = temp;
            }
            return true;
        }
        #endregion

        #region 4.8 InsertionSort
        // 4.8 Create a method called “InsertionSort” which has a single parameter of type LinkedList, while the
        // calling code argument is the linkedlist name. The method code must follow the pseudo code supplied below in
        // the Appendix. The return type is Boolean.

        private bool InsertionSort(LinkedList<Double> linkedList)
        {
            int max = NumberOfNodes(linkedList);
            for (int i = 0; i < max - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (linkedList.ElementAt(j - 1) > linkedList.ElementAt(j))
                    {
                        LinkedListNode<Double> current = linkedList.Find(linkedList.ElementAt(j));
                        LinkedListNode<Double> currentPrevious = linkedList.Find(linkedList.ElementAt(j - 1));
                        var temp = currentPrevious.Value;
                        currentPrevious.Value = current.Value;
                        current.Value = temp;
                    }
                }
            }
            return true;
        }
        #endregion

        #region 4.9 Binary Search Iterative
        // 4.9	Create a method called “BinarySearchIterative” which has the following four parameters: LinkedList,
        // SearchValue, Minimum and Maximum. This method will return an integer of the linkedlist element from a
        // successful search or the nearest neighbour value. The calling code argument is the linkedlist name, search
        // value, minimum list size and the number of nodes in the list. The method code must follow the pseudo code
        // supplied below in the Appendix.

        private int BinarySearchIterative(LinkedList<Double> linkedList, int searchValue, int minimum, int maximum)
        {
            while (minimum <= maximum - 1)
            {
                int middle = (minimum + maximum) / 2;
                if (searchValue == linkedList.ElementAt(middle))
                {
                    return ++middle;
                }
                else if (searchValue < linkedList.ElementAt(middle))
                {
                    maximum = middle - 1;
                }
                else
                {
                    minimum = middle + 1;
                }
            }
            return minimum;
        }
        #endregion

        #region 4.10 Binary Search Recursive
        // 4.10	Create a method called “BinarySearchRecursive” which has the following four parameters: LinkedList,
        // SearchValue, Minimum and Maximum. This method will return an integer of the linkedlist element from a
        // successful search or the nearest neighbour value. The calling code argument is the linkedlist name, search
        // value, minimum list size and the number of nodes in the list. The method code must follow the pseudo code
        // supplied below in the Appendix.

        private int BinarySearchRecursive(LinkedList<Double> linkedList, int searchValue, int minimum, int maximum)
        {
            if (minimum <= maximum - 1)
            {
                int middle = (minimum + maximum) / 2;
                if (searchValue == linkedList.ElementAt(middle))
                {
                    return middle;
                }
                else if (searchValue < linkedList.ElementAt(middle))
                {
                    return BinarySearchRecursive(linkedList, searchValue, minimum, middle - 1);
                }
                else
                {
                    return BinarySearchRecursive(linkedList, searchValue, middle + 1, maximum);
                }
            }
            return minimum;
        }
        #endregion

        #region 4.11 Buttons Search
        // 4.11	Create four button click methods that will search the LinkedList for an integer value entered into a
        // textbox on the form. The four methods are:
        // 1. Method for Sensor A and Binary Search Iterative
        // 2. Method for Sensor A and Binary Search Recursive
        // 3. Method for Sensor B and Binary Search Iterative
        // 4. Method for Sensor B and Binary Search Recursive
        // The search code must check to ensure the data is sorted, then start a stopwatch before calling the search
        // method. Once the search is complete the stopwatch will stop, and the number of ticks will be displayed in a
        // read only textbox. Finally, the code/method will call the “DisplayListboxData” method and highlight the
        // search target number and two values on each side.

        private void ButtonSensorAIterativeSearch_Click(object sender, RoutedEventArgs e)
        {
            Search(LLSensorA, TextBoxSensorASearchTarget, BinarySearchIterative, TextBoxSensorAIterativeSearchTime, ListBoxSensorA);
        }
        private void ButtonSensorARecursiveSearch_Click(object sender, RoutedEventArgs e)
        {
            Search(LLSensorA, TextBoxSensorASearchTarget, BinarySearchRecursive, TextBoxSensorARecursiveSearchTime, ListBoxSensorA);
        }

        private void ButtonSensorBIterativeSearch_Click(object sender, RoutedEventArgs e)
        {
            Search(LLSensorB, TextBoxSensorBSearchTarget, BinarySearchIterative, TextBoxSensorBIterativeSearchTime, ListBoxSensorB);
        }

        private void ButtonSensorBRecursiveSearch_Click(object sender, RoutedEventArgs e)
        {
            Search(LLSensorB, TextBoxSensorBSearchTarget, BinarySearchRecursive, TextBoxSensorBRecursiveSearchTime, ListBoxSensorB);
        }

        // The reason for this method is to remove code repetition from the search button click methods.
        // The paramaters:
        //      linkedList is the linkedList<Double> that is being searched,
        //      searchBox is the TextBox that has the target value,
        //      searchMethod is the method being used to perform the search, either BinarySearchIterative or BinarySearchRecursive,
        //      timeBox is the TextBox which will show the time taken to perform the search,
        //      listBox is the ListBox which will shows the sensor data with the highlighted elements.
        private void Search(LinkedList<Double> linkedList, TextBox searchBox, Func<LinkedList<Double>, int, int, int, int> searchMethod, TextBox timeBox, ListBox listBox)
        {
            // Checking that the linked list contains data
            // Checking that the search box contains a search value
            // Checking that the linked list has been sorted
            if (NumberOfNodes(linkedList) > 0 && !String.IsNullOrEmpty(searchBox.Text) && InsertionSort(linkedList))
            {
                int targetValue = Int32.Parse(searchBox.Text);
                Stopwatch stopwatch = Stopwatch.StartNew();
                int targetIndex = searchMethod(linkedList, targetValue, 0, NumberOfNodes(linkedList));
                stopwatch.Stop();
                timeBox.Text = stopwatch.ElapsedTicks.ToString() + " ticks";
                DisplayListBoxData(linkedList, listBox);
                Highlight(targetIndex, listBox);
            }
        }

        // The Highlight method calls the InBounds method to validate the index.
        private void Highlight(int targetIndex, ListBox listBox)
        {
            // The sort method does not allow node at index 399 to be selected.
            if (targetIndex == 400) { targetIndex = 399; }
            // Looping through the 2 nodes below and above the target index.
            for (int i = targetIndex - 2; i <= targetIndex + 2; i++)
            {
                if (InBounds(i, listBox.Items.Count))
                {
                    listBox.SelectedItems.Add(listBox.Items.GetItemAt(i));
                    listBox.ScrollIntoView(listBox.Items.GetItemAt(i));
                }
            }
        }

        // Checking if the index is in the bounds of the linked list.
        private bool InBounds(int index, int max)
        {
            if (index < 0 || index >= max)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 4.12 Buttons Sort
        // 4.12	Create four button click methods that will sort the LinkedList using the Selection and Insertion
        // methods. The four methods are:
        // 1. Method for Sensor A and Selection Sort
        // 2. Method for Sensor A and Insertion Sort
        // 3. Method for Sensor B and Selection Sort
        // 4. Method for Sensor B and Insertion Sort
        // The button method must start a stopwatch before calling the sort method.Once the sort is complete the
        // stopwatch will stop, and the number of milliseconds will be displayed in a read only textbox. Finally, the
        // code/method will call the “ShowAllSensorData” method and “DisplayListboxData” for the appropriate sensor.

        private void ButtonSensorASelectionSort_Click(object sender, RoutedEventArgs e)
        {
            Sort(SelectionSort, LLSensorA, TextBoxSensorASelectionSortTime, ListBoxSensorA);
        }

        private void ButtonSensorAInsertionSort_Click(object sender, RoutedEventArgs e)
        {
            Sort(InsertionSort, LLSensorA, TextBoxSensorAInsertionSortTime, ListBoxSensorA);
        }
        private void ButtonSensorBSelectionSort_Click(object sender, RoutedEventArgs e)
        {
            Sort(SelectionSort, LLSensorB, TextBoxSensorBSelectionSortTime, ListBoxSensorB);
        }

        private void ButtonSensorBInsertionSort_Click(object sender, RoutedEventArgs e)
        {
            Sort(InsertionSort, LLSensorB, TextBoxSensorBInsertionSortTime, ListBoxSensorB);
        }

        // The reason for this method is to remove code repetition from the sort button click methods
        // The parameters:
        //      sortMethod is the method being used to perform the sort, either InsertionSort or SelectionSort,
        //      linkedList is the linkedList<Double> that is being sorted,
        //      timeBox is the TextBox which will show the time taken to perform the sort,
        //      listBox is the ListBox which will shows the sorted sensor data.
        private void Sort(Func<LinkedList<Double>, bool> sortMethod, LinkedList<Double> linkedList, TextBox timeBox, ListBox listBox)
        {
            // Checking that the linked list contains data
            if (NumberOfNodes(linkedList) > 0)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                sortMethod(linkedList);
                stopwatch.Stop();
                timeBox.Text = stopwatch.ElapsedMilliseconds.ToString() + " milliseconds";
                DisplayListBoxData(linkedList, listBox);
            }
        }
        #endregion

        #region 4.13 Sigma and Mu
        // 4.13	Add two numeric input controls for Sigma and Mu. The value for Sigma must be limited with a minimum of
        // 10 and a maximum of 20. Set the default value to 10. The value for Mu must be limited with a minimum of 35
        // and a maximum of 75. Set the default value to 50.

        // See method MainWindow().
        private void LoadComboBox(ComboBox comboBox, int min, int max, int defaultValue)
        {
            for (int i = min; i <= max; i++)
            {
                comboBox.Items.Add(i);
            }
            comboBox.SelectedValue = defaultValue;
        }
        #endregion

        #region 4.14 Search Value
        // 4.14	Add two textboxes for the search value; one for each sensor, ensure only numeric integer values can be
        // entered.

        private void IntegerValidation(object sender, TextCompositionEventArgs e)
        {
            // Regex [^0-9]+ : any character other than 0-9 one or more times.
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]");
        }
        #endregion

        // 4.15	All code is required to be adequately commented. Map the programming criteria and features to your
        // code/methods by adding comments/regions above the method signatures. Ensure your code is compliant with the
        // CITEMS coding standards (refer http://www.citems.com.au/).
    }
}
