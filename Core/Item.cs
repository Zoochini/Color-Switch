using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Color_Switch.Core
{
    public class Item : CSObject
    {
        public Item(int frameWidth, int frameHeight) : base(frameWidth, frameHeight)
        { 
        }

        public void ReinitialisationRectangleDestination()
        {
            rectangleDestination.X = 0;
            rectangleDestination.Y = 0;
            rectangleDestination.Width = 0;
            rectangleDestination.Height = 0;
        }

        
    }
}
