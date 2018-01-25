using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_Switch.Core
{
    public class Score : CSObject
    {

        private bool hasCollidedScore;
        public bool HasCollidedScore
        {
            get { return hasCollidedScore; }
        }

        public int ScoreCompteur { get => scoreCompteur; set => scoreCompteur = value; }

        private int scoreCompteur;

        public Score(int frameWidth, int frameHeight) : base(frameWidth, frameHeight)
        {
            ScoreCompteur = 0;
        }

        public void CollisionBall(Ball ball)
        {
            if (rectangleDestination.Intersects(ball.RectangleDestination))
            {
                ReinitialisationRectangleDestination();
                hasCollidedScore = true;
                ScoreCompteur++;
            }
        }
    }
}
