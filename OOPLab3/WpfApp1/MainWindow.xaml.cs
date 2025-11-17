using System;
using System.Linq;
using System.Windows;

namespace Lab3_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(InputN.Text, out int n) || n <= 0)
            {
                MessageBox.Show("Введіть коректне число n!");
                return;
            }

            double[] arr = new double[n];
            Random rnd = new Random();
            double minVal = -7.51;
            double maxVal = 3.59;

            for (int i = 0; i < n; i++)
            {
                double val = minVal + rnd.NextDouble() * (maxVal - minVal);
                arr[i] = Math.Round(val, 2);
            }

            OriginalArrayOutput.Text = string.Join(";  ", arr);

            double sumModules = 0;
            foreach (var item in arr)
            {
                if ((Math.Abs(item) % 1) < 0.5)
                {
                    sumModules += Math.Abs(item);
                }
            }
            ResultSumOutput.Text = Math.Round(sumModules, 2).ToString();

            double currentMin = arr[0];
            int minIndex = 0;
            for (int i = 1; i < n; i++)
            {
                if (arr[i] < currentMin)
                {
                    currentMin = arr[i];
                    minIndex = i;
                }
            }

            if (minIndex < n - 1)
            {
                int lengthToSort = n - (minIndex + 1);
                double[] tempArr = new double[lengthToSort];
                Array.Copy(arr, minIndex + 1, tempArr, 0, lengthToSort);

                Array.Sort(tempArr);
                Array.Reverse(tempArr); 

                Array.Copy(tempArr, 0, arr, minIndex + 1, lengthToSort);
            }

            SortedArrayOutput.Text = string.Join(";  ", arr);
        }
    }
}