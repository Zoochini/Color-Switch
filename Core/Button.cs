using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_Switch.Core
{
    //afin de pouvoir créer des boutons pour le menu par exemples
    class Button
    {
        Rectangle position;
        bool clique;
        bool disponible;
        Texture2D image;

        public Button()
        {
            position = new Rectangle(100, 100, 100, 50);
            clique = false;
            disponible = true;

        }
        public Button(Rectangle rec, bool disp)
        {
            position = rec;
            disponible = disp;
            clique = false;
        }

        //getters et setters
        public bool Clique
        {

            get { return clique; }

            set { clique = value; }

        }
        public bool Diponible
        {
            get { return disponible; }
            set { disponible = value; }
        }
        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }

        //telecharger 
        public void load(ContentManager content, string name)
        {
            image = content.Load<Texture2D>(name);
        }
        public bool update(Vector2 mouse)
        {
            if (mouse.X >= position.X && mouse.X <= position.X + position.Width && mouse.Y >= position.Y && mouse.Y <= position.Y + position.Height)
            {
                clique = true;
            }

            else
            {
                clique = false;
            }

            if (!disponible)
            {
                clique = false;
            }

            return clique;

        }

        //Draw
        public void draw(SpriteBatch sp)
        {

            Color couleur = Color.White;

            if (!disponible)
            {
                couleur = new Color(50, 50, 50);
            }

            if (clique)
            {
                couleur = Color.Green;
            }

            sp.Draw(image, position, couleur);
        }


    }
}
