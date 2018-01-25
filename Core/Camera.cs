using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_Switch.Core
{
    public class Camera
    {
        public Matrix Translation;
        public bool supHalfScreenHeight = false;

        public void Follow(Ball ball)
        {
            if (ball.velocity.Y < 0)
            {
                Translation = Matrix.CreateTranslation(-ball.positionObject.X - (ball.RectangleDestination.Width / 2), -ball.positionObject.Y - (ball.RectangleDestination.Height / 2), 0)
                           * Matrix.CreateTranslation((ColorSwitch.screenWidth / 2), (ColorSwitch.screenHeight / 2), 0);
                supHalfScreenHeight = true;
            }
        }
    }
}
