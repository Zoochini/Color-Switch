using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Color_Switch.Core
{
    public class Ball : CSObject
    {

        private bool Hold;
        protected bool hasJumped;

        public enum Sprites
        {
            Yellow = 0,
            Red = 1,
            Blue = 2,
            Green = 3
        }


        public Sprites spriteIndex;

        private bool hasCollided;
        public bool HasCollided
        {
            get { return hasCollided; }
        }


        public Ball(int frameWidth, int frameHeight) : base(frameWidth, frameHeight)
        {
            spriteIndex = Sprites.Yellow;
            hasCollided = false;
            rectangleSource = new Rectangle(0, 0, frameWidth, frameHeight);
        }



        public void UpdateColor(GameTime gameTime, Item item)
        {
            if (rectangleDestination.Intersects(item.RectangleDestination))
            {
                Random rnd = new Random();
                int random = rnd.Next(0, 4);
                switch (random)
                {
                    case 0:
                        spriteIndex = Sprites.Yellow;
                        break;
                    case 1:
                        spriteIndex = Sprites.Red;
                        break;
                    case 2:
                        spriteIndex = Sprites.Blue;
                        break;
                    case 3:
                        spriteIndex = Sprites.Green;
                        break;

                }
                rectangleSource.X = (int)spriteIndex * frameWidth;
                item.ReinitialisationRectangleDestination();
                hasCollided = true;

            }
        }


        public void Jump(GameTime time)
        {
            positionObject += velocity;
            rectangleDestination.Y = (int)positionObject.Y;
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !Hold) //IsKeyDown = Gets whether given key is currently being pressed. 
            { 
                positionObject.Y -= 8f;
                velocity.Y = -5f;
                rectangleDestination.Y = (int)positionObject.Y;
                hasJumped = true;
                Hold = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Space)) //IsKeyUp=Gets whether given key is currently being not pressed. 
            {
                Hold = false;
            }
            if (hasJumped)
                velocity.Y += 0.25f;


        }
    }
}
