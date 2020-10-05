using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab0
{
    public class TeacherViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Teacher> ListOfTeachers { get; set; }

        private Teacher selectedTeacher;

        private Teacher newTeacher;
        private string newTeacherPosition;
        private string newTeacherSpecialization;

        private readonly Dictionary<string, Position> dictTeacherPosition = new Dictionary<string, Position>()
        {
            {"Асистент", Position.Assistant}, {"Старший викладач", Position.SeniorLecturer},
            {"Доцент", Position.Docent}, {"Професор", Position.Professor}
        };
        private readonly Dictionary<string, Specialization> dictTeacherSpecialization = new Dictionary<string, Specialization>()
        {
            {"Формальні науки", Specialization.Formalist}, {"Природні науки", Specialization.Naturalist},
            {"Соціальні науки", Specialization.Socialist}
        };

        private RelayCommand addTeacherCommand;
        private RelayCommand removeTeacherCommand;


        public Teacher SelectedTeacher
        {
            get { return selectedTeacher; }
            set
            {
                selectedTeacher = value;
                OnPropertyChanged("SelectedTeacher");
            }
        }
        public Teacher NewTeacher
        {
            get => newTeacher;
            set
            {
                newTeacher = value;
                OnPropertyChanged("NewTeacher");
            }
        }

        public string NewTeacherPosition
        {
            get => newTeacherPosition;
            set
            {
                NewTeacher.Position = dictTeacherPosition[value];
                newTeacherPosition = value;
            }
        }
        public string NewTeacherSpecialization
        {
            get => newTeacherSpecialization;
            set
            {
                NewTeacher.Specialization = dictTeacherSpecialization[value];
                newTeacherSpecialization = value;
            }
        }

        public RelayCommand AddTeacherCommand
        {
            get
            {
                return addTeacherCommand ??
                       (addTeacherCommand = new RelayCommand(obj =>
                           {
                               if (NewTeacher.Position == Position.Assistant)
                               {
                                   NewTeacher.RawSalary = 6397;
                                   NewTeacher.Bonus = 0;
                                   NewTeacher.MaxWorkHours = 600;
                               }
                               else if (NewTeacher.Position == Position.SeniorLecturer)
                               {
                                   NewTeacher.RawSalary = 6824;
                                   NewTeacher.Bonus = 0.1;
                                   NewTeacher.MaxWorkHours = 400;
                               }
                               else if (NewTeacher.Position == Position.Docent)
                               {
                                   NewTeacher.RawSalary = 7293;
                                   NewTeacher.Bonus = 0.2;
                                   NewTeacher.MaxWorkHours = 300;
                               }
                               else if (NewTeacher.Position == Position.Professor)
                               {
                                   NewTeacher.RawSalary = 7761;
                                   NewTeacher.Bonus = 0.3;
                                   NewTeacher.MaxWorkHours = 200;
                               }

                               ListOfTeachers.Add(newTeacher);
                               
                               NewTeacher = new Teacher();
                           },
                           obj => NewTeacher.IsValid()));
            }
        }
        public RelayCommand RemoveTeacherCommand
        {
            get
            {
                return removeTeacherCommand ??
                       (removeTeacherCommand = new RelayCommand(obj =>
                           {
                               ListOfTeachers.Remove(SelectedTeacher);
                           },
                           obj => ListOfTeachers.Count > 0 && SelectedTeacher != null));
            }
        }


        public TeacherViewModel()
        {
            ListOfTeachers = new ObservableCollection<Teacher>();
            NewTeacher = new Teacher();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
