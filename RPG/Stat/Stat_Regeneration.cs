using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace RPG
{
    class Stat_Regeneration:Stat
    {
        protected Stat_Resource Target;//Reference
        protected Semaphore Lock;//Contains
        protected double Elapsed = 0;//TODO

        public Stat_Regeneration()
        {
            Lock = new Semaphore(1, 1);
            Set_Target(null);
        }
        public Stat_Regeneration(Stat stat,Stat_Resource target ):base(stat)
        {
            Lock = new Semaphore(1, 1);
            Copy(target);
        }
        public Stat_Regeneration(Stat_Regeneration cloned)
        {
            Lock = new Semaphore(1, 1);
            Copy(cloned);
        }
        public bool Copy(Stat_Regeneration copied)
        {
           return base.Copy(copied) && Copy(copied.Get_Target());
        }
        public bool Copy(Stat_Resource target)
        {
            return 
            Set_Target(target);
        }
        public override ScriptObject Clone()
        {
            return new Stat_Regeneration(this);
        }
        public override bool Assign(Record copied)
        {
            return Copy((Stat_Regeneration)copied);
        }

        public bool Bind(List<Stat_Resource> resources)
        {
            if (resources == null) return false;
            foreach(Stat_Resource iter in resources)
            {
                if (iter.Equals(Target))
                {
                    Target = iter;
                    GameTimer.Updater.Elapsed += On_Tick;
                    return true;
                }
            }
            return false;
        }
        public void On_Tick(System.Object sender, ElapsedEventArgs e)
        {
            Lock.WaitOne();
            double curr=Target.Get_Current();
            double inter = (GameTimer.Updater.Interval / 1000);
            Elapsed += GameTimer.Get_Interval();
            if (Elapsed >= 1)
            {
                Elapsed = 0;
                Target.Modify_Current((Flat + Target.Get_Max() * (double)(Percent / 100)));
            }
            Lock.Release();
        }

        public Stat_Resource Get_Target()
        {
            return Target;
        }

        public bool Set_Target(Stat_Resource target)
        {
            Target = target;
            return true;
        }

        public override bool Set_Variable(string name, object value)
        {
            switch (name)
            {
                case "Target": return Set_Target((Stat_Resource)value);
                default: return base.Set_Variable(name, value);
            }
        }
        public override object Get_Variable(string name)
        {
            switch (name)
            {
                case "Target":return Get_Target();
                default: return base.Get_Variable(name);
            }
        }
        public override object Call_Function(string name, object[] args)
        {
            switch (name)
            {
                case "Bind": return Bind((List<Stat_Resource>)args[0]);
                case "Tick": On_Tick(null,null);return null;
                default: return base.Call_Function(name, args);
            }
        }
    }
}
