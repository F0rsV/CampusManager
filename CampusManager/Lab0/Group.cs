using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace Lab0
{
    public class Group : INotifyPropertyChanged, IValid, IDataErrorInfo
    {       
        private string name;
        private int numOfStudents;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public int NumOfStudents
        {
            get { return numOfStudents; }
            set
            {
                numOfStudents = value;
                OnPropertyChanged("NumOfStudents");
            }
        }


        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                            break;

                        Regex groupRegex = new Regex(@"\w{2}-\d{2}");
                        if (!groupRegex.IsMatch(Name))
                        {
                            error = "Назва групи має мати формат 'СС-DD'(С - символ, D - цифра)";
                        }
                        break;
                    case "NumOfStudents":
                        if ((NumOfStudents < 0) || (NumOfStudents > 30))
                        {
                            error = "Кількість студентів має бути більше 0 і менше 30";
                        }
                        break;
                }
                return error;
            }
        }
        public string Error { get; }



        public Group() { }

        public Group(string name, int numOfStudents)
        {
            this.name = name;
            this.numOfStudents = numOfStudents;
        }


        public bool IsValid()
        {
            if (String.IsNullOrEmpty(Name)) return false;
            Regex groupRegex = new Regex(@"\w{2}-\d{2}");
            if (!groupRegex.IsMatch(Name)) return false;

            if (NumOfStudents <= 0 || NumOfStudents > 30) return false;


            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
