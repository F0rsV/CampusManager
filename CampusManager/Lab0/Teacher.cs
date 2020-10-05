using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab0
{
    public enum Specialization
    {
        Formalist,
        Naturalist,
        Socialist
    }

    public enum Position
    {
        Assistant,
        SeniorLecturer,
        Docent,
        Professor
    }

    public class Teacher : INotifyPropertyChanged, IValid, IDataErrorInfo
    {
        private ObservableCollection<WorkLoad> anualPlan;
        private Position position;
        private Specialization specialization;
        private string fullName;
        private string phoneNumber;
        private string mail;

        private double rawSalary;
        private double bonus;
        private int maxWorkHours;



        public ObservableCollection<WorkLoad> AnualPlan
        {
            get { return anualPlan; }
            set
            {
                anualPlan = value;
                OnPropertyChanged("AnualPlan");
            }
        }
        public Position Position
        {
            get => position;
            set
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }
        public Specialization Specialization
        {
            get => specialization;
            set
            {
                specialization = value;
                OnPropertyChanged("Specialization");
            }
        }
        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;
                OnPropertyChanged("FullName");
            }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        public string Mail
        {
            get => mail;
            set
            {
                mail = value;
                OnPropertyChanged("Mail");
            }
        }
        public double RawSalary
        {
            get => rawSalary;
            set
            {
                rawSalary = value;
                OnPropertyChanged("RawSalary");
            }
        }
        public double Bonus
        {
            get => bonus;
            set
            {
                bonus = value;
                OnPropertyChanged("Bonus");
            }
        }
        public int MaxWorkHours
        {
            get => maxWorkHours;
            set
            {
                maxWorkHours = value;
                OnPropertyChanged("MaxWorkHours");
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "PhoneNumber":
                        if (string.IsNullOrEmpty(PhoneNumber))
                            break;

                        var phoneRegex = new Regex(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$");
                        if (!phoneRegex.IsMatch(PhoneNumber))
                        {
                            error = "Введено недійсний номер";
                        }
                        break;
                    case "Mail":
                        if (string.IsNullOrEmpty(Mail))
                            break;

                        var mailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                        if (!mailRegex.IsMatch(Mail))
                        {
                            error = "Введено недійсну пошту";
                        }
                        break;
                }
                return error;
            }
        }
        public string Error { get;  }


        public Teacher()
        {
            AnualPlan = new ObservableCollection<WorkLoad>();
        }


        public Teacher(Specialization specialization, string fullName, string phoneNumber, string mail)
        {
            AnualPlan = new ObservableCollection<WorkLoad>();

            this.specialization = specialization;
            this.fullName = fullName;
            this.phoneNumber = phoneNumber;
            this.mail = mail;
        }


        public virtual bool IsValid()
        {
            if (string.IsNullOrEmpty(FullName)) return false;

            if (string.IsNullOrEmpty(PhoneNumber)) return false;
            var phoneRegex = new Regex(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$");
            if (!phoneRegex.IsMatch(PhoneNumber)) return false;

            if (String.IsNullOrEmpty(Mail)) return false;
            var mailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!mailRegex.IsMatch(Mail)) return false;


            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}