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
    public class GroupViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Group> ListOfGroups { get; set; }
        public ObservableCollection<Group> ListOfTempGroups { get; set; }

        private Group selectedGroup;
        private Group newGroup;

        private RelayCommand addGroupCommand;
        private RelayCommand addTempGroupCommand;
        private RelayCommand removeGroupCommand;
        private RelayCommand removeTempGroupCommand;




        public Group SelectedGroup
        {
            get { return selectedGroup; }
            set
            {
                selectedGroup = value;
                OnPropertyChanged("SelectedGroup");
            }
        }
        public Group NewGroup
        {
            get => newGroup;
            set
            {
                newGroup = value;
                OnPropertyChanged("NewGroup");
            }
        }

        public RelayCommand AddGroupCommand
        {
            get
            {
                return addGroupCommand ??
                       (addGroupCommand = new RelayCommand(obj =>
                           {
                               ListOfGroups.Add(NewGroup);
                               NewGroup = new Group();
                           },
                           obj => NewGroup.IsValid()));
            }
        }
        public RelayCommand AddTempGroupCommand
        {
            get
            {
                return addTempGroupCommand ??
                       (addTempGroupCommand = new RelayCommand(obj =>
                           {
                               WorkLoad newWorkLoad = obj as WorkLoad;
                               ListOfTempGroups.Add(SelectedGroup);
                               newWorkLoad.Groups = ListOfTempGroups;
                           },
                           obj => SelectedGroup != null));
            }
        }
        public RelayCommand RemoveGroupCommand
        {
            get
            {
                return removeGroupCommand ??
                       (removeGroupCommand = new RelayCommand(obj =>
                           {
                               ListOfGroups.Remove(SelectedGroup);
                           },
                           obj => ListOfGroups.Count > 0 && SelectedGroup != null));
            }
        }
        public RelayCommand RemoveTempGroupCommand
        {
            get
            {
                return removeTempGroupCommand ??
                       (removeTempGroupCommand = new RelayCommand(obj => { ListOfTempGroups.Clear(); },
                           obj => ListOfTempGroups.Count > 0));
            }
        }


        public GroupViewModel()
        {
            ListOfGroups = new ObservableCollection<Group>();
            ListOfTempGroups = new ObservableCollection<Group>();
            NewGroup = new Group();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
