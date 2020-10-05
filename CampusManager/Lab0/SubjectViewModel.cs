using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab0
{
    public class SubjectViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Subject> ListOfSubjects { get; set; }

        private Subject selectedSubject;

        private Subject newSubject;
        private string newSubjectType;

        private RelayCommand addSubjectCommand;
        private RelayCommand removeSubjectCommand;


        public Subject SelectedSubject
        {
            get => selectedSubject;
            set
            {
                selectedSubject = value;
                OnPropertyChanged("SelectedSubject");
            }
        }

        public Subject NewSubject
        {
            get => newSubject;
            set
            {
                newSubject = value;
                OnPropertyChanged("NewSubject");
            }
        }
        public string NewSubjectType
        {
            get => newSubjectType;
            set
            {
                NewSubject.SubjectType = dictSubjectType[value];
                newSubjectType = value;
            }
        }

        private readonly Dictionary<string, SubjectType> dictSubjectType = new Dictionary<string, SubjectType>()
        {
            {"Формальний", SubjectType.Formal}, {"Природничий", SubjectType.Natural}, {"Соціальний", SubjectType.Social}
        };

        public RelayCommand AddSubjectCommand
        {
            get
            {
                return addSubjectCommand ??
                       (addSubjectCommand = new RelayCommand(obj =>
                           {
                               ListOfSubjects.Add(NewSubject);
                               NewSubject = new Subject(NewSubject.SubjectType);
                           },
                           obj => NewSubject.IsValid()));
            }
        }
        public RelayCommand RemoveSubjectCommand
        {
            get
            {
                return removeSubjectCommand ??
                       (removeSubjectCommand = new RelayCommand(obj =>
                           {
                               ListOfSubjects.Remove(SelectedSubject);
                           },
                           obj => ListOfSubjects.Count > 0 && selectedSubject != null));
            }
        }


        public SubjectViewModel()
        {
            ListOfSubjects = new ObservableCollection<Subject>();
            NewSubject = new Subject();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
