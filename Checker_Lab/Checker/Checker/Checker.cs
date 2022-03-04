using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Checker
{
    public class Checker : Game
    {
        const int _TILESIZE = 75;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        enum PlayerTurn
        {
            RedTurn,
            YellowTurn
        }

        PlayerTurn _currentPlayerTurn;

        //TODO: Game State Machine

        MouseState _mouseState, _previousMouseState;
        Point _clickedPos;

        //TODO: Image files and font

        int[,] _gameTable;

        public Checker()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 600;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 600;   // set this value to the desired height of your window
            _graphics.ApplyChanges();

            _currentPlayerTurn = PlayerTurn.RedTurn;

            _gameTable = new int[8, 8]
            {
                { 1,0,1,0,1,0,1,0},
                { 0,1,0,1,0,1,0,1},
                { 0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0},
                { -1,0,-1,0,-1,0,-1,0},
                { 0,-1,0,-1,0,-1,0,-1}
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _previousMouseState = _mouseState;

            //TODO: Game logic in state machine

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            //TODO: Draw board

            //TODO: draw chips


            _spriteBatch.End();

            _graphics.BeginDraw();

            base.Draw(gameTime);
        }
    }
}
