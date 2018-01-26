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
        protected Vector2 positionObject; //Position X et Y des objets
        protected Texture2D textureObject; //Charger les sprites des objets
        protected Rectangle rectangleSource; //Avoir les coordonnées un sprite dans un fichier en contenant plusieurs
        protected Rectangle rectangleDestination; //Hitbox des objets
        protected Vector2 velocity; //Vitesse de déplacement des objets
        protected int width; //la largeur de l'objet
        protected int height; //la hauteur de l'objet

        public int Width { get => width; }
        public int Height { get => height; }

        public Rectangle RectangleDestination { get => rectangleDestination; }
        public Vector2 PositionObject { get => positionObject; set => positionObject = value; }
        public Texture2D TextureObject { get => textureObject; set => textureObject = value; }
        public Vector2 Velocity { get => velocity; }




        public CSObject()
        {
        }

        public CSObject(int Width, int Height)
        {
            this.width = Width;
            this.height = Height;

        }

        //pour initialiser la hitbox des objets

        public void InitialisationRectangleDestination()
        {
            rectangleDestination = new Rectangle((int)PositionObject.X, (int)PositionObject.Y, width, height);
        }

        //pour mettre à jour la hitbox des objets

        public void UpdateRectangleDestination(Vector2 positionObject)
        {
            rectangleDestination.Y = (int)positionObject.Y;
        }

        //pour "faire disparaite" la hitbox d'un objet

        public void ReinitialisationRectangleDestination()
        {
            rectangleDestination.X = 0;
            rectangleDestination.Y = 0;
            rectangleDestination.Width = 0;
            rectangleDestination.Height = 0;
        }

        //Affichage standard des objets

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureObject, rectangleDestination, Color.White);
        }

    }
}
