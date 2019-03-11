using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RPG
{
    class GameTimer
    {
        static public Timer Updater=new Timer();

        static public void Start(double interval=1)
        {
            if (interval < 0)
            {
                Updater.Interval = 1000;
            }
            else
            {
                Updater.Interval = interval*1000;
            }
            Updater.AutoReset = true;
            Updater.Enabled = true;
        }
        static public void Stop()
        {
            Updater.Enabled = false;
        }
        static public void Continue()
        {
            Updater.Enabled = true;

        }

        static public void Add(ElapsedEventHandler ticker)
        {
            Updater.Elapsed += ticker;
        }
        static public void Remove(ElapsedEventHandler ticker)
        {
            Updater.Elapsed -= ticker;
        }

        static public double Get_Interval()
        {
            return Updater.Interval / 1000;
        }
        static public void Set_Interval(double interval)
        {
            Updater.Interval = interval * 1000;
        }
    }

    
}
