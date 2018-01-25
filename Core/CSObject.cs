using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Color_Switch.Core
{
    public class CSObject
    {
        public Vector2 positionObject;
        public Texture2D textureObject;
        protected Rectangle rectangleSource;
        protected Rectangle rectangleDestination;
        public Rectangle RectangleDestination
        {
            get { return rectangleDestination; }
        }
        public Vector2 velocity;

        
      
        public int frameWidth;       
        public int frameHeight;
        

        public CSObject()
        {
        }

        public CSObject(int frameWidth, int frameHeight)
        {
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;

        }

        public void InitialisationRectangleDestination()
        {
            rectangleDestination = new Rectangle((int)positionObject.X, (int)positionObject.Y, frameWidth, frameHeight);
        }

        public void UpdateRectangleDestination(Vector2 positionObject)
        {
            rectangleDestination.Y = (int)positionObject.Y;
        }

        public void ReinitialisationRectangleDestination()
        {
            rectangleDestination.X = 0;
            rectangleDestination.Y = 0;
            rectangleDestination.Width = 0;
            rectangleDestination.Height = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureObject, rectangleDestination, Color.White);
        }

        public void DrawAnimation(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureObject, rectangleDestination, rectangleSource, Color.White);
            //Vector2 screenCenter = new Vector2(ColorSwitch.screenWidth / 2, ColorSwitch.screenHeight / 2);
           // Vector2 imageCenter = new Vector2(this.frameWidth / 2, frameHeight / 2);
            //spriteBatch.Draw(textureObject, screenCenter, rectangleDestination, rectangleSource, imageCenter, 0f, null, Color.White, SpriteEffects.None, 0f);
        }

    }
}
