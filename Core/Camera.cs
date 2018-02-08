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
        Matrix translation;
        bool supHalfScreenHeight = false; //pour vérifier que la balle a atteint le centre de l'écran et activer la caméra. Servira dans la méthode Draw de ColorSwitch.cs

        public Matrix Translation { get => translation; set => translation = value; }
        public bool SupHalfScreenHeight { get => supHalfScreenHeight; set => supHalfScreenHeight = value; }

        //Utilisation de la caméra centrée sur la balle

        public void Follow(Ball ball)
        {
            if (ball.Velocity.Y < 0) //La caméra ne suivra la balle que si cette dernière monte
            {
                Translation = Matrix.CreateTranslation(-ball.PositionObject.X - (ball.RectangleDestination.Width / 2), -ball.PositionObject.Y - (ball.RectangleDestination.Height / 2), 0)
                           * Matrix.CreateTranslation((ColorSwitch.screenWidth / 2), (ColorSwitch.screenHeight / 2), 0);
                supHalfScreenHeight = true; //la balle ayant atteint le centre de l'écran, on la met à true pour activer la caméra
            }
        }
    }
}
