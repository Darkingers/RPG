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
        protected Stat_Resource Target;
        protected Semaphore Lock;

        public Stat_Regeneration(
            Mod source = null
           ,string name = ""
           ,string description = ""
           ,List<Tag> tags = null
           ,double flat = 0
           ,double percent = 0
           ,Stat_Resource target=null
            ) : base(source, name, description, tags, flat, percent)
        {
            Lock = new Semaphore(1, 1);
            Set_Target(target);
        }
        public override Type Clone()
        {
            return new Stat_Regeneration(Source, Name, Description, Tags, 
                Flat, Percent,Target);
        }

        public override void Load(string[] args, ref int iter, Mod _mod)
        {
            for (string token = args[++iter]; token != "[END]"; token = args[++iter])
            {
                switch (token)
                {
                    case "[Stat]": base.Load(args, ref iter, _mod); break;
                    case "[Regenerated]":Set_Target(args[++iter]);break;
                    default: throw new Exception("[Stat_Regeneration]:Invalid token: " + token);
                }
            }
        }
        public override string Save(string tab)
        {
            string temp =
                tab + "[Stat_Regeneration]" + Environment.NewLine +
                base.Save(tab + "  ") + Environment.NewLine +
                tab + "  [Regenerated] "+Target.Get_Identifier()+Environment.NewLine+
                tab + "[END]";
            return temp;
        }
        public override string ToString()
        {
            string temp =
                "[Stat_Regeneration]" + Environment.NewLine +
                base.Save("  ") + Environment.NewLine +
                "  [Regenerated] " + Target.Get_Identifier() + Environment.NewLine +
                "[END]";
            return temp;
        }
        public override string ToLine()
        {
            return Name + "/" + Flat + "/" + Percent;
        }

        public void Bind(List<Stat_Resource> resources)
        {
            if (resources == null) return;
            foreach(Stat_Resource iter in resources)
            {
                if (iter.Equals(Target))
                {
                    Target = iter;
                    GameTimer.Updater.Elapsed += On_Tick;
                    return;
                }
            }
        }
        public void On_Tick(System.Object sender, ElapsedEventArgs e)
        {
            Lock.WaitOne();
            double curr=Target.Get_Current();
            double inter = (GameTimer.Updater.Interval / 1000);
            Target.Modify_Current( Flat+Target.Get_Max()*(double)(Percent/100));
            Lock.Release();
        }

        public Stat_Resource Get_Target()
        {
            return Target;
        }

        public void Set_Target(Stat_Resource target)
        {
            Target = target;
        }
        public void Set_Target(string identifier)
        {
            Target =(Stat_Resource) Database.Get(identifier);
        }
    }
}
