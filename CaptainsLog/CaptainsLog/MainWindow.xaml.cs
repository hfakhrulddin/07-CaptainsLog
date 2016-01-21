using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CaptainsLog.Core;
using System.Collections.ObjectModel;
//using Newtonsoft.Json;
//using JsonParser;
using LitJson;
using System.IO;
using Newtonsoft.Json;
namespace CaptainsLog
{
    public partial class MainWindow : Window
    {
        // ObservableCollection<LogEntery> logEnteries = new ObservableCollection<LogEntery>();
        ObservableCollection<LogEntery> logEnteries;
        int i;
        public MainWindow()
        {
            logEnteries = new ObservableCollection<LogEntery>();
            InitializeComponent();
            if (File.Exists("C:\\Div\\data\\unsorted-numbers.txt"))
            {
                string JsonListread = File.ReadAllText("C:\\Div\\data\\unsorted-numbers.txt");
                if (JsonListread != "")
                {
                    ObservableCollection<LogEntery> loaded = JsonConvert.DeserializeObject<ObservableCollection<LogEntery>>(JsonListread);
                    logEnteries = loaded;
                }
            }
                dataGrid.ItemsSource = logEnteries;
            i = logEnteries.Count + 1;
        }
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            LogEntery log = new LogEntery();
                log.ID = i;
                i += 1;    
            log.Date_Time = DateTime.Now;
            log.Description = txb1.Text;
            try {
                logEnteries.Add(log);
                JsonData JsonList;
                JsonList = JsonMapper.ToJson(logEnteries);
                File.WriteAllText("C:\\Div\\data\\unsorted-numbers.txt", JsonList.ToString());
                //string json = JsonConvert.SerializeObject(logEnteries);
            }
            catch (Exception ex) {
                MessageBox.Show("You have a problem " + ex.Message);
            }
        }
        private void removeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex >= 0)
            {
                logEnteries.RemoveAt(dataGrid.SelectedIndex);
                JsonData JsonList;
                JsonList = JsonMapper.ToJson(logEnteries);
                File.WriteAllText("C:\\Div\\data\\unsorted-numbers.txt", JsonList.ToString());
            }
            else
            {
                MessageBox.Show("You have to select an item first");
            }
        }
    }
}
