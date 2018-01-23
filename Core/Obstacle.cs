using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Color_Switch.Core
{
    public class Obstacle : CSObject
    {

        private float angle = 0;



        public Obstacle(int frameWidth, int frameHeight) : base(frameWidth, frameHeight)
        {
            rectangleSource = new Rectangle(0, 0, frameWidth, frameHeight);
        }


        public void Rotate()
        {
            angle += 0.04f;
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(textureObject.Width/2, textureObject.Height/2);

            spriteBatch.Draw(textureObject, positionObject, rectangleSource, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);
        }
    }
}
