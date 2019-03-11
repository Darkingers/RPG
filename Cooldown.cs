using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace RPG
{
        class Cooldown
        {
            protected double Time;
            protected double Time_Left;
            protected bool Done;

            protected Semaphore Lock;

            public Cooldown(double time=0)
            {
                Set_Time(time);
                Set_Time_Left(Time);
                Lock = new Semaphore(1, 1);
            }
            public Cooldown(Cooldown cloned)
            {
                
                Set_Time(cloned.Get_Time());
                Set_Time_Left(cloned.Get_Time_Left());
                Lock = new Semaphore(1, 1);
            }
            public Cooldown Clone()
            {
                return new Cooldown(Time);
            }

            public void Load(string[] args, ref int iter)
            {
                for (string token = args[++iter]; token != "[END]"; token = args[++iter])
                {
                    switch (token)
                    {
                         case "[Time]": Set_Time(double.Parse(args[++iter]));break;
                         case "[Time_Left]": Set_Time_Left(double.Parse(args[++iter])); break;
                         default: throw new Exception("[Cooldown]:Invalid token: " + token); 
                    }
                }
            }
            public string Save(string tab)
            {
                 return tab + ToString();
            }
            public override string ToString()
            {
                return "[Cooldown]"+" [Time] "+Time+" [Time_Left] " +Time_Left+" [END]";
            }

            public bool Is_Done()
            {
                return Done;
            }
            public void Start()
            {
                 Set_Time_Left(Time);
            }
            public void Stop()
            {
                  GameTimer.Remove(Tick);
            }
            public void Continue()
            {
                 GameTimer.Add(Tick);
            }
            public void Tick(System.Object sender, ElapsedEventArgs e)
            {
                Lock.WaitOne();
                Time_Left -= GameTimer.Updater.Interval/1000;
                if (Time_Left < 0)
                {
                     Set_Time_Left(0);
                }
                Lock.Release();
            }

            public double Get_Time()
        {
            return Time;
        }
            public double Get_Time_Left()
            {
                return Time_Left;
            }

            public void Set_Time(double time)
            {
                if (time < 0) time = 0;
                Time = time;
            }
            public void Set_Time_Left(double time)
            {
                 if (time <= 0)
                 {
                     Done = true;
                     Time_Left = 0;
                     GameTimer.Remove(Tick);
                 }
                 else
                 {
                      Time_Left = time;
                      Done = false;
                      GameTimer.Add(Tick);
                 }
            }
        }   
    }
