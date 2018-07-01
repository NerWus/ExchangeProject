using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Globalization;
using System.IO.Compression;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms.DataVisualization;
using OxyPlot;
using OxyPlot.Series;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private const string EXTRACT_PATH = @"D:\Projekt chasz\WpfApplication3\wypakowane\";

        public MainWindow()
        {
            InitializeComponent();
           LinkTextbox.Background = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)); 
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
           // DownloadAndExtract();
            
            var filename = Directory.GetFiles(EXTRACT_PATH);
            string xx;
            TreeViewItem rootnode = new TreeViewItem() { Header = "głowny" };
            treeView1.Items.Add(rootnode);
            foreach (string name in filename)
            {
                var item = new TreeViewItem();
                xx = System.IO.Path.GetFileName(name);
                rootnode.Items.Add(xx);
                item.Header = System.IO.Path.GetFileName(name);
                item.Selected += Item_Selected;
                treeView1.Items.Add(item);

            }
            LinkTextbox.Background = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
        }

        private void Item_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            var rowList = ParseFile((string)item.Header);
            dataGrid.ItemsSource = rowList;

            var listax = new List<DataPoint>();
            for (int i = 0; i < rowList.Count; i++)
            {
                var pojelezlisty = rowList[i].OPEN;
                var tmpDataPoint = new DataPoint(i, pojelezlisty);
                //date time jako X parsowane 
                //

                listax.Add(tmpDataPoint);  
            }

                PlotPoints.ItemsSource = listax; 
        }

        private List<FileRow> ParseFile(string fileName)
        {
            //string[] fileLines = File.ReadAllLines(EXTRACT_PATH + "\\" + fileName);
            var fileLines = File.ReadAllLines(EXTRACT_PATH + "\\" + fileName);
            List<FileRow> parsedRows = new List<FileRow>();
            for (int i = 1; i < fileLines.Length; i++)
            {
                var splitLine = fileLines[i].Split(',');
                // var splitLine = fileLines[i].Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries;
                var tmpFileRow = new FileRow();
                tmpFileRow.TICKER = splitLine[0];
                tmpFileRow.DTYYYYMMDD = splitLine[1];
                tmpFileRow.OPEN = double.Parse(splitLine[2], NumberStyles.Number, CultureInfo.InvariantCulture);
                tmpFileRow.HIGH = double.Parse(splitLine[3], NumberStyles.Number, CultureInfo.InvariantCulture);
                tmpFileRow.LOW = double.Parse(splitLine[4], NumberStyles.Number, CultureInfo.InvariantCulture);
                tmpFileRow.CLOSE = double.Parse(splitLine[5], NumberStyles.Number, CultureInfo.InvariantCulture);
                tmpFileRow.VOL = int.Parse(splitLine[6]);

                parsedRows.Add(tmpFileRow);

            }
            return parsedRows;
        }

        private void DownloadAndExtract()
        {
            LinkTextbox.Background = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
            string zipPath = EXTRACT_PATH + ".zip";

            using (WebClient client = new WebClient())
            {
                
                //client.Dispose
                client.DownloadFile(LinkTextbox.Text,
                                    zipPath);
            }


            string extractPath = EXTRACT_PATH;
            if (Directory.Exists(extractPath))
            {
                Directory.Delete(extractPath, true);
            }

            ZipFile.ExtractToDirectory(zipPath, extractPath);

        }
       




        //http://bossa.pl/pub/metastock/mstock/mstall.zip


    }
}