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

        
      
        protected int frameWidth;       
        protected int frameHeight;
        

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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureObject, rectangleDestination, Color.White);
        }

        public void DrawAnimation(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureObject, rectangleDestination, rectangleSource, Color.White);
        }

    }
}
