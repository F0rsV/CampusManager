using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab0
{
    public class WorkLoadViewModel : INotifyPropertyChanged
    {
        private Teacher selectedTeacher;

        private WorkLoad selectedWorkLoad;
        private WorkLoad newWorkLoad;

        private RelayCommand addWorkLoadCommand;
        private RelayCommand removeWorkLoadCommand;


        public Teacher SelectedTeacher
        {
            get => selectedTeacher;
            set
            {
                selectedTeacher = value;
                OnPropertyChanged("SelectedTeacher");
            }
        }

        public WorkLoad SelectedWorkLoad
        {
            get => selectedWorkLoad;
            set
            {
                selectedWorkLoad = value;
                OnPropertyChanged("SelectedWorkLoad");
            }
        }
        public WorkLoad NewWorkLoad
        {
            get => newWorkLoad;
            set
            {
                newWorkLoad = value;
                OnPropertyChanged("NewWorkLoad");
            }
        }

        public RelayCommand AddWorkLoadCommand
        {
            get
            {
                return addWorkLoadCommand ??
                       (addWorkLoadCommand = new RelayCommand(obj =>
                           {
                               WorkLoad workLoad = NewWorkLoad.Clone() as WorkLoad;
                               SelectedTeacher.AnualPlan.Add(workLoad);
                               NewWorkLoad = new WorkLoad();
                           },
                           obj => NewWorkLoad.IsValid(SelectedTeacher)));
            }
        }
        public RelayCommand RemoveWorkLoadCommand
        {
            get
            {
                return removeWorkLoadCommand ??
                       (removeWorkLoadCommand = new RelayCommand(obj =>
                           {
                               Teacher teacher = obj as Teacher;
                               teacher.AnualPlan.Remove(SelectedWorkLoad);
                           },
                           obj => obj != null && SelectedWorkLoad != null));
            }
        }



        public WorkLoadViewModel()
        {
            NewWorkLoad = new WorkLoad();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }


}
