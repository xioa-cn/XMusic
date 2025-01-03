using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMusic.Src.MainView.Model
{
    public class Messenger<T>
    {
        public T? data;
    }

    public class ChangeTitle
    {
        public string Title { get; set; }
    }
}
