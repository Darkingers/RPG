using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Player:Entity
    {
        public Player():base()
        {


        }
        public Player(Player cloned)
        {
            Copy(cloned);
        }
        public Player(Entity type) : base(type)
        {

        }

        public bool Copy(Player copied)
        {
            return base.Copy(copied);
        }
        public override ScriptObject Clone()
        {
            return new Player(this);
        }

    }
}
