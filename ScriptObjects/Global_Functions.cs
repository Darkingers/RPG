using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Global_Functions
    {
        static public object Call_Function(string name,object[] args)
        {
            switch (name)
            {
                case "Popup": System.Windows.Forms.MessageBox.Show((string)args[0]);return null;
                case "Message":Form1.Add_Message((string)args[0]);return null;

            default:return ((Script)Database.Get(name)).Execute(args);break;
            }
        }
    }
}
