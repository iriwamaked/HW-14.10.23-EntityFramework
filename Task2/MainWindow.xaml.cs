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

namespace Task2
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
            List<Department> departments = new List<Department>(){
                new Department(){ Id = 1, Country = "Ukraine", City = "Donetsk" },
                 new Department(){ Id = 2, Country = "Ukraine", City = "Kyiv" },
                 new Department(){ Id = 3, Country = "France", City = "Paris" },
                new Department(){ Id = 4, Country = "UK", City = "London" }
             };
            List<Employee> employees = new List<Employee>(){
                new Employee(){ Id = 1, FirstName = "Tamara", LastName = "Ivanova", Age = 22, DepartmentId = 2 },
                new Employee(){ Id = 2, FirstName = "Nikita", LastName = "Larin", Age = 33, DepartmentId = 1 },
                new Employee() { Id = 3, FirstName = "Alica", LastName = "Ivanova", Age = 43, DepartmentId = 3 },
                new Employee() { Id = 4, FirstName = "Lida", LastName = "Marusyk", Age = 22, DepartmentId = 2 },
                new Employee() { Id = 5, FirstName = "Lida", LastName = "Voron", Age = 36, DepartmentId = 4 },
                new Employee() { Id = 6, FirstName = "Ivan", LastName = "Kalyta", Age = 22, DepartmentId = 2 },
                new Employee() { Id = 7, FirstName = "Nikita", LastName = " Krotov ", Age = 27,DepartmentId = 4 }
             };
            using (EDContext db = new EDContext())
            {
                foreach (var d1 in departments)
                {
                    db.Departments.Add(d1);
                }
                foreach(var e in employees)
                {
                    db.Employees.Add(e);
                }
                await db.SaveChangesAsync();
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializeDb();
        }
        private void FirstTask_Expanded(object sender, RoutedEventArgs e)
        {
            Task1TextBlock.Text = "";
            using (EDContext db = new EDContext()) 
            { 
                var query=db.Employees
                    .Where(e=>e.Department.Country=="Ukraine" && e.Department.City!= "Donetsk")
                    .Select(e=> new {e.FirstName, e.LastName})
                    .ToList();

                foreach (var empl in query)
                {
                    Task1TextBlock.Text += $"{empl.FirstName} {empl.LastName }\n";
                }
            }
        }

        private void SecondTask_Expanded(object sender, RoutedEventArgs e)
        {
            Task2TextBlock.Text = "";
            using (EDContext db = new EDContext())
            {
                var countries=db.Departments.Select(d=>d.Country).Distinct().ToList();

                foreach(var country in countries)
                {
                    Task2TextBlock.Text += $"{country}\n";
                }
            }
        }

        private void ThirdTask_Expanded(object sender, RoutedEventArgs e)
        {
            Task3TextBlock.Text = "";
            using (EDContext db = new EDContext())
            {
                var empl = db.Employees.Where(e=>e.Age>25).Take(3) .ToList();

                foreach (var c in empl)
                {
                    Task3TextBlock.Text += $"{c.FirstName} {c.LastName}, возраст: {c.Age}\n";
                }
            }
        }

        private void FourthTask_Expanded(object sender, RoutedEventArgs e)
        {
            Task4TextBlock.Text = "";
            using (EDContext db = new EDContext())
            {
                var empl = db.Employees.Where
                    (e => e.Department.City=="Kyiv" && e.Age>23)
                    .Select(e => new { e.FirstName, e.LastName, e.Age }).ToList();

                foreach (var c in empl)
                {
                    Task4TextBlock.Text += $"{c.FirstName} {c.LastName}, возраст: {c.Age}\n";
                }
            }
        }
    }
}
