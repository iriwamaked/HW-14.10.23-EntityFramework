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

namespace HW_14._10._23
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }

        public async Task InitializeDb()
        {
            List<Person> person = new List<Person>()
            {
                new Person(){ Name = "Andrey", Age = 24, City = "Kyiv" },
                new Person(){ Name = "Liza", Age = 18, City = "Kryvyi Rih" },
                new Person(){ Name = "Oleg", Age = 15, City = "London" },
                new Person(){ Name = "Sergey", Age = 55, City = "Kyiv" },
                new Person(){ Name = "Sergey", Age = 32, City = "Kyiv" }
            };
            using (PersonContext db= new PersonContext()) 
            { 
                foreach(var p1 in person) 
                { 
                   db.Persons.Add(p1);
                }
                await db.SaveChangesAsync();
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializeDb();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Results.Text = "Люди, старше 25 лет:\n";
            using (PersonContext db = new PersonContext())
            {
                var query = db.Persons.Where(p => p.Age > 25).ToList();
                foreach (var p in query)
                {
                    Results.Text += $"{p.Name} - {p.Age} лет\n";
                }
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Results.Text = "Люди, проживающие не в Киеве:\n";
            using (PersonContext db=new PersonContext())
            {
                
                var query=db.Persons.Where(p=>p.City!="Kyiv").ToList();
                foreach(var p in query)
                {
                    Results.Text += $"{p.Name}, {p.Age}, {p.City}\n";
                }
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Results.Text = "Имена людей, проживающих в Киеве:\n";
            using (PersonContext db=new PersonContext())
            {
                var query = db.Persons.Where(p=>p.City=="Kyiv").ToList();
                foreach(var p in query)
                {
                    Results.Text += $"{p.Name}\n";
                }
            }
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            Results.Text = "Выбрать людей старших 35 лет с именем Sergey:\n";
            using (PersonContext db = new PersonContext())
            {
                var query = db.Persons.Where(p => p.Age>35 && p.Name=="Sergey" ).ToList();
                foreach (var p in query)
                {
                    Results.Text += $"\n{p.Name}, {p.Age}, {p.City}\n";
                }
            }
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            Results.Text = "Выбрать людей, проживающих в Кривом Роге:\n";
            using (PersonContext db = new PersonContext())
            {
                var query = db.Persons.Where(p => p.City == "Kryvyi Rih").ToList();
                foreach (var p in query)
                {
                    Results.Text += $"\n{p.Name}, {p.Age}, {p.City}\n";
                }
            }
        }
    }
}
