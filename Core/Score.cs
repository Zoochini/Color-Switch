using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_Switch.Core
{
    public class Score : CSObject
    {

        bool hasCollidedScore; //servira à faire disparaitre graphiquement la hitbox de l'item dans la méthode Draw de ColorSwitch.cs lorsque la balle l'aura touchée
        int scoreCompteur; //Score total du joueur durant la partie

        public int ScoreCompteur { get => scoreCompteur; set => scoreCompteur = value; }
        public bool HasCollidedScore { get => hasCollidedScore; set => hasCollidedScore = value; }

        public Score(int Width, int Height) : base(Width, Height)
        {
            ScoreCompteur = 0;
        }

        //Gère la collision de l'objet avec la balle

        public void CollisionBall(Ball ball)
        {
            if (rectangleDestination.Intersects(ball.RectangleDestination))
            {
                ReinitialisationRectangleDestination(); //on fait disparaitre physiquement la hitbox de le l'objet
                hasCollidedScore = true; 
                ScoreCompteur++;
            }
        }
    }
}
