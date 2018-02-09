using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color_Switch.Core;
using System;
using System.Collections.Generic;

namespace Color_Switch
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ColorSwitch : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Ball ball;
        Item item;
        Score score;
        Camera camera;
        Color color;
        Obstacle obstacleActuel;
        Obstacle nouvelObstacle;
        Obstacle temp;
        SpriteFont font;

        int random = 0;
        int randomPrecedent = 0;
        float hauteurMax=768/2;

        public static int screenWidth;
        public static int screenHeight;
        /*pour le menu*/
        const int menu = 0, play = 1, gameover = 2, scoree = 3;
        int CurrentScreen = menu;
        Texture2D scoresgame, playgame;
        Button playButton, scoreButton;
        float screenwidth, screenheight;
        Texture2D bgimage;
        MouseState mouseState, previousMouseState;
        KeyboardState ks;
        /*FIn pour le menu*/

        public ColorSwitch()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;
            hauteurMax = screenHeight / 2;
            color = new Color(10, 10, 10);

            this.IsMouseVisible = true;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ball = new Ball(26, 26);
            item = new Item(30, 30);
            nouvelObstacle = new Obstacle(2048, 11);
            obstacleActuel = new Obstacle(0, 0); //ne sert à rien en l'état actuel, il est juste créé pour pouvoir être dans la méthode Draw. Il servira lorsqu'un nouvel obstacle sera créé
            score = new Score(33, 32);
            camera = new Camera();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ball.TextureObject = Content.Load<Texture2D>("ball");
            ball.PositionObject = new Vector2((screenWidth - ball.Width) / 2, (((screenHeight - ball.Height) / 2)) + (screenHeight / 4));
            ball.InitialisationRectangleDestination();

            item.TextureObject = Content.Load<Texture2D>("multicolor");
            item.PositionObject = new Vector2((screenWidth - ball.Width) / 2, ((screenHeight - item.Height) / 2) - (screenHeight / 3));
            item.InitialisationRectangleDestination();

            nouvelObstacle.TextureObject = Content.Load<Texture2D>("obstacleLine");
            nouvelObstacle.PositionObject = new Vector2(-1024, item.PositionObject.Y - (screenHeight / 4));
            nouvelObstacle.InitialisationRectangleDestination();

            obstacleActuel.TextureObject = Content.Load<Texture2D>("obstacleLine");
            obstacleActuel.PositionObject = new Vector2(0, 0);
            obstacleActuel.InitialisationRectangleDestination();

            score.TextureObject = Content.Load<Texture2D>("star");
            score.PositionObject = new Vector2((screenWidth - ball.Width) / 2, nouvelObstacle.PositionObject.Y - 100);
            score.InitialisationRectangleDestination();

            font = Content.Load<SpriteFont>("Score");

            /*pour le menu*/
            scoresgame = Content.Load<Texture2D>("scores");

            playgame = Content.Load<Texture2D>("play");
            bgimage = Content.Load<Texture2D>("bgColor");

            scoreButton = new Button(new Rectangle(300, 300, scoresgame.Width, scoresgame.Height), true);
            scoreButton.load(Content, "scores");


            playButton = new Button(new Rectangle(300, 100, playgame.Width, playgame.Height), true);
            playButton.load(Content, "play");

            /*fin pour le menu*/
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            ball.UpdateColor(item);
            score.CollisionBall(ball);
            ball.Jump();

            //Lorsque l'obstacle atteint le bas de l'écran, on charge un nouvel obstacle tout en conservant l'actuel

            if (nouvelObstacle.PositionObject.Y >= ball.PositionObject.Y + 200)
            {
                temp = nouvelObstacle;
                nouvelObstacle = UpdateObstacles(); //pour générer le nouvel obstacle. La méthode se situe juste après la méthode Update
                obstacleActuel = temp;
            }

            //pour gérer le déplacement du nouvel obstacle

            if (random == 1 || random == 2)
                nouvelObstacle.Rotate();
            else
                nouvelObstacle.Move(gameTime);

            //pour gérer le déplacement de l'ancien obstacle

            if (randomPrecedent == 1 || randomPrecedent == 2)
                obstacleActuel.Rotate();
            else
                obstacleActuel.Move(gameTime);


            //tant que la balle n'a pas atteint la hauteur maximale qu'elle a atteint, la camera ne la suivra pas

            if (ball.PositionObject.Y <= hauteurMax)
            {
                camera.Follow(ball);
                hauteurMax = ball.PositionObject.Y;
            }
            /*Pour le menu*/
            //verifier l'état de la souris
            mouseState = Mouse.GetState();
            ks = Keyboard.GetState();
            UpdateMenu();

            /*fin pour le menu*/
            base.Update(gameTime);
        }

        //pour la gestion du menu

        void UpdateMenu()
        {
            switch (CurrentScreen)
            {
                case menu:
                    //pour aller dans le jeux
                    if (playButton.update(new Vector2(mouseState.X, mouseState.Y)) == true && mouseState != previousMouseState && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        CurrentScreen = play;
                    }

                    //pour aller dans les scores
                    if (scoreButton.update(new Vector2(mouseState.X, mouseState.Y)) == true && mouseState != previousMouseState && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        CurrentScreen = scoree;
                    }

                    break;

                case scoree:
                    if (ks.IsKeyDown(Keys.A))
                    {
                        CurrentScreen = menu;
                    }
                    break;

                case play:
                    //What we want to happen when we play our GAME goes in here.
                    if (ks.IsKeyDown(Keys.A))
                    {
                        CurrentScreen = menu;
                    }
                    break;

                case gameover:
                    //What we want to happen when our GAME is OVER goes in here.
                    break;

            }
            previousMouseState = mouseState;
        }


        //pour gérer la création d'un nouvel obstacle

        public Obstacle UpdateObstacles()
        {
            Random rnd = new Random();
            Obstacle obstacle;

            //l'index prend la valeur de l'ancien obstacle puis on met à jour indexNouvelObstacle pour la création du nouvel obstacle

            randomPrecedent = random;
            random = rnd.Next(0, 3);

            //création de l'obstacle en forme de ligne

            if (random == 0)
            {
                obstacle = new Obstacle(2048, 11);
                obstacle.TextureObject = Content.Load<Texture2D>("obstacleLine");
                obstacle.PositionObject = new Vector2(-1024, ball.PositionObject.Y - 500);
                item.PositionObject = new Vector2((screenWidth - ball.Width) / 2, obstacle.PositionObject.Y + 100);
                score.PositionObject = new Vector2((screenWidth - ball.Width) / 2, obstacle.PositionObject.Y - 100);
            }
            else
            {
                //création de l'obstacle en forme de carré

                if (random == 1)
                {
                    obstacle = new Obstacle(300, 299);
                    obstacle.TextureObject = Content.Load<Texture2D>("obstacleSquare");
                    obstacle.PositionObject = new Vector2(screenWidth / 2, ball.PositionObject.Y - 600);
                    item.PositionObject = new Vector2((screenWidth - ball.Width) / 2, obstacle.PositionObject.Y + 200);
                    score.PositionObject = new Vector2(obstacle.PositionObject.X - (ball.Width) / 2, obstacle.PositionObject.Y - (ball.Height) / 2);
                }
                else
                {
                    //création de l'obstacle en forme de cercle

                    obstacle = new Obstacle(300, 300);
                    obstacle.TextureObject = Content.Load<Texture2D>("obstacleCircle");
                    obstacle.PositionObject = new Vector2(screenWidth / 2, ball.PositionObject.Y - 600);
                    score.PositionObject = new Vector2(obstacle.PositionObject.X - (ball.Width) / 2, obstacle.PositionObject.Y - (ball.Height) / 2);
                    item.PositionObject = new Vector2((screenWidth - ball.Width) / 2, obstacle.PositionObject.Y + 200);
                }

            }

            obstacle.InitialisationRectangleDestination();
            item.InitialisationRectangleDestination();
            score.InitialisationRectangleDestination();

            //pour que les objets ball et score réaparaissent

            ball.HasCollidedItem = false;
            score.HasCollidedScore = false;
            return obstacle;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(color);

            // TODO: Add your drawing code here
            //pour le menu
            if(hauteurMax>ball.PositionObject.Y)

                spriteBatch.Begin();
            else
                spriteBatch.Begin(transformMatrix: camera.Translation);
            switch (CurrentScreen)
            {
                case menu:
                    spriteBatch.Draw(bgimage, new Rectangle(0, 0, bgimage.Width, bgimage.Height), Color.White);
                    spriteBatch.Draw(playgame, new Rectangle(300, 100, playgame.Width, playgame.Height), Color.White);
                    spriteBatch.Draw(scoresgame, new Rectangle(300, 200, scoresgame.Width, scoresgame.Height), Color.White);
                    break;
                case scoree:
                    spriteBatch.Draw(scoresgame, new Rectangle(300, 300, scoresgame.Width, scoresgame.Height), Color.White);
                    break;
                case play:
                    DrawGamePlay(gameTime);
                    break;
            }
            //Gère le cas où la balle n'a pas encore atteint le centre de l'écran


           spriteBatch.End();
            base.Draw(gameTime);
        }

        void DrawGamePlay(GameTime gameTime)
        {
            /* ajout de cette fonction  le 08/02/2018 a 15h05
             les elements de cette fonction provienne de la fonction DRAW */
            //Gère le cas où la balle n'a pas encore atteint le centre de l'écran

            if (ball.PositionObject.Y > screenHeight / 2 && !camera.SupHalfScreenHeight)
            {

             
                spriteBatch.DrawString(font, "" + score.ScoreCompteur, new Vector2(0, 0), Color.White);
            }

            else
            {
               

                //Pour gérer la position du score (car elle est dynamique) en fonction de la position de la balle

                spriteBatch.DrawString(font, "" + score.ScoreCompteur, new Vector2(0, ball.PositionObject.Y - ((screenHeight - ball.Height) / 2) - (ball.PositionObject.Y - hauteurMax)), Color.White);
            }

            ball.DrawAnimation(spriteBatch);

            //Deux affichages différents pour les obstacles : un qui gère la rotation, l'autre qui gère le déplacement horizontal

            if (random == 0)
                nouvelObstacle.Draw(spriteBatch);
            else
                nouvelObstacle.DrawRotate(spriteBatch);
            if (randomPrecedent == 0)
                obstacleActuel.Draw(spriteBatch);
            else
                obstacleActuel.DrawRotate(spriteBatch);

            //lorsque la balle touche l'item, ce dernier disparait de l'écran

            if (!ball.HasCollidedItem)
                item.Draw(spriteBatch);

            //pareil lorsque la balle touche l'objet score

            if (!score.HasCollidedScore)
                score.Draw(spriteBatch);
        }
    }
}
