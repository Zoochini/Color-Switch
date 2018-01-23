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

        public ColorSwitch()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;


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
            ball.positionObject = new Vector2(512, 700);
            ball.InitialisationRectangleDestination();
            item.textureObject = Content.Load<Texture2D>("multicolor");
            item.positionObject = new Vector2(512, 300);
            item.InitialisationRectangleDestination();
            obstacle.textureObject = Content.Load<Texture2D>("obstacle");
            obstacle.positionObject = new Vector2(520, item.positionObject.Y-200);
            obstacle.InitialisationRectangleDestination();
            camera = new Camera();
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
            ball.Jump(gameTime);
            obstacle.Rotate();
            camera.Follow(ball);
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
            if (ball.positionObject.Y <= ColorSwitch.screenHeight / 2)
                spriteBatch.Begin(transformMatrix: camera.Translation);
            else
                spriteBatch.Begin();
            ball.DrawAnimation(spriteBatch);
            obstacle.Draw(spriteBatch);
            if(!ball.HasCollided)
                item.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
