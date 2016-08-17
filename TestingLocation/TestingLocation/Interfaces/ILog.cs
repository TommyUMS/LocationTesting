using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingLocation.Interfaces
{
    public interface ILog
    {
        void addText(string filename, string text);
        string LoadText(string filename);
    }
}
