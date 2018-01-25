using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color_Switch.Core;

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
        Obstacle obstacle;
        Score score;
        Camera camera;
        public static int screenWidth;
        public static int screenHeight;
        public float hauteurMax;
        private SpriteFont font;

        public ColorSwitch()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;
            hauteurMax = screenHeight/2;


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
            obstacle = new Obstacle(300, 300);
            score = new Score(33, 32);
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
            ball.textureObject = Content.Load<Texture2D>("ball");
            ball.positionObject = new Vector2((screenWidth - ball.frameWidth) / 2, (((screenHeight - ball.frameHeight) / 2))+(screenHeight/4));
            ball.InitialisationRectangleDestination();
            item.textureObject = Content.Load<Texture2D>("multicolor");
            item.positionObject = new Vector2((screenWidth - ball.frameWidth) / 2, ((screenHeight - item.frameHeight) / 2)-(screenHeight/3));
            item.InitialisationRectangleDestination();
            obstacle.textureObject = Content.Load<Texture2D>("obstacle");
            obstacle.positionObject = new Vector2(screenWidth / 2, item.positionObject.Y-(screenHeight/4));
            obstacle.InitialisationRectangleDestination();
            score.textureObject = Content.Load<Texture2D>("star");
            score.positionObject = new Vector2(obstacle.positionObject.X-(ball.frameWidth)/2, obstacle.positionObject.Y-(ball.frameHeight)/2);
            score.InitialisationRectangleDestination();
            camera = new Camera();
            font = Content.Load<SpriteFont>("Score");
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
            ball.UpdateColor(gameTime, item);
            score.CollisionBall(ball);
            ball.Jump(gameTime);
            obstacle.Rotate();
            if (ball.positionObject.Y <= hauteurMax)
            {
                camera.Follow(ball);
                hauteurMax = ball.positionObject.Y;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.TransparentBlack);

            // TODO: Add your drawing code here
            if (ball.positionObject.Y > ColorSwitch.screenHeight/2 && !camera.supHalfScreenHeight)
            {

                spriteBatch.Begin();
                spriteBatch.DrawString(font, "" + score.ScoreCompteur, new Vector2(0, 0), Color.White);
            }
            else
            {
                spriteBatch.Begin(transformMatrix: camera.Translation);
                spriteBatch.DrawString(font, "" + score.ScoreCompteur, new Vector2(0, ball.positionObject.Y - ((screenHeight - ball.frameHeight) / 2) - (ball.positionObject.Y - hauteurMax)), Color.White);
            }
                
            ball.DrawAnimation(spriteBatch);
            obstacle.Draw(spriteBatch);
            if(!ball.HasCollidedItem)
                item.Draw(spriteBatch);
            if (!score.HasCollidedScore)
                score.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
