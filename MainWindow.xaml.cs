﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Dynamic;
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
        public MainWindow()
        {
            InitializeComponent();
            PopulateComboBoxes();
            LoadData();
            ShowAllSensorData();
        }

        // 4.2 Copy the Galileo.DLL file into the root directory of your solution folder and add the appropriate
        // reference in the solution explorer. Create a method called “LoadData” which will populate both LinkedLists.
        // Declare an instance of the Galileo library in the method and create the appropriate loop construct to
        // populate the two LinkedList; the data from Sensor A will populate the first LinkedList, while the data from
        // Sensor B will populate the second LinkedList. The LinkedList size will be hardcoded inside the method and
        // must be equal to 400. The input parameters are empty, and the return type is void.

        private void LoadData()
        {
            ReadData galileo6 = new ReadData();
            int sigma = (int)ComboBoxSigma.SelectedValue;
            int mu = (int)ComboBoxMu.SelectedValue;
            int size = 400;
            for (int i = 0; i < size; i++)
            {
                LLSensorA.AddLast(galileo6.SensorA(mu, sigma));
                LLSensorB.AddLast(galileo6.SensorB(mu, sigma));

                // Shows all sensor data.
                Trace.WriteLine("SensorA: " + LLSensorA.ElementAt(i) + "   SensorB: " + LLSensorB.ElementAt(i));
            }
            //Trace.WriteLine("sigma: " + sigma.ToString() + "\nmu: " + mu.ToString());
        }

        // 4.3 Create a custom method called “ShowAllSensorData” which will display both LinkedLists in a ListView. Add
        // column titles “Sensor A” and “Sensor B” to the ListView. The input parameters are empty, and the return type
        // is void.

        private void ShowAllSensorData()
        {
            //GVCSensorA.DisplayMemberBinding = new Binding(LLSensorA.ToString());
            //GVCSensorB.DisplayMemberBinding = new Binding(LLSensorB.ToString());

            //ListViewSensorData.ItemsSource = LLSensorA;
            
            //GVCSensorA.DisplayMemberBinding = new Binding(LLSensorA.ToString());

            for (int i = 0; i < 400; i++)
            {
                //ListViewItem item = new ListViewItem(LLSensorA());
                //ListViewSensorData.Items.Add(LLSensorA.ElementAt(i).ToString());
                //ListViewItem item = new ListViewItem(LLSensorA.ElementAt(i));
                //ListViewSensorData.Items.Add(new string { LLSensorA.ElementAt(i), LLSensorB.ElementAt(i) });

                // Shows the whole row string in both grid views
                //var row = new { LLSensorA = LLSensorA.ElementAt(i).ToString(), LLSensorB = LLSensorB.ElementAt(i).ToString() };
                //ListViewSensorData.Items.Add(row);
                ListViewSensorData.Items.Add(new { LLSensorA = LLSensorA.ElementAt(i).ToString(), LLSensorB = LLSensorB.ElementAt(i).ToString() });

                //ListViewSensorData.Items.Add( { LLSensorA.ElementAt(i).ToString(), LLSensorB.ElementAt(i).ToString()});
                //ListViewSensorData.Items.Add(LLSensorB.ElementAt(i));



                //ListViewSensorData.Items.Add(LLSensorA.ElementAt(i).ToString());
                //ListViewSensorData.Items.Add(LLSensorB.ElementAt(i).ToString());
                //ListViewSensorData.ItemsSource = LLSensorA;
                //ListViewItem sensorA = new ListViewItem();
                //ListViewItem sensorB = new ListViewItem();
                //sensorA = LLSensorA.ElementAt(i).ToString();
                //ListViewItem item = new ListViewItem();
                //ListViewItem item = new ListViewItem("item");
            }
        }

        // 4.4	Create a button and associated click method that will call the LoadData and ShowAllSensorData methods.
        // The input parameters are empty, and the return type is void.
        private void ButtonLoadSensorData_Click(object sender, RoutedEventArgs e)
        {

        }

        // 4.5 Create a method called “NumberOfNodes” that will return an integer which is the number of nodes(elements)
        // in a LinkedList.The method signature will have an input parameter of type LinkedList, and the calling code
        // argument is the linkedlist name.

        // 4.6 Create a method called “DisplayListboxData” that will display the content of a LinkedList inside the
        // appropriate ListBox.The method signature will have two input parameters; a LinkedList, and the ListBox name.
        // The calling code argument is the linkedlist name and the listbox name.

        // 4.7 Create a method called “SelectionSort” which has a single input parameter of type LinkedList, while the
        // calling code argument is the linkedlist name.The method code must follow the pseudo code supplied below in
        // the Appendix.The return type is Boolean.

        // 4.8 Create a method called “InsertionSort” which has a single parameter of type LinkedList, while the
        // calling code argument is the linkedlist name. The method code must follow the pseudo code supplied below in
        // the Appendix. The return type is Boolean.

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
            List<int> SigmaList = new List<int>();
            for (int i = 10; i <= 20; i++)
            {
                SigmaList.Add(i);
            }
            ComboBoxSigma.ItemsSource = SigmaList;
            ComboBoxSigma.SelectedItem = 10;
            List<int> MuList = new List<int>();
            for (int i = 35; i <= 70; i++)
            {
                MuList.Add(i);
            }
            ComboBoxMu.ItemsSource = MuList;
            ComboBoxMu.SelectedItem = 35;
        }
    }
}
