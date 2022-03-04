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

        Texture2D _chip, _horse, _rect;

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
            _chip = this.Content.Load<Texture2D>("Chip");
            _horse = this.Content.Load<Texture2D>("Horse");
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
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    switch(_gameTable[i, j])
                    {
                        case 1:
                            _spriteBatch.Draw(_chip, new Vector2(_TILESIZE * j, _TILESIZE * i), null, Color.Yellow, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            break;
                        case -1:
                            _spriteBatch.Draw(_chip, new Vector2(_TILESIZE * j, _TILESIZE * i), null, Color.Red, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            break;
                        case 2:
                            _spriteBatch.Draw(_horse, new Vector2(_TILESIZE * j, _TILESIZE * i), null, Color.Yellow, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            break;
                        case -2:
                            _spriteBatch.Draw(_horse, new Vector2(_TILESIZE * j, _TILESIZE * i), null, Color.Red, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            break;
                        default:
                            break;
                    }
                }
            }


            _spriteBatch.End();
            
            _graphics.BeginDraw();

            base.Draw(gameTime);
        }
    }
}
