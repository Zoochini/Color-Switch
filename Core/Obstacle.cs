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

        private float angle = 0; //pour modifier l'angle de rotation de l'obstacle. Servira dans la méthode Rotate()
        private double time=0; //pour réinitialiser le temps écoulé après un changement de déplacement de l'obstacle. Servira dans la méthode Move()
        bool right = false;  //pour savoir si l'obstacle se déplacait vers la gauche ou vers la droite. Servira dans la méthode Move()
           




        public Obstacle(int Width, int Height) : base(Width, Height)
        {
            rectangleSource = new Rectangle(0, 0, Width, Height);
        }

        //Gère la rotation de l'obstacle si celui-ci est un carré ou un cercle

        public void Rotate()
        {
            angle += 0.04f;
        }

        /*Gère le mouvement horizontal de l'obstacle lorsque celui-ci est une droite
         * gameTime permet de changer la direction du déplacement de l'obstacle après un certain temps*/

        public void Move(GameTime gameTime)
        {
            velocity.X = 1.5f;
            time += gameTime.ElapsedGameTime.TotalSeconds; //on ajoute le temps écoulé à time
       
            if (time<5.0 && right) //si le déplacement dure depuis 5 secondes et que l'obstacle se déplace vers la droite, on le fait déplacer vers la gauche
            {
                positionObject -= velocity;
                positionObject.X -= velocity.X;
                rectangleDestination.X = (int)positionObject.X;

            }           
            
            else
            {
                if (time >= 5.0 && !right) //si le déplacement dure 5 secondes ou plus, on réinitialise le temps écoulé et on change la direction du mouvement de l'obstacle
                {
                    time = 0.0;
                    right = true;
                }
                else
                {
                    if (time >= 5.0 && right) //si le déplacement dure 5 secondes ou plus, on réinitialise le temps écoulé et on change la direction du mouvement de l'obstacle
                    {
                        time = 0.0;
                        right = false;
                    }
                    else
                    {
                        if(time<5.0 && !right) //si le déplacement dure depuis 5 secondes et que l'obstacle se déplace vers la gauche, on le fait déplacer vers la droite
                        {
                            PositionObject += velocity;
                            positionObject.X += velocity.X;
                            rectangleDestination.X = (int)PositionObject.X;
                        }

                    }
                }

            }
               
        }

        //pour gérer le déplacement lorsque l'obstacle fait une rotation

        public void DrawRotate(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(textureObject.Width / 2, textureObject.Height / 2);

            spriteBatch.Draw(textureObject, positionObject, rectangleSource, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);
        }
    }
}
