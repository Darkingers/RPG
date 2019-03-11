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

        public Player(
            Mod source = null
           ,string name = ""
           ,string description = ""
           ,List<Tag> tags = null
           ,Bitmap image = null
           ,Cooldown speed = null
           ,MovementMode movement = MovementMode.Walk
           ,int sight = 3
           ,Tile position = null
           ,Direction orientation = Direction.Right
           ,List<Stat> stats=null
           ,List<Stat_Resource> resources = null
           ,List<Stat_Modifier> modifiers = null
           ,List<Stat_Regeneration> regeneration = null
           ,Dictionary<Slot, Item> equipment = null
           ,List<Item> _inventory = null
           ) : base(source, name, description, tags,image,movement,speed,sight,position,orientation,stats,resources,modifiers,regeneration,equipment,_inventory)
        {

        }
        public override Type Clone()
        {
            return new Player(Source, Name, Description, Tags, Graphics_Image, Speed,Movement, Sight, Position, Orientation,Stats, Resources, Modifiers, Regenerators, Equipment, Inventory);
        }

    }
}
