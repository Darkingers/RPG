using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    enum Target {Flat,Current,Percent}
    enum Intention {Increase=1,Decrease=-1}
    class External_Effect
    {
        protected double Value;
        protected List<Tag> Sender_Tags;
        protected List<Tag> Recievenr_Tags;
        protected List<Tag> Affected_Tags;
        protected Target Target_number;
        protected Intention Modifier;
        protected bool Change_Sign;
        public External_Effect(
            Target target=Target.Flat
           ,Intention intention=Intention.Decrease
           ,double value=0
           ,List<Tag> sender_tags = null
           ,List<Tag> reciever_tags = null
           ,List<Tag> affected_tags = null
           ,bool change_sign=false
           )
        {
            Value = value;
            Target_number = target;
            Modifier = intention;
            Change_Sign = change_sign;
            if (sender_tags == null)
            {
               Sender_Tags = new List<Tag>();
            }
            {
                Sender_Tags = sender_tags;
            }
            if (reciever_tags == null)
            {
                Recievenr_Tags= new List<Tag>();
            }
            {
                Recievenr_Tags = reciever_tags;
            }
            if (affected_tags == null)
            {
                Affected_Tags = new List<Tag>();
            }
            else
            {
                Affected_Tags = affected_tags;
            }

        }

        public void Apply(Stat _stat)
        {
            List<Tag> _tags = _stat.Get_Tags();
            foreach (Tag tag in _tags)
            {
                if (!Affected_Tags.Contains(tag))
                {
                    return;
                }
            }
            if (Value < 0) Value = 0;
            switch (Target_number)
            {
                case Target.Flat: _stat.Modify_Flat(Value*(int)Modifier); break;
                case Target.Percent: _stat.Modify_Percent(Value*(int)Modifier); break;
                case Target.Current: _stat.Modify_Current(Value*(int)Modifier); break;
            }
        }
       
        public void Remove()
        {


        }
        public void Tick()
        {


        }


        public void Modify_Percent(double modifier)
        {
            Value *= modifier;
        }
        public void Modify_Value(double _value)
        {
            Value += _value;
        }
        public void Sign_Changeable()
        {
            Change_Sign = true;
        }
        public List<Tag> Get_Sender_Tags()
        {
            return Sender_Tags;
        }
        public List<Tag> Get_Reciever_Tags()
        {
            return Recievenr_Tags;
        }
    }
}
