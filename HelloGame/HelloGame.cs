using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HelloGame
{
    public class HelloGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 ballPosition;
        private Vector2 ballVelocity;
        private Texture2D ballTexture;
        /// <summary>
        /// initializes window firstly
        /// </summary>
        public HelloGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.Title = "Hello wonderful first game!";
        }
        /// <summary>
        /// initialization for actual game logic
        /// </summary>
        protected override void Initialize()
        {
            //2d vector, floats (object or struct?)
            //vecot is magnitude and direction
            ballPosition = new Vector2(
                GraphicsDevice.Viewport.Width / 2,
                GraphicsDevice.Viewport.Height / 2
                );
            //random direction for ball.  Good enough for now
            //might want to look for gaussian noise creators for better randomness
            System.Random random = new System.Random();//throwing away precision of doubles
            ballVelocity = new Vector2(
                (float)random.NextDouble(),
                (float)random.NextDouble()
                );
            ballVelocity.Normalize();//rescales vector to be of length 1
            ballVelocity *= 100;//100 pixels per second
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //load up sprites here to load content after graphics card is spun up
            //need textures on RAM, but not system RAM(slower)
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ballTexture = Content.Load<Texture2D>("ball");//beware of file paths, make sure names are different
            // TODO: use this.Content to load your game content here
        }
        /// <summary>
        /// update game status (want to change ball's position)
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //fixed timestep for sole sytems with unique clock, synced with monitor refresh rate
            ballPosition += ballVelocity * (float) gameTime.ElapsedGameTime.TotalSeconds;//vector2 overrides +=, can use operators directly 
            //elapsed time is time between frames
            //Vector2.Add();
            // TODO: Add your update logic here
            //catches for off the screen
            //64 is width of ball, otherwise ball could still go off screen
            if (ballPosition.X < GraphicsDevice.Viewport.X
                || ballPosition.X > GraphicsDevice.Viewport.Width - 64) {
                ballVelocity.X *= -1;//flipping direction

            }
            if (ballPosition.Y < GraphicsDevice.Viewport.Y
                || ballPosition.Y > GraphicsDevice.Viewport.Height - 64)
            {
                ballVelocity.Y *= -1;//flipping direction

            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //batches up a bunch of renderings and does it all at once
            _spriteBatch.Begin();
            _spriteBatch.Draw(ballTexture, ballPosition, Color.DarkOrange);//prep drawing
            _spriteBatch.End();//drawn here
            base.Draw(gameTime);
        }
    }
}
