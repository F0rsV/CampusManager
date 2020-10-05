using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab0
{
    public enum SubjectType { Formal, Natural, Social }
    public class Subject : INotifyPropertyChanged, IValid
    {
        private string name;
        private SubjectType subjectType;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public SubjectType SubjectType
        {
            get { return subjectType; }
            set
            {
                subjectType = value;
                OnPropertyChanged("Type");
            }
        }

        

        public Subject() { }

        public Subject(SubjectType subjectType)
        {
            this.subjectType = subjectType;
        }


        public bool IsValid()
        {
            return !String.IsNullOrEmpty(Name);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
