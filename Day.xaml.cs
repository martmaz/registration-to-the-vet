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
using WpfAppScheduler.Models;
using WpfAppScheduler.Models.Info;

namespace WpfAppScheduler
{
    /// <summary>
    /// Logika interakcji dla klasy Day.xaml
    /// </summary>
    public partial class Day : Window
    {
        static DateTime _dt = DateTime.Now;
        DateTime calDt = _dt;

        VisitsMenager visitsMenager = new VisitsMenager();

        public Day()
        {
            InitializeComponent();
            onLoaded();
            showVisitsForThisDay();
        }

        private void onLoaded()
        {
            dayDate.Content = "Date: " + calDt;
        }

        private void showVisitsForThisDay()
        {
            List<VisitInfo> allVisitsForDay = new List<VisitInfo>();
            allVisitsForDay = visitsMenager.GetVisitsForDay(calDt);

        }

        private void prevBtn_Click(object sender, RoutedEventArgs e)
        {
            calDt.AddDays(-1);
            dayDate.Content = "Date: " + calDt;
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            calDt.AddDays(1);
            dayDate.Content = "Date: " + calDt;
        }
    }
}
