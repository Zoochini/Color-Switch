using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Color_Switch.Core
{
    public class Ball : CSObject
    {

        private bool Hold; //pour empêcher la balle de monter si le bouton 'Espace' reste enfoncé. Servira dans la méthode Jump()
        bool hasCollidedItem; //servira à faire disparaitre graphiquement la hitbox de l'item dans la méthode Draw de ColorSwitch.cs lorsque la balle l'aura touchée. Servira dans la méthode Jump()
        bool hasJumped; //pour vérifier que la balle a bien sautée afin de gérer la gravité. Servira dans la méthode Jump()
        Sprites spriteIndex; //prendra une valeur de Sprites, afin de calculer la position d'un sprite de la balle. Servira dans la méthode UpdateColor()


        protected enum Sprites
        {
            Yellow = 0,
            Red = 1,
            Blue = 2,
            Green = 3
        }


        public bool HasCollidedItem { get => hasCollidedItem; set => hasCollidedItem = value; }

        public Ball(int Width, int Height) : base(Width, Height)
        {
            spriteIndex = Sprites.Yellow;
            hasCollidedItem = false;
            rectangleSource = new Rectangle(0, 0, Width, Height);
        }

        //Gère la collision avec l'item et le changement de couleur (qui est aléatoire) de la balle en conséquence

        public void UpdateColor(Item item)
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
                rectangleSource.X = (int)spriteIndex * width; //on cherche la position du nouveau sprite de la balle dans le fichier "ball.png"
                item.ReinitialisationRectangleDestination(); //on fait disparaitre physiquement la hitbox de l'item
                hasCollidedItem = true; 

            }
        }

        //gère le mouvement de la balle lorsque l'on appuie sur la touche espace

        public void Jump()
        {
            positionObject += velocity; 
            rectangleDestination.Y = (int)positionObject.Y; //mise à jour de la hitbox de la balle après avoir changé de position
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !Hold) //la balle monte lorsqu'on appuie sur la touche 'Espace'
            { 
                positionObject.Y -= 8f;
                velocity.Y = -5f;
                rectangleDestination.Y = (int)positionObject.Y;
                hasJumped = true; 
                Hold = true; //empêche la balle de monter lorsque l'on reste appuyé sur la touche Espace 
            }

            //pour permettre de regérer la montée de la balle une fois la touche 'Espace' relâchée
            if (Keyboard.GetState().IsKeyUp(Keys.Space)) 
            {
                Hold = false;
            }
            if (hasJumped)
                velocity.Y += 0.3f; //pour gérer la gravité de la balle
        }

        //pour gérer l'affichage de la balle en fonction de la partie de l'image affichée

        public void DrawAnimation(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureObject, rectangleDestination, rectangleSource, Color.White);
        }
    }
}
