using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace The_Snake_Lab
{
    public class TheSnake : Game
    {
        public const int WIDTH = 800;
        public const int HEIGHT = 500;  

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D _pellet;
        Texture2D _rect;

        SpriteFont _font;

        public TheSnake()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = WIDTH;
            _graphics.PreferredBackBufferHeight = HEIGHT + 100;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _pellet = this.Content.Load<Texture2D>("Sprite/Pellet");
            _font = this.Content.Load<SpriteFont>("Font/GameFont");

            _rect = new Texture2D(_graphics.GraphicsDevice, 1, 1); 
            Color[] data = new Color[1];
            data[0] = Color.White;
            _rect.SetData(data);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
