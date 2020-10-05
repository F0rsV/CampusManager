using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab0
{
    public interface ISerializable
    {
        void LoadDataJson(string filePath);
        void SaveDataJson(string filePath);
    }
}
