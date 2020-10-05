using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Newtonsoft.Json;
using System.Xml.Serialization;


namespace Lab0
{
    public class Secretary : INotifyPropertyChanged, ISerializable //ApplicationViewModel
    {
        private static readonly double GovTaxes = 0.12;

        public TeacherViewModel TeacherVm { get; set; }
        public SubjectViewModel SubjectVm { get; set; }
        public GroupViewModel GroupVm { get; set; }
        public WorkLoadViewModel WorkLoadVm { get; set; }


        public Secretary()
        {
            TeacherVm = new TeacherViewModel();
            SubjectVm = new SubjectViewModel();
            GroupVm = new GroupViewModel();
            WorkLoadVm = new WorkLoadViewModel();
        }


        public void LoadDataJson(string filePath)
        {
            using (StreamReader file = File.OpenText(filePath))
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                JsonSerializer serializer = JsonSerializer.Create(settings);

                Secretary deserializedSecretary = (Secretary) serializer.Deserialize(file, typeof(Secretary));

                TeacherVm.ListOfTeachers = deserializedSecretary.TeacherVm.ListOfTeachers;
                SubjectVm.ListOfSubjects = deserializedSecretary.SubjectVm.ListOfSubjects;
                GroupVm.ListOfGroups = deserializedSecretary.GroupVm.ListOfGroups;
            }
        }
        public void SaveDataJson(string filePath)
        {
            using (StreamWriter file = File.CreateText(filePath))
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                };
                JsonSerializer serializer = JsonSerializer.Create(settings);

                serializer.Serialize(file, this);
            }
        }

        public static double GetSalary(Teacher teacher)
        {
            return teacher.RawSalary + teacher.RawSalary * teacher.Bonus - teacher.RawSalary * GovTaxes;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}