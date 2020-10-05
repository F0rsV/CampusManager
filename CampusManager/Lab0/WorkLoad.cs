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
    public class WorkLoad : INotifyPropertyChanged, ICloneable //IVILID ????
    {
        private Subject subject;
        private ObservableCollection<Group> groups;
        private int workHours;


        public Subject Subject
        {
            get { return subject; }
            set
            {
                
                subject = value;
                OnPropertyChanged("Subject");
            }
        }
        public ObservableCollection<Group> Groups
        {
            get => groups;
            set
            {
                groups = value;
                OnPropertyChanged("Groups");
            }
        }
        public int WorkHours
        {
            get => workHours;
            set
            {
                workHours = value;
                OnPropertyChanged("WorkHours");
            }
        }


        public WorkLoad() { }


        public bool IsValid(Teacher teacher)
        {
            if (teacher == null || Subject == null || WorkHours <= 0) return false;
            
            if ((int)Subject.SubjectType != (int)teacher.Specialization) return false;

            if (teacher.AnualPlan == null) return true;
            int currentWorkHours = 0;
            foreach (WorkLoad workLoad in teacher.AnualPlan)
                currentWorkHours += workLoad.WorkHours;
            if (currentWorkHours + workHours > teacher.MaxWorkHours) return false;


            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        public object Clone()
        {
            Subject currentSubject = new Subject
            {
                Name = this.Subject.Name,
                SubjectType = this.Subject.SubjectType
            };

            ObservableCollection<Group> groupsClone = new ObservableCollection<Group>();

            foreach (Group group in this.Groups)
            {
                groupsClone.Add(group);
            }


            return new WorkLoad
            {
                Subject = currentSubject,
                Groups = groupsClone,
                WorkHours = this.WorkHours
            };
        }
    }
}
