using System;
using System.Windows;
using System.Windows.Controls;
using WpfAppScheduler.Models;
using WpfAppScheduler.Models.DB;
using WpfAppScheduler.Models.Info;

namespace WpfAppScheduler
{
    /// <summary>
    /// Logika interakcji dla klasy Visit.xaml
    /// </summary>
    public partial class AddVisit : Window
    {
        VisitsMenager visitsMenager = new VisitsMenager();
        DateTime _dt = DateTime.Now;
        bool _ifExist = false;
        int idExist;
        public AddVisit()
        {
            InitializeComponent();
            if(!_ifExist)
            {
                btnDelete.IsEnabled = false;
            }  
        }

        public AddVisit(DateTime dt) : this()
        {
            _dt = dt;
            dtPicker.SelectedDate = _dt;
            from.SelectedIndex = getComboBoxIndexFromHour(_dt.Hour, _dt.Minute);

        }
        public AddVisit(int id) : this()
        {
            VisitInfo v = visitsMenager.GetVisit(id);
            idExist = v.VisitId;
            name.Text = v.Name;
            phone.Text = v.Phone;
            email.Text = v.Email;
            notes.Text = v.Note;
            dtPicker.SelectedDate = v.VisitDate;

            from.SelectedIndex = getComboBoxIndexFromHour(v.Start.Hour, v.Start.Minute);
            to.SelectedIndex = getComboBoxIndexFromHour(v.End.Hour, v.End.Minute);

            _ifExist = true;
            btnDelete.IsEnabled = true;

        }

        public AddVisit(string nam, string pho, string ema, string not, DateTime date, DateTime hour) : this()
        {
            name.Text = nam;
            phone.Text = pho;
            email.Text = ema;
            notes.Text = not;
            dtPicker.SelectedDate = date;

            from.SelectedIndex = getComboBoxIndexFromHour(hour.Hour, hour.Minute);
            to.SelectedIndex = getComboBoxIndexFromHour((hour.AddHours(0.5)).Hour, (hour.AddHours(0.5)).Minute);

        }

        private int getComboBoxIndexFromHour(int hour, int minute)
        {
            int index = 0;

            switch (hour)
            {
                case 9:
                    index = 0;
                    break;
                case 10:
                    index = 4;
                    break;
                case 11:
                    index = 8;
                    break;
                case 12:
                    index = 12;
                    break;
                case 13:
                    index = 16;
                    break;
                case 14:
                    index = 20;
                    break;
                case 15:
                    index = 24;
                    break;
                case 16:
                    index = 28;
                    break;
                case 17:
                    index = 32;
                    break;
                case 18:
                    index = 36;
                    break;
            }
            switch (minute)
            {
                case 0:
                    index += 0;
                    break;
                case 15:
                    index += 1;
                    break;
                case 30:
                    index += 2;
                    break;
                case 45:
                    index += 3;
                    break;
            }

            return index;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtPicker.SelectedDate = _dt;
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {

            //Tworzenie / edycja wizyty, zapisywanie wizyty do BD i zamyka 
            if(_ifExist==true)
            {
                visitsMenager.RemoveVisit(idExist);
            }


            if (!name.Text.Equals("") && !phone.Text.Equals("") && dtPicker.SelectedDate != null)
            {
                DateTime newDate = (DateTime)dtPicker.SelectedDate;
                DateTime st = new DateTime(dtPicker.SelectedDate.Value.Year, dtPicker.SelectedDate.Value.Month, dtPicker.SelectedDate.Value.Day, HourToIntd(from), MinuteToIntd(from), 0);
                DateTime en = new DateTime(dtPicker.SelectedDate.Value.Year, dtPicker.SelectedDate.Value.Month, dtPicker.SelectedDate.Value.Day, HourToIntd(to), MinuteToIntd(to), 0);

                if (st < en)
                {
                    Visit newVisit = new Visit(name.Text, phone.Text, email.Text, notes.Text, newDate, st, en);
                    if(visitsMenager.CheckIfVisitDateCorrect(newVisit))
                    {
                        visitsMenager.AddNewVisit(newVisit);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Niepoprawna godzina!");
                    }                      
                    
                }
                else
                {
                    MessageBox.Show("Niepoprawne dane!");
                }

            }
            else
            {
                MessageBox.Show("Uzupełnij dane!");
            }

        }

        private void Button_Return_Click(object sender, RoutedEventArgs e)
        {
            //anuluje wizyte i zamyka onko
            this.Close();
        }

        private int HourToIntd(System.Windows.Controls.ComboBox c)
        {

            ComboBoxItem typeItem = (ComboBoxItem)c.SelectedItem;
            string value = typeItem.Content.ToString();
            string h = value.Substring(0, 2);
            return int.Parse(h);
        }

        private int MinuteToIntd(System.Windows.Controls.ComboBox c)
        {
            ComboBoxItem typeItem = (ComboBoxItem)c.SelectedItem;
            string value = typeItem.Content.ToString();
            string m = value.Substring(3, 2);
            return int.Parse(m);
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            //ususwa istniejącą wizytę z bazy danych i zamyka okno, możliwe tylko gdy wizyta istnieje (przy edycji np)
            visitsMenager.RemoveVisit(idExist);
            this.Close();
        }
    }
}
