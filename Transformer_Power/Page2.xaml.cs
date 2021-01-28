using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using System.Linq;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using LiveCharts.Helpers;
using LiveCharts.Geared;

namespace Transformer_Power
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
       
        public Page2()
        {
            InitializeComponent();
            ConvertData("0_norm_work.txt");
            DrawChart();
        }
        public void ConvertData(string filepath)
        {
            string readText = File.ReadAllText(filepath);
            List<string> listStrLineElements = readText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            listStrLineElements.RemoveAt(0);
            listStrLineElements.RemoveAt(0);
            H2_List.Clear(); CO_List.Clear(); C2H2_List.Clear(); C2H4_List.Clear();

            string pattern = @"-?\d+(?:\.\d+)?";
            Regex rgx = new Regex(pattern);

            for (int i = 0; i < listStrLineElements.Count(); i++)
            {
                string[] doubleData = new string[4];
                int j = 0;
                foreach (Match match in rgx.Matches(listStrLineElements.ElementAt(i)))
                    doubleData[j++] = match.Value;

                H2_List.Add(Convert.ToDouble(doubleData[0]));
                CO_List.Add(Convert.ToDouble(doubleData[1]));
                C2H4_List.Add(Convert.ToDouble(doubleData[2]));
                C2H2_List.Add(Convert.ToDouble(doubleData[3]));
            }


            List<string> rowList = listStrLineElements.SelectMany(s => s.Split("    ")).ToList();
            //string connetionString;
            //SqlConnection cnn;
            //connetionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Transformers;Integrated Security=True";
            //cnn = new SqlConnection(connetionString);

            //try
            //{
            //    cnn.Open();
            //    MessageBox.Show("Connection Open  !");
            //    for (int i = 0; i <= rowList.Count - 4; i += 4)//Implement by 3...
            //    {
            //        //Replace table_name with your table name, and Column1 with your column names (replace for all).
            //        SqlCommand myCommand = new SqlCommand("INSERT INTO table_name (Column1, Column2, Column3, Column4) " +
            //                             String.Format("Values ('{0}','{1}','{2}','{3}')", x[i], x[i + 1], x[i + 2], x[i + 3]), myConnection);
            //        myCommand.ExecuteNonQuery();
            //    }

            //}
            //catch (Exception e) { Console.WriteLine(e.ToString()); }
            //try { cnn.Close(); }
            //catch (Exception e) { Console.WriteLine(e.ToString()); }
        }
        public void DrawChart()
        {
            SeriesCollection = new SeriesCollection
            {
                new GLineSeries
                {
                    Title = "Series 1",
                    //Values = new ChartValues<double> { 4, 6, 5, 2 ,4 }
                     Values = H2_List.AsGearedValues().WithQuality(Quality.Low),
                    //Values = H2_List.AsChartValues(),
                    Fill = Brushes.Transparent,
                    StrokeThickness = .5,
                    PointGeometry = null
                },
                new GLineSeries
                {
                    Title = "Series 2",
                    //Values = new ChartValues<double> { 6, 7, 3, 4 ,6 },
                    //Values = CO_List.AsChartValues(),
                    Values = CO_List.AsGearedValues().WithQuality(Quality.Low),
                    Fill = Brushes.Transparent,
                    StrokeThickness = .5,
                    PointGeometry = null
                },
                new GLineSeries
                {
                    Title = "Series 3",
                    //Values = new ChartValues<double> { 4,2,7,2,7 },
                    //Values = C2H2_List.AsChartValues(),
                    Values = C2H2_List.AsGearedValues().WithQuality(Quality.Low),
                    Fill = Brushes.Transparent,
                    StrokeThickness = .5,
                    PointGeometry = null
                    //PointGeometry = DefaultGeometries.Square,
                    //PointGeometrySize = 15
                },
                new GLineSeries
                {
                    Title = "Series 4",
                    //Values = new ChartValues<double> { 4, 6, 5, 2 ,4 }
                    //Values = C2H4_List.AsChartValues(),
                    Values = C2H4_List.AsGearedValues().WithQuality(Quality.Low),
                    Fill = Brushes.Transparent,
                    StrokeThickness = .5,
                    PointGeometry = null
                }
            };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString("C");


            DataContext = this;
        }


        List<double> H2_List = new List<double>();
        List<double> CO_List = new List<double>();
        List<double> C2H4_List = new List<double>();
        List<double> C2H2_List = new List<double>();
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
    }
}
