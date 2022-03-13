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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using WpfAppScheduler.Models.DB;
using WpfAppScheduler.Models;
using System.Collections.ObjectModel;

namespace WpfAppScheduler
{
    /// <summary>
    /// Logika interakcji dla klasy WaitingList.xaml
    /// </summary>
    public partial class WaitingList : Window
    {
        WaitingVisitsMenager waitingVisitsMenager = new WaitingVisitsMenager();
        List<WaitingVisit> items = new List<WaitingVisit>();
        
        public WaitingList()
        {
            InitializeComponent();
            onLoad();
        }

        private void onLoad()
        {

            ListViewWaiting.ItemsSource = null;
            ListViewWaiting.Items.Clear();

            items = waitingVisitsMenager.GetAllVisits();

            ListViewWaiting.ItemsSource = items;
            
        }
        
        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            WaitingVisit w = (WaitingVisit)ListViewWaiting.SelectedItem;
            int i = ListViewWaiting.SelectedIndex;
            
            if (i != -1)
            {
                AddVisit vWidnow = new AddVisit(w.Name, w.Phone, w.Email, w.Note, w.Data, w.Hour);
                vWidnow.ShowDialog();

                DeleteFromWaitingList();
            }
            else
            {
                MessageBox.Show("Wybierz pozycję z listy!");
            }
           

        }

        private void ButtonRefuse_Click(object sender, RoutedEventArgs e)
        {
            int i = ListViewWaiting.SelectedIndex;
            if (i != -1)
            {
                DeleteFromWaitingList();
            }
            else
            {
                MessageBox.Show("Wybierz pozycję z listy!");
            }
        }

        private void DeleteFromWaitingList()
        {
            int i = ListViewWaiting.SelectedIndex;
            waitingVisitsMenager.RemoveVisit(i);
            onLoad();
        }

       
    }
}


