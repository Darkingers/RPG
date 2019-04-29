using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace RPG
{
    class Cooldown : ScriptObject
    {
        protected double Time;
        protected double Time_Left;
        protected Func<object, object> EndFunction;

        protected bool Done;
        protected Semaphore Lock;

        public Cooldown()
        {
            Lock = new Semaphore(1, 1);
            Copy(0, 0, null);

        }
        public Cooldown(ScriptObject scriptobject, double time, double time_left, Func<object, object> endfunction) : base(scriptobject)
        {
            Lock = new Semaphore(1, 1);
            Copy(time, time_left, endfunction);
        }
        public Cooldown(Cooldown cloned)
        {
            Lock = new Semaphore(1, 1);
            Copy(cloned);
            
        }
        public bool Copy(Cooldown copied)
        {
            return base.Copy(copied)&&Copy(copied.Get_Time(), copied.Get_Time_Left(), copied.Get_EndFunction());
        }
        public bool Copy(double time, double time_left, Func<object, object> endfunction)
        {
            return 
            Set_Time(time) &&
            Set_Time_Left(time)&&
            Set_EndFunction(endfunction);
        }
        public override ScriptObject Clone()
        {
            return new Cooldown(this);
        }

        public bool Start()
        {
            return Set_Time_Left(Time);
        }
        public bool Stop()
        {
            GameTimer.Remove(Tick);
            return true;
        }
        public bool Continue()
        {
            GameTimer.Add(Tick);
            return true;
        }
        public void Tick(System.Object sender, ElapsedEventArgs e)
        {
            Lock.WaitOne();
            Time_Left -= GameTimer.Updater.Interval / 1000;
            if (Time_Left < 0)
            {
                Set_Time_Left(0);
            }
            Lock.Release();
        }

        public bool Get_Done()
        {
            return Done;
        }
        public double Get_Time()
        {
            return Time;
        }
        public double Get_Time_Left()
        {
            return Time_Left;
        }
        public Func<object, object> Get_EndFunction()
        {
            return EndFunction;
        }

        public bool Set_EndFunction(Func<object, object> endfunction)
        {
            EndFunction = endfunction;
            return true;
        }
        public bool Set_Time(double time)
        {
            if (time < 0)
            {
                time = 0;
                return false;
            }
            else
            {
                Time = time;
                return true;
            }
        }
        public bool Set_Time_Left(double time)
        {
            if (time <= 0)
            {
                Done = true;
                Time_Left = 0;
                GameTimer.Remove(Tick);
                if (EndFunction != null)
                {
                    EndFunction(null);
                }
                
                return true;
            }
            else
            {
                Time_Left = time;
                Done = false;
                GameTimer.Add(Tick);
                return true;
            } 
        }

        public override string ToString()
        {
            return Time+"/"+Time_Left;
        }
        public override bool Set_Variable(string name, object value)
        {
            switch (name)
            {
                case "Time": return Set_Time((double)value);
                case "Time_Left": return Set_Time_Left((double)value);
                case "EndFunction": return Set_EndFunction((Func<object, object>)value);
                default: return base.Set_Variable(name, value);
            }
        }
        public override object Get_Variable(string name)
        {
            switch (name)
            {
                case "Time": return Get_Time();
                case "Time_Left": return Get_Time_Left();
                case "Done": return Get_Done();
                case "EndFunction": return Get_EndFunction();
                default: return base.Get_Variable(name);

            }
        }
        public override int Compare(object compared)
        {
            if (((Cooldown)compared).Equals(this))
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        public override object Call_Function(string name, object[] args)
        {
            switch (name)
            {
                case "Is_Done": return Get_Done();
                case "Start": return Start();
                case "Stop": return Stop();
                case "Continue": return Continue();
                case "Tick": Tick(null, null); return null;
                default: return base.Call_Function(name, args);
            }
        }
    }
}
