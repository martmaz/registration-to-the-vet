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
using WpfAppScheduler.Models.DB;
using WpfAppScheduler.Models;
using WpfAppScheduler.Models.Info;
using System.ComponentModel;
using System.Windows.Threading;

namespace WpfAppScheduler
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// 
    

    public partial class MainWindow : Window
{
        
        VisitsMenager visitsMenager = new VisitsMenager();
        WaitingVisitsMenager wvMenager = new WaitingVisitsMenager();

        static DateTime _dt = DateTime.Now;
        string dayOfWeek = _dt.DayOfWeek.ToString();
        DateTime prevDT = new DateTime(_dt.Year, _dt.Month, _dt.Day);
        DateTime monCal = DateTime.Now;
        DateTime sunCal = DateTime.Now;

        public MainWindow()
        {
            InitializeComponent();
            onLoaded();
        }

        private void onLoaded()
        {
            today.Content = "Dzisiaj jest: " + _dt;
            setDates();
            showVisitsForThisWeek();
            
            if(areWaitings())
            {
                //są ozekujące - wyświetl powiadomienie
                //goToWaitingList.Background = Brushes.LightCoral;

                ToolTip tt = new ToolTip();
                tt.Content = "Nowe oczekujące wizyty!";
                goToWaitingList.ToolTip = tt;

            }
            else
            {
                //brak oczekujących, normalny wygląd

                ToolTip tt = new ToolTip();
                tt.Content = "Brak wizyt do zaakceptowania";
                goToWaitingList.ToolTip = tt;
                goToWaitingList.IsEnabled = false;
            }
        }

        private bool areWaitings()
        {
            List<WaitingVisit> allVisits = new List<WaitingVisit>();
            allVisits = wvMenager.GetAllVisits();
            if (allVisits.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        
        private void setDates()
        {
            string caseSwitch = dayOfWeek;
            switch (caseSwitch)
            {
                case "Monday":
                    dayLabel1.Content = "Pon " + _dt.Day + "/" + _dt.Month + "/" + _dt.Year; //dzień 0
                    dayLabel2.Content = "Wto " + (_dt.AddDays(1).Day) + "/" + _dt.AddDays(1).Month + "/" + _dt.AddDays(1).Year;
                    dayLabel3.Content = "Śro " + (_dt.AddDays(2).Day) + "/" + _dt.AddDays(2).Month + "/" + _dt.AddDays(2).Year;
                    dayLabel4.Content = "Czw " + (_dt.AddDays(3).Day) + "/" + _dt.AddDays(3).Month + "/" + _dt.AddDays(3).Year;
                    dayLabel5.Content = "Pt " + (_dt.AddDays(4).Day) + "/" + _dt.AddDays(4).Month + "/" + _dt.AddDays(4).Year;
                    dayLabel6.Content = "Sob " + (_dt.AddDays(5).Day) + "/" + _dt.AddDays(5).Month + "/" + _dt.AddDays(5).Year;
                    dayLabel7.Content = "Nd " + (_dt.AddDays(6).Day) + "/" + _dt.AddDays(6).Month + "/" + _dt.AddDays(6).Year;
                    monCal = prevDT;
                    sunCal = prevDT.AddDays(6);
                    break;
                case "Tuesday":
                    dayLabel1.Content = "Pon " + (_dt.AddDays(-1).Day) + "/" + _dt.AddDays(-1).Month + "/" + _dt.AddDays(-1).Year;
                    dayLabel2.Content = "Wto " + _dt.Day + "/" + _dt.Month + "/" + _dt.Year; //dzień 0
                    dayLabel3.Content = "Śro " + (_dt.AddDays(1).Day) + "/" + _dt.AddDays(1).Month + "/" + _dt.AddDays(1).Year;
                    dayLabel4.Content = "Czw " + (_dt.AddDays(2).Day) + "/" + _dt.AddDays(2).Month + "/" + _dt.AddDays(2).Year;
                    dayLabel5.Content = "Pt " + (_dt.AddDays(3).Day) + "/" + _dt.AddDays(3).Month + "/" + _dt.AddDays(3).Year;
                    dayLabel6.Content = "Sob " + (_dt.AddDays(4).Day) + "/" + _dt.AddDays(4).Month + "/" + _dt.AddDays(4).Year;
                    dayLabel7.Content = "Nd " + (_dt.AddDays(5).Day) + "/" + _dt.AddDays(5).Month + "/" + _dt.AddDays(5).Year;
                    monCal = prevDT.AddDays(-1);
                    sunCal = prevDT.AddDays(5);
                    break;
                case "Wednesday":
                    dayLabel1.Content = "Pon " + (_dt.AddDays(-2).Day) + "/" + _dt.AddDays(-2).Month + "/" + _dt.AddDays(-2).Year;
                    dayLabel2.Content = "Wto " + (_dt.AddDays(-1).Day) + "/" + _dt.AddDays(-1).Month + "/" + _dt.AddDays(-1).Year;
                    dayLabel3.Content = "Śro " + _dt.Day + "/" + _dt.Month + "/" + _dt.Year; //dzień 0
                    dayLabel4.Content = "Czw " + (_dt.AddDays(1).Day) + "/" + _dt.AddDays(1).Month + "/" + _dt.AddDays(1).Year;
                    dayLabel5.Content = "Pt " + (_dt.AddDays(2).Day) + "/" + _dt.AddDays(2).Month + "/" + _dt.AddDays(2).Year;
                    dayLabel6.Content = "Sob " + (_dt.AddDays(3).Day) + "/" + _dt.AddDays(3).Month + "/" + _dt.AddDays(3).Year;
                    dayLabel7.Content = "Nd " + (_dt.AddDays(4).Day) + "/" + _dt.AddDays(4).Month + "/" + _dt.AddDays(4).Year;
                    monCal = prevDT.AddDays(-2);
                    sunCal = prevDT.AddDays(4);
                    break;
                case "Thursday":
                    dayLabel1.Content = "Pon " + (_dt.AddDays(-3).Day) + "/" + _dt.AddDays(-3).Month + "/" + _dt.AddDays(-3).Year;
                    dayLabel2.Content = "Wto " + (_dt.AddDays(-2).Day) + "/" + _dt.AddDays(-2).Month + "/" + _dt.AddDays(-2).Year;
                    dayLabel3.Content = "Śro " + (_dt.AddDays(-1).Day) + "/" + _dt.AddDays(-1).Month + "/" + _dt.AddDays(-1).Year;
                    dayLabel4.Content = "Czw " + _dt.Day + "/" + _dt.Month + "/" + _dt.Year; //dzień 0
                    dayLabel5.Content = "Pt " + (_dt.AddDays(1).Day) + "/" + _dt.AddDays(1).Month + "/" + _dt.AddDays(1).Year;
                    dayLabel6.Content = "Sob " + (_dt.AddDays(2).Day) + "/" + _dt.AddDays(2).Month + "/" + _dt.AddDays(2).Year;
                    dayLabel7.Content = "Nd " + (_dt.AddDays(3).Day) + "/" + _dt.AddDays(3).Month + "/" + _dt.AddDays(3).Year;
                    monCal = prevDT.AddDays(-3);
                    sunCal = prevDT.AddDays(3);
                    break;
                case "Friday":
                    dayLabel1.Content = "Pon " + (_dt.AddDays(-4).Day) + "/" + _dt.AddDays(-4).Month + "/" + _dt.AddDays(-4).Year;
                    dayLabel2.Content = "Wto " + (_dt.AddDays(-3).Day) + "/" + _dt.AddDays(-3).Month + "/" + _dt.AddDays(-3).Year;
                    dayLabel3.Content = "Śro " + (_dt.AddDays(-2).Day) + "/" + _dt.AddDays(-2).Month + "/" + _dt.AddDays(-2).Year;
                    dayLabel4.Content = "Czw " + (_dt.AddDays(-1).Day) + "/" + _dt.AddDays(-1).Month + "/" + _dt.AddDays(-1).Year;
                    dayLabel5.Content = "Pt " + _dt.Day + "/" + _dt.Month + "/" + _dt.Year;//dzień 0
                    dayLabel6.Content = "Sob " + (_dt.AddDays(1).Day) + "/" + _dt.AddDays(1).Month + "/" + _dt.AddDays(1).Year;
                    dayLabel7.Content = "Nd " + (_dt.AddDays(2).Day) + "/" + _dt.AddDays(2).Month + "/" + _dt.AddDays(2).Year;
                    monCal = prevDT.AddDays(-4);
                    sunCal = prevDT.AddDays(2);
                    break;
                case "Saturday":
                    dayLabel1.Content = "Pon " + (_dt.AddDays(-5).Day) + "/" + _dt.AddDays(-5).Month + "/" + _dt.AddDays(-5).Year;
                    dayLabel2.Content = "Wto " + (_dt.AddDays(-4).Day) + "/" + _dt.AddDays(-4).Month + "/" + _dt.AddDays(-4).Year;
                    dayLabel3.Content = "Śro " + (_dt.AddDays(-3).Day) + "/" + _dt.AddDays(-3).Month + "/" + _dt.AddDays(-3).Year;
                    dayLabel4.Content = "Czw " + (_dt.AddDays(-2).Day) + "/" + _dt.AddDays(-2).Month + "/" + _dt.AddDays(-2).Year;
                    dayLabel5.Content = "Pt " + (_dt.AddDays(-1).Day) + "/" + _dt.AddDays(-1).Month + "/" + _dt.AddDays(-1).Year;
                    dayLabel6.Content = "Sob " + _dt.Day + "/" + _dt.Month + "/" + _dt.Year;//dzień 0
                    dayLabel7.Content = "Nd " + (_dt.AddDays(1).Day) + "/" + _dt.AddDays(1).Month + "/" + _dt.AddDays(1).Year;
                    monCal = prevDT.AddDays(-5);
                    sunCal = prevDT.AddDays(1);
                    break;
                case "Sunday":
                    dayLabel1.Content = "Pon " + (_dt.AddDays(-6).Day) + "/" + _dt.AddDays(-6).Month + "/" + _dt.AddDays(-6).Year;
                    dayLabel2.Content = "Wto " + (_dt.AddDays(-5).Day) + "/" + _dt.AddDays(-5).Month + "/" + _dt.AddDays(-5).Year;
                    dayLabel3.Content = "Śro " + (_dt.AddDays(-4).Day) + "/" + _dt.AddDays(-4).Month + "/" + _dt.AddDays(-4).Year;
                    dayLabel4.Content = "Czw " + (_dt.AddDays(-3).Day) + "/" + _dt.AddDays(-3).Month + "/" + _dt.AddDays(-3).Year;
                    dayLabel5.Content = "Pt " + (_dt.AddDays(-2).Day) + "/" + _dt.AddDays(-2).Month + "/" + _dt.AddDays(-2).Year;
                    dayLabel6.Content = "Sob " + (_dt.AddDays(-1).Day) + "/" + _dt.AddDays(-1).Month + "/" + _dt.AddDays(-1).Year;
                    dayLabel7.Content = "Nd " + _dt.Day + "/" + _dt.Month + "/" + _dt.Year; //dzień 0
                    monCal = prevDT.AddDays(-6);
                    sunCal = prevDT;
                    break;

            }
            
        }

        private void prevBtn_Click(object sender, RoutedEventArgs e)
        {
            //wyświetl przeszły tydzień
            string caseSwitch = dayOfWeek;
            prevDT = prevDT.AddDays(-7);

            switch (caseSwitch)
            {
                case "Monday":
                    dayLabel1.Content = "Pon " + prevDT.Day + "/" + prevDT.Month + "/" + prevDT.Year; //dzień 0
                    dayLabel2.Content = "Wto " + (prevDT.AddDays(1).Day) + "/" + prevDT.AddDays(1).Month + "/" + prevDT.AddDays(1).Year;
                    dayLabel3.Content = "Śro " + (prevDT.AddDays(2).Day) + "/" + prevDT.AddDays(2).Month + "/" + prevDT.AddDays(2).Year;
                    dayLabel4.Content = "Czw " + (prevDT.AddDays(3).Day) + "/" + prevDT.AddDays(3).Month + "/" + prevDT.AddDays(3).Year;
                    dayLabel5.Content = "Pt " + (prevDT.AddDays(4).Day) + "/" + prevDT.AddDays(4).Month + "/" + prevDT.AddDays(4).Year;
                    dayLabel6.Content = "Sob " + (prevDT.AddDays(5).Day) + "/" + prevDT.AddDays(5).Month + "/" + prevDT.AddDays(5).Year;
                    dayLabel7.Content = "Nd " + (prevDT.AddDays(6).Day) + "/" + prevDT.AddDays(6).Month + "/" + prevDT.AddDays(6).Year;
                    monCal = prevDT; //data poniedziałku w przeszłym tygodniu 
                    sunCal = prevDT.AddDays(6); //data niedzieli w przeszłym tygodniu
                    break;
                case "Tuesday":
                    dayLabel1.Content = "Pon " + (prevDT.AddDays(-1).Day) + "/" + prevDT.AddDays(-1).Month + "/" + prevDT.AddDays(-1).Year;
                    dayLabel2.Content = "Wto " + prevDT.Day + "/" + prevDT.Month + "/" + prevDT.Year; ; //dzień 0
                    dayLabel3.Content = "Śro " + (prevDT.AddDays(1).Day) + "/" + prevDT.AddDays(1).Month + "/" + prevDT.AddDays(1).Year;
                    dayLabel4.Content = "Czw " + (prevDT.AddDays(2).Day) + "/" + prevDT.AddDays(2).Month + "/" + prevDT.AddDays(2).Year;
                    dayLabel5.Content = "Pt " + (prevDT.AddDays(3).Day) + "/" + prevDT.AddDays(3).Month + "/" + prevDT.AddDays(3).Year;
                    dayLabel6.Content = "Sob " + (prevDT.AddDays(4).Day) + "/" + prevDT.AddDays(4).Month + "/" + prevDT.AddDays(4).Year;
                    dayLabel7.Content = "Nd " + (prevDT.AddDays(5).Day) + "/" + prevDT.AddDays(5).Month + "/" + prevDT.AddDays(5).Year;
                    monCal = prevDT.AddDays(-1);
                    sunCal = prevDT.AddDays(5);
                    break;
                case "Wednesday":
                    dayLabel1.Content = "Pon " + (prevDT.AddDays(-2).Day) + "/" + prevDT.AddDays(-2).Month + "/" + prevDT.AddDays(-2).Year;
                    dayLabel2.Content = "Wto " + (prevDT.AddDays(-1).Day) + "/" + prevDT.AddDays(-1).Month + "/" + prevDT.AddDays(-1).Year;
                    dayLabel3.Content = "Śro " + prevDT.Day + "/" + prevDT.Month + "/" + prevDT.Year; ; //dzień 0
                    dayLabel4.Content = "Czw " + (prevDT.AddDays(1).Day) + "/" + prevDT.AddDays(1).Month + "/" + prevDT.AddDays(1).Year;
                    dayLabel5.Content = "Pt " + (prevDT.AddDays(2).Day) + "/" + prevDT.AddDays(2).Month + "/" + prevDT.AddDays(2).Year;
                    dayLabel6.Content = "Sob " + (prevDT.AddDays(3).Day) + "/" + prevDT.AddDays(3).Month + "/" + prevDT.AddDays(3).Year;
                    dayLabel7.Content = "Nd " + (prevDT.AddDays(4).Day) + "/" + prevDT.AddDays(4).Month + "/" + prevDT.AddDays(4).Year;
                    monCal = prevDT.AddDays(-2);
                    sunCal = prevDT.AddDays(4);
                    break;
                case "Thursday":
                    dayLabel1.Content = "Pon " + (prevDT.AddDays(-3).Day) + "/" + prevDT.AddDays(-3).Month + "/" + prevDT.AddDays(-3).Year;
                    dayLabel2.Content = "Wto " + (prevDT.AddDays(-2).Day) + "/" + prevDT.AddDays(-2).Month + "/" + prevDT.AddDays(-2).Year;
                    dayLabel3.Content = "Śro " + (prevDT.AddDays(-1).Day) + "/" + prevDT.AddDays(-1).Month + "/" + prevDT.AddDays(-1).Year;
                    dayLabel4.Content = "Czw " + prevDT.Day + "/" + prevDT.Month + "/" + prevDT.Year; ; //dzień 0
                    dayLabel5.Content = "Pt " + (prevDT.AddDays(1).Day) + "/" + prevDT.AddDays(1).Month + "/" + prevDT.AddDays(1).Year;
                    dayLabel6.Content = "Sob " + (prevDT.AddDays(2).Day) + "/" + prevDT.AddDays(2).Month + "/" + prevDT.AddDays(2).Year;
                    dayLabel7.Content = "Nd " + (prevDT.AddDays(3).Day) + "/" + prevDT.AddDays(3).Month + "/" + prevDT.AddDays(3).Year;
                    monCal = prevDT.AddDays(-3);
                    sunCal = prevDT.AddDays(3);
                    break;
                case "Friday":
                    dayLabel1.Content = "Pon " + (prevDT.AddDays(-4).Day) + "/" + prevDT.AddDays(-4).Month + "/" + prevDT.AddDays(-4).Year;
                    dayLabel2.Content = "Wto " + (prevDT.AddDays(-3).Day) + "/" + prevDT.AddDays(-3).Month +"/" + prevDT.AddDays(-3).Year;
                    dayLabel3.Content = "Śro " + (prevDT.AddDays(-2).Day) + "/" + prevDT.AddDays(-2).Month + "/" + prevDT.AddDays(-2).Year;
                    dayLabel4.Content = "Czw " + (prevDT.AddDays(-1).Day) + "/" + prevDT.AddDays(-1).Month + "/" + prevDT.AddDays(-1).Year;
                    dayLabel5.Content = "Pt " + prevDT.Day + "/" + prevDT.Month + "/" + prevDT.Year; ;//dzień 0
                    dayLabel6.Content = "Sob " + (prevDT.AddDays(1).Day) + "/" + prevDT.AddDays(1).Month + "/" + prevDT.AddDays(1).Year;
                    dayLabel7.Content = "Nd " + (prevDT.AddDays(2).Day) + "/" + prevDT.AddDays(2).Month + "/" + prevDT.AddDays(2).Year;
                    monCal = prevDT.AddDays(-4);
                    sunCal = prevDT.AddDays(2);
                    break;
                case "Saturday":
                    dayLabel1.Content = "Pon " + (prevDT.AddDays(-5).Day) + "/" + prevDT.AddDays(-5).Month + "/" + prevDT.AddDays(-5).Year;
                    dayLabel2.Content = "Wto " + (prevDT.AddDays(-4).Day) + "/" + prevDT.AddDays(-4).Month + "/" + prevDT.AddDays(-4).Year;
                    dayLabel3.Content = "Śro " + (prevDT.AddDays(-3).Day) + "/" + prevDT.AddDays(-3).Month + "/" + prevDT.AddDays(-3).Year;
                    dayLabel4.Content = "Czw " + (prevDT.AddDays(-2).Day) + "/" + prevDT.AddDays(-2).Month + "/" + prevDT.AddDays(-2).Year;
                    dayLabel5.Content = "Pt " + (prevDT.AddDays(-1).Day) + "/" + prevDT.AddDays(-1).Month + "/" + prevDT.AddDays(-1).Year;
                    dayLabel6.Content = "Sob " + prevDT.Day + "/" + prevDT.Month + "/" + prevDT.Year; ;//dzień 0
                    dayLabel7.Content = "Nd " + (prevDT.AddDays(1).Day) + "/" + prevDT.AddDays(1).Month + "/" + prevDT.AddDays(1).Year;
                    monCal = prevDT.AddDays(-5);
                    sunCal = prevDT.AddDays(1);
                    break;
                case "Sunday":
                    dayLabel1.Content = "Pon " + (prevDT.AddDays(-6).Day) + "/" + prevDT.AddDays(-6).Month + "/" + prevDT.AddDays(-6).Year;
                    dayLabel2.Content = "Wto " + (prevDT.AddDays(-5).Day) + "/" + prevDT.AddDays(-5).Month + "/" + prevDT.AddDays(-5).Year;
                    dayLabel3.Content = "Śro " + (prevDT.AddDays(-4).Day) + "/" + prevDT.AddDays(-4).Month + "/" + prevDT.AddDays(-4).Year;
                    dayLabel4.Content = "Czw " + (prevDT.AddDays(-3).Day) + "/" + prevDT.AddDays(-3).Month + "/" + prevDT.AddDays(-3).Year;
                    dayLabel5.Content = "Pt " + (prevDT.AddDays(-2).Day) + "/" + prevDT.AddDays(-2).Month + "/" + prevDT.AddDays(-2).Year;
                    dayLabel6.Content = "Sob " + (prevDT.AddDays(-1).Day) + "/" + prevDT.AddDays(-1).Month + "/" + prevDT.AddDays(-1).Year;
                    dayLabel7.Content = "Nd " + prevDT.Day + "/" + prevDT.Month + "/" + prevDT.Year; ; //dzień 0
                    monCal = prevDT.AddDays(-6);
                    sunCal = prevDT;
                    break;

            }
            clrConvas();
            showVisitsForThisWeek();

        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            // wyświetl następny tydzień
            string caseSwitch = dayOfWeek;
            prevDT = prevDT.AddDays(7);
            switch (caseSwitch)
            {
                case "Monday":
                    dayLabel1.Content = "Pon " + prevDT.Day + "/" + prevDT.Month + "/" + prevDT.Year; //dzień 0
                    dayLabel2.Content = "Wto " + (prevDT.AddDays(1).Day) + "/" + prevDT.AddDays(1).Month + "/" + prevDT.AddDays(1).Year;
                    dayLabel3.Content = "Śro " + (prevDT.AddDays(2).Day) + "/" + prevDT.AddDays(2).Month + "/" + prevDT.AddDays(2).Year;
                    dayLabel4.Content = "Czw " + (prevDT.AddDays(3).Day) + "/" + prevDT.AddDays(3).Month + "/" + prevDT.AddDays(3).Year;
                    dayLabel5.Content = "Pt " + (prevDT.AddDays(4).Day) + "/" + prevDT.AddDays(4).Month + "/" + prevDT.AddDays(4).Year;
                    dayLabel6.Content = "Sob " + (prevDT.AddDays(5).Day) + "/" + prevDT.AddDays(5).Month + "/" + prevDT.AddDays(5).Year;
                    dayLabel7.Content = "Nd " + (prevDT.AddDays(6).Day) + "/" + prevDT.AddDays(6).Month + "/" + prevDT.AddDays(6).Year;
                    monCal = new DateTime(prevDT.Year, prevDT.Month, prevDT.Day);
                    sunCal = monCal.AddDays(6);
                    break;
                case "Tuesday":
                    dayLabel1.Content = "Pon " + (prevDT.AddDays(-1).Day) + "/" + prevDT.AddDays(-1).Month + "/" + prevDT.AddDays(-1).Year;
                    dayLabel2.Content = "Wto " + prevDT.Day + "/" + prevDT.Month + "/" + prevDT.Year; //dzień 0
                    dayLabel3.Content = "Śro " + (prevDT.AddDays(1).Day) + "/" + prevDT.AddDays(1).Month + "/" + prevDT.AddDays(1).Year;
                    dayLabel4.Content = "Czw " + (prevDT.AddDays(2).Day) + "/" + prevDT.AddDays(2).Month + "/" + prevDT.AddDays(2).Year;
                    dayLabel5.Content = "Pt " + (prevDT.AddDays(3).Day) + "/" + prevDT.AddDays(3).Month + "/" + prevDT.AddDays(3).Year;
                    dayLabel6.Content = "Sob " + (prevDT.AddDays(4).Day) + "/" + prevDT.AddDays(4).Month + "/" + prevDT.AddDays(4).Year;
                    dayLabel7.Content = "Nd " + (prevDT.AddDays(5).Day) + "/" + prevDT.AddDays(5).Month + "/" + prevDT.AddDays(5).Year;
                    monCal = new DateTime(prevDT.Year, prevDT.Month, prevDT.Day).AddDays(-1);
                    sunCal = monCal.AddDays(6);
                    break;
                case "Wednesday":
                    dayLabel1.Content = "Pon " + (prevDT.AddDays(-2).Day) + "/" + prevDT.AddDays(-2).Month + "/" + prevDT.AddDays(-2).Year;
                    dayLabel2.Content = "Wto " + (prevDT.AddDays(-1).Day) + "/" + prevDT.AddDays(-1).Month + "/" + prevDT.AddDays(-1).Year;
                    dayLabel3.Content = "Śro " + prevDT.Day + "/" + prevDT.Month + "/" + prevDT.Year; //dzień 0
                    dayLabel4.Content = "Czw " + (prevDT.AddDays(1).Day) + "/" + prevDT.AddDays(1).Month + "/" + prevDT.AddDays(1).Year;
                    dayLabel5.Content = "Pt " + (prevDT.AddDays(2).Day) + "/" + prevDT.AddDays(2).Month + "/" + prevDT.AddDays(2).Year;
                    dayLabel6.Content = "Sob " + (prevDT.AddDays(3).Day) + "/" + prevDT.AddDays(3).Month + "/" + prevDT.AddDays(3).Year;
                    dayLabel7.Content = "Nd " + (prevDT.AddDays(4).Day) + "/" + prevDT.AddDays(4).Month + "/" + prevDT.AddDays(4).Year;
                    monCal = new DateTime(prevDT.Year, prevDT.Month, prevDT.Day).AddDays(-2);
                    sunCal = monCal.AddDays(6);
                    break;
                case "Thursday":
                    dayLabel1.Content = "Pon " + (prevDT.AddDays(-3).Day) + "/" + prevDT.AddDays(-3).Month + "/" + prevDT.AddDays(-3).Year;
                    dayLabel2.Content = "Wto " + (prevDT.AddDays(-2).Day) + "/" + prevDT.AddDays(-2).Month + "/" + prevDT.AddDays(-2).Year;
                    dayLabel3.Content = "Śro " + (prevDT.AddDays(-1).Day) + "/" + prevDT.AddDays(-1).Month + "/" + prevDT.AddDays(-1).Year;
                    dayLabel4.Content = "Czw " + prevDT.Day + "/" + prevDT.Month + "/" + prevDT.Year; //dzień 0
                    dayLabel5.Content = "Pt " + (prevDT.AddDays(1).Day) + "/" + prevDT.AddDays(1).Month + "/" + prevDT.AddDays(1).Year;
                    dayLabel6.Content = "Sob " + (prevDT.AddDays(2).Day) + "/" + prevDT.AddDays(2).Month + "/" + prevDT.AddDays(2).Year;
                    dayLabel7.Content = "Nd " + (prevDT.AddDays(3).Day) + "/" + prevDT.AddDays(3).Month + "/" + prevDT.AddDays(3).Year;
                    monCal = new DateTime(prevDT.Year, prevDT.Month, prevDT.Day).AddDays(-3);
                    sunCal = monCal.AddDays(6);
                    break;
                case "Friday":
                    dayLabel1.Content = "Pon " + (prevDT.AddDays(-4).Day) + "/" + prevDT.AddDays(-4).Month + "/" + prevDT.AddDays(-4).Year;
                    dayLabel2.Content = "Wto " + (prevDT.AddDays(-3).Day) + "/" + prevDT.AddDays(-3).Month + "/" + prevDT.AddDays(-3).Year;
                    dayLabel3.Content = "Śro " + (prevDT.AddDays(-2).Day) + "/" + prevDT.AddDays(-2).Month + "/" + prevDT.AddDays(-2).Year;
                    dayLabel4.Content = "Czw " + (prevDT.AddDays(-1).Day) + "/" + prevDT.AddDays(-1).Month + "/" + prevDT.AddDays(-1).Year;
                    dayLabel5.Content = "Pt " + prevDT.Day + "/" + prevDT.Month + "/" + prevDT.Year;//dzień 0
                    dayLabel6.Content = "Sob " + (prevDT.AddDays(1).Day) + "/" + prevDT.AddDays(1).Month + "/" + prevDT.AddDays(1).Year;
                    dayLabel7.Content = "Nd " + (prevDT.AddDays(2).Day) + "/" + prevDT.AddDays(2).Month + "/" + prevDT.AddDays(2).Year;
                    monCal = new DateTime(prevDT.Year, prevDT.Month, prevDT.Day).AddDays(-4);
                    sunCal = monCal.AddDays(6);
                    break;
                case "Saturday":
                    dayLabel1.Content = "Pon " + (prevDT.AddDays(-5).Day) + "/" + prevDT.AddDays(-5).Month + "/" + prevDT.AddDays(-5).Year;
                    dayLabel2.Content = "Wto " + (prevDT.AddDays(-4).Day) + "/" + prevDT.AddDays(-4).Month + "/" + prevDT.AddDays(-4).Year;
                    dayLabel3.Content = "Śro " + (prevDT.AddDays(-3).Day) + "/" + prevDT.AddDays(-3).Month + "/" + prevDT.AddDays(-3).Year;
                    dayLabel4.Content = "Czw " + (prevDT.AddDays(-2).Day) + "/" + prevDT.AddDays(-2).Month + "/" + prevDT.AddDays(-2).Year;
                    dayLabel5.Content = "Pt " + (prevDT.AddDays(-1).Day) + "/" + prevDT.AddDays(-1).Month + "/" + prevDT.AddDays(-1).Year;
                    dayLabel6.Content = "Sob " + prevDT.Day + "/" + prevDT.Month + "/" + prevDT.Year;//dzień 0
                    dayLabel7.Content = "Nd " + (prevDT.AddDays(1).Day) + "/" + prevDT.AddDays(1).Month + "/" + prevDT.AddDays(1).Year;
                    monCal = new DateTime(prevDT.Year, prevDT.Month, prevDT.Day).AddDays(-5);
                    sunCal = monCal.AddDays(6);
                    break;
                case "Sunday":
                    dayLabel1.Content = "Pon " + (prevDT.AddDays(-6).Day) + "/" + prevDT.AddDays(-6).Month + "/" + prevDT.AddDays(-6).Year;
                    dayLabel2.Content = "Wto " + (prevDT.AddDays(-5).Day) + "/" + prevDT.AddDays(-5).Month + "/" + prevDT.AddDays(-5).Year;
                    dayLabel3.Content = "Śro " + (prevDT.AddDays(-4).Day) + "/" + prevDT.AddDays(-4).Month + "/" + prevDT.AddDays(-4).Year;
                    dayLabel4.Content = "Czw " + (prevDT.AddDays(-3).Day) + "/" + prevDT.AddDays(-3).Month + "/" + prevDT.AddDays(-3).Year;
                    dayLabel5.Content = "Pt " + (prevDT.AddDays(-2).Day) + "/" + prevDT.AddDays(-2).Month + "/" + prevDT.AddDays(-2).Year;
                    dayLabel6.Content = "Sob " + (prevDT.AddDays(-1).Day) + "/" + prevDT.AddDays(-1).Month + "/" + prevDT.AddDays(-1).Year;
                    dayLabel7.Content = "Nd " + prevDT.Day + "/" + prevDT.Month + "/" + prevDT.Year; //dzień 0
                    monCal = new DateTime(prevDT.Year, prevDT.Month, prevDT.Day).AddDays(-6);
                    sunCal = monCal.AddDays(6);
                    break;
                }

            clrConvas();
            showVisitsForThisWeek();
        }

        private void goToWaitingList_Click(object sender, RoutedEventArgs e)
        {
            //idż do listy 
            WaitingList wlWindow = new WaitingList();
            wlWindow.ShowDialog();

        }

        private void goToAddVisit_Click(object sender, RoutedEventArgs e)
        {
            AddVisit vWidnow = new AddVisit();
            vWidnow.ShowDialog();
        }


        void OnPreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string day = "";
            int startHour = 0;
            int startMinute = 0;
            DateTime clickedDay;

            if (e.ClickCount == 2) // for double-click, remove this condition if only want single click
            {
                var point = Mouse.GetPosition(EventsGrid);

                int row = 0;
                int col = 0;
                double accumulatedHeight = 0.0;
                double accumulatedWidth = 0.0;

                // calc row mouse was over
                foreach (var rowDefinition in EventsGrid.RowDefinitions)
                {
                    accumulatedHeight += rowDefinition.ActualHeight;
                    if (accumulatedHeight >= point.Y)
                        break;
                    row++;
                }

                // calc col mouse was over
                foreach (var columnDefinition in EventsGrid.ColumnDefinitions)
                {
                    accumulatedWidth += columnDefinition.ActualWidth;
                    if (accumulatedWidth >= point.X)
                        break;
                    col++;
                }

                Console.WriteLine(row + ":" + col);

                switch(col)
                {
                    case 0:
                        day += dayLabel1.Content;
                        break;
                    case 1:
                        day += dayLabel1.Content;
                        break;
                    case 2:
                        day += dayLabel2.Content;
                        break;
                    case 3:
                        day += dayLabel3.Content;
                        break;
                    case 4:
                        day += dayLabel4.Content;
                        break;
                    case 5:
                        day += dayLabel5.Content;
                        break;
                    case 6:
                        day += dayLabel6.Content;
                        break;
                    case 7:
                        day += dayLabel7.Content;
                        break;
                }
                     
                day = day.Remove(0, 4);
                Console.WriteLine(day);
                string[] subs = day.Split('/');          
                int d = int.Parse(subs[0]);
                int m = int.Parse(subs[1]);
                int y = int.Parse(subs[2]);

                switch(row)
                {
                    case 0:
                        startHour = 9;
                        break;
                    case 1:
                        startHour = 9;
                        break;
                    case 2:
                        startHour = 9;
                        break;
                    case 3:
                        startHour = 9;
                        break;
                    case 4:
                        startHour = 10;
                        break;
                    case 5:
                        startHour = 10;
                        break;
                    case 6:
                        startHour = 10;
                        break;
                    case 7:
                        startHour = 10;
                        break;
                    case 8:
                        startHour = 11;
                        break;
                    case 9:
                        startHour = 11;
                        break;
                    case 10:
                        startHour = 11;
                        break;
                    case 11:
                        startHour = 11;
                        break;
                    case 12:
                        startHour = 12;
                        break;
                    case 13:
                        startHour = 12;
                        break;
                    case 14:
                        startHour = 12;
                        break;
                    case 15:
                        startHour = 12;
                        break;
                    case 16:
                        startHour = 13;
                        break;
                    case 17:
                        startHour = 13;
                        break;
                    case 18:
                        startHour = 13;
                        break;
                    case 19:
                        startHour = 13;
                        break;
                    case 20:
                        startHour = 14;
                        break;
                    case 21:
                        startHour = 14;
                        break;
                    case 22:
                        startHour = 14;
                        break;
                    case 23:
                        startHour = 14;
                        break;
                    case 24:
                        startHour = 15;
                        break;
                    case 25:
                        startHour = 15;
                        break;
                    case 26:
                        startHour = 15;
                        break;
                    case 27:
                        startHour = 15;
                        break;
                    case 28:
                        startHour = 16;
                        break;
                    case 29:
                        startHour = 16;
                        break;
                    case 30:
                        startHour = 16;
                        break;
                    case 31:
                        startHour = 16;
                        break;
                    case 32:
                        startHour = 17;
                        break;
                    case 33:
                        startHour = 17;
                        break;
                    case 34:
                        startHour = 17;
                        break;
                    case 35:
                        startHour = 17;
                        break;
                    case 36:
                        startHour = 18;
                        break;
                    case 37:
                        startHour = 18;
                        break;
                    case 38:
                        startHour = 18;
                        break;
                    case 39:
                        startHour = 18;
                        break;

                }

                switch(row%4)
                {
                    case 0:
                        startMinute = 0;
                        break;
                    case 1:
                        startMinute = 15;
                        break;
                    case 2:
                        startMinute = 30;
                        break;
                    case 3:
                        startMinute = 45;
                        break;

                }

                clickedDay = new DateTime(y, m, d, startHour, startMinute, 0);
                Console.WriteLine(clickedDay);
                if (clickedDay != null)
                {
                    AddVisit vWindow = new AddVisit(clickedDay);
                    vWindow.ShowDialog();
                }
            }
            
        }

        public void showVisitsForThisWeek()
        {
            clrConvas();
            //wyświetl wizyty z danego tygodnia:
            Console.WriteLine("Pon i nd: " + monCal + " , " + sunCal);

            List<VisitInfo> allVisits = new List<VisitInfo>();
            List<VisitInfo> weekVisits = new List<VisitInfo>();

            allVisits = visitsMenager.GetAllVisits();
            weekVisits = VisitsInThisWeek(allVisits);

            List<VisitInfo> VisitsInThisWeek(List<VisitInfo> visitList)
            {
                List<VisitInfo> result = new List<VisitInfo>();

                foreach (var v in visitList)
                { 
                    //sprawdzenie czy wizyta w danym tygodniu: 
                    if((v.VisitDate).CompareTo(monCal) >= 0 && (v.VisitDate).CompareTo(sunCal) <= 0)
                    { 
                        result.Add(v);
                    }
                }
                return result;
            }

            foreach (var v in weekVisits)
            {
                CreateVisitControl(v);
            }

        }
        private void clrConvas()
        {
            column1.Children.Clear();
            column2.Children.Clear();
            column3.Children.Clear();
            column4.Children.Clear();
            column5.Children.Clear();
            column6.Children.Clear();
            column7.Children.Clear();
        }
        private void CreateVisitControl(VisitInfo v)
        {
            int column = (int)v.VisitDate.DayOfWeek;//0 dla niedzieli, 1 dla pon...
            int row = 0;
            int h = v.Start.Hour;
            int m = v.Start.Minute;
            int eh = v.End.Hour;
            int em = v.End.Minute;
            
            switch (h)
            {
                case 9:
                    row = 0;
                    break;
                case 10:
                    row = 4;
                    break;
                case 11:
                    row = 8;
                    break;
                case 12:
                    row = 12;
                    break;
                case 13:
                    row = 16;
                    break;
                case 14:
                    row = 20;
                    break;
                case 15:
                    row = 24;
                    break;
                case 16:
                    row = 28;
                    break;
                case 17:
                    row = 32;
                    break;
                case 18:
                    row = 36;
                    break;
            }

            switch(m)
            {
                case 0:
                    row += 0;
                    break;
                case 15:
                    row += 1;
                    break;
                case 30:
                    row += 2;
                    break;
                case 45:
                    row += 3;
                    break;
            }

            int length = ((eh * 60 + em) - (h*60 + m))/15 ;

            //tworzenie kontrolki button - wizyty
            Button b = new Button();      
            b.Background = Brushes.BlanchedAlmond;
            b.Name = "visitEvent";
            b.Height = 40 * length;
            b.Width = column5.ActualWidth; //150;
            b.HorizontalAlignment = HorizontalAlignment.Stretch;
            b.Margin = new Thickness(0, row * 40, 0, 0);
            b.Focusable = true;
            b.Content = v.Name +  "\n" + v.Phone;
            if (v.Note != "")
            {
                b.Content += "\n" + v.Note;
            }
            b.DataContext = v.VisitId;
            b.Click += new RoutedEventHandler(EventClick);

            ToolTip tt = new ToolTip();
            tt.Content = v.Name + "\n" + v.Phone; 
            if(v.Email != "")
            {
                tt.Content += "\n" + v.Email;
            }
            if (v.Note != "")
            {
                tt.Content += "\n" + v.Note;
            }

            b.ToolTip = tt;
            switch (column)
            {
                case 1:
                    column1.Children.Add(b);
                    break;
                case 2:
                    column2.Children.Add(b);
                    break;
                case 3:
                    column3.Children.Add(b);
                    break;
                case 4:
                    column4.Children.Add(b);
                    break;
                case 5:
                    column5.Children.Add(b);
                    break;
                case 6:
                    column6.Children.Add(b);
                    break;
                case 0:
                    column7.Children.Add(b);
                    break;
            }
            
        }

        private void EventClick(object sender, RoutedEventArgs ev)
        {
            Button _btn = sender as Button;
            Console.WriteLine(_btn.DataContext);
            int _btnVisitId = (int)_btn.DataContext;
            Console.WriteLine(_btnVisitId); 
            AddVisit vWidnow = new AddVisit(_btnVisitId);
            vWidnow.ShowDialog();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            clrConvas();
            showVisitsForThisWeek();
        }

  
    }
}
