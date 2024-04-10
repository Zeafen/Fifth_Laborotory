using Final_Practice.Commands;
using Final_Practice.database;
using Final_Practice.serializers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Final_Practice.ViewModels.AdminPagesViewModels
{
    public class EmployeesViewModel : INotifyPropertyChanged
    {
        private RelayCommand<ICollection<Employees>> _saveEmployeesCommand = null;
        public RelayCommand<ICollection<Employees>> saveEmployeesCommand => _saveEmployeesCommand ?? (_saveEmployeesCommand = new RelayCommand<ICollection<Employees>>(SaveEmployees, (employees) => employees != null && employees.Count > 0));


        private RelayCommand<Employees> _addEmployeeCommand = null;
        public RelayCommand<Employees> addEmployeeCommand => _addEmployeeCommand ?? (_addEmployeeCommand = new RelayCommand<Employees>(AddEmployee, 
            (employee) => !string.IsNullOrEmpty(employee?.Employee_Name) && !string.IsNullOrEmpty(employee?.Employee_Surname) && employee?.Employee_Age != null && Employees.FirstOrDefault(e => e.ID_Employee == employee.ID_Employee) == null));

        private RelayCommand<Employees> _deleteEmployeeCommand = null;
        public RelayCommand<Employees> deleteEmployeeCommand => _deleteEmployeeCommand ?? (_deleteEmployeeCommand = new RelayCommand<Employees>(DeleteEmployee, (employee) => employee != null && Employees.FirstOrDefault(e => e.ID_Employee == employee?.ID_Employee) != null));

        private RelayCommand<Employees> _updateEmployeeCommand = null;
        public RelayCommand<Employees> updateEmployeeCommand => _updateEmployeeCommand ?? (_updateEmployeeCommand = new RelayCommand<Employees>(UpdateEmployee, (employee) => employee != null && Employees.FirstOrDefault(e => e.ID_Employee == employee?.ID_Employee) != null));



        private Employees _CurrentEmployee { get; set; }
        public Employees CurrentEmployee
        {
            get => _CurrentEmployee;
            set
            {
                _CurrentEmployee = value;
                OnPropertyChanged();
            }
        }

        private TeaShopDBContext ctx;
        public ObservableCollection<Employees> Employees { get; set; }
        public ObservableCollection<Accounts> Accounts { get; set; }
        public EmployeesViewModel()
        {
            ctx = new TeaShopDBContext();
            Employees = new ObservableCollection<Employees>(ctx.Employees.ToList());
            Accounts = new ObservableCollection<Accounts>(ctx.Accounts.ToList());
        }


        public void SaveEmployees(ICollection<Employees> employees)
        {
            try
            {
                Employee_Serializer.Serialize(employees, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Employees.json");
                MessageBox.Show("Successfully saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void LoadEmployees()
        {
            try
            {
                var dlg = new OpenFileDialog();
                if (dlg.ShowDialog() ?? false)
                    if (dlg.FileName == string.Empty || !dlg.FileName.EndsWith(".json")) return;
                string path = dlg.FileName;
                var employees = Employee_Serializer.Deserialize(path);
                if (employees == default) throw new ArgumentException("There aren't required data in this file");
                foreach (var employee in employees)
                {
                    if (ctx.Employees.FirstOrDefault(e => e.Account_ID == employee.Account_ID) == null)
                    {

                        ctx.Employees.Add(employee);
                        Employees.Add(employee);
                    }
                }
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ClearInfo()
        {
            CurrentEmployee = new Employees();
        }
        public void AddEmployee(Employees newEmployee)
        {
            try
            {
                if (newEmployee == null) throw new ArgumentNullException("Cannot add null value");
                ctx.Employees.Add(newEmployee);
                ctx.SaveChanges();
                Employees.Add(newEmployee);
                CurrentEmployee = new Employees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateEmployee(Employees employee)
        {
            try
            {
                if (employee == null) throw new ArgumentNullException("Cannot add null value");
                var updatedEmployee = ctx.Employees.FirstOrDefault(r => r.ID_Employee == employee.ID_Employee);
                var arrayUpdatedEmployee = Employees.FirstOrDefault(r => r.ID_Employee == employee.ID_Employee);
                if (updatedEmployee != null)
                {
                    updatedEmployee = employee;
                    ctx.SaveChanges();
                    arrayUpdatedEmployee = employee;
                }
                CurrentEmployee = new Employees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DeleteEmployee(Employees employee)
        {
            try
            {
                if (employee == null) throw new ArgumentNullException("Cannot add null value");
                foreach (var collection in ctx.Collections)
                {
                    if (collection.Employee_ID == employee.ID_Employee)
                        ctx.Collections.FirstOrDefault(c => c.ID_Collection == collection.ID_Collection).Employee_ID = null;
                }
                foreach (var order in ctx.Orders)
                {
                    if (order.Emplyoee_ID == employee.ID_Employee)
                        ctx.Orders.FirstOrDefault(c => c.ID_Order == order.ID_Order).Emplyoee_ID = null;
                }
                ctx.Employees.Remove(employee);
                ctx.SaveChanges();
                Employees.Remove(employee);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
