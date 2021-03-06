using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TicTacToe
{
    public class TicTacToe : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D _line, _circle, _cross;

        int[,] _gameTable;

        bool _isCircleTurn = true;
        bool _isOWinning = false;
        bool _isXWinning = false;

        public TicTacToe()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 600;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 600;   // set this value to the desired height of your window
            _graphics.ApplyChanges();

            _gameTable = new int[3, 3];

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load a Texture2D from a file 
            _line = this.Content.Load<Texture2D>("Line");
            _circle = this.Content.Load<Texture2D>("Circle");
            _cross = this.Content.Load<Texture2D>("Cross");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState state = Mouse.GetState();

            if (state.LeftButton == ButtonState.Pressed)
            {
                //TODO: do clicking
                int xPos = state.X / 200;
                int yPos = state.Y / 200;

                if (xPos >= 0 && xPos < 3 && yPos >= 0 && yPos < 3)
                {
                    if(_gameTable[yPos, xPos] == 0 && _isOWinning == false && _isXWinning == false)
                    {
                        if(_isCircleTurn)
                        {
                            _gameTable[yPos, xPos] = 1;
                        }
                        else
                        {
                            _gameTable[yPos, xPos] = -1;
                        }
                        _isCircleTurn = !_isCircleTurn;
                    }
                }
            }

            //TODO: check winning condition
            // Winning when
            // (0,0) - (0,1) - (0,2)
            // (1,0) - (1,1) - (1,2)
            // (2,0) - (2,1) - (2,2)
            // (0,0) - (1,0) - (2,0)
            // (0,1) - (1,1) - (2,1)
            // (0,2) - (1,2) - (2,2)
            // (0,0) - (1,1) - (2,2)
            // (0,2) - (1,1) - (2,0)

            // O Winning Condition. 
            if(_gameTable[0,0] == 1 && _gameTable[0,1] == 1 && _gameTable[0,2] == 1)
            {
                _isOWinning = true;
            }
            else if(_gameTable[1,0] == 1 && _gameTable[1,1] == 1 && _gameTable[1,2] == 1)
            {
                _isOWinning = true;
            }
            else if (_gameTable[2, 0] == 1 && _gameTable[2, 1] == 1 && _gameTable[2, 2] == 1)
            {
                _isOWinning = true;
            }
            else if (_gameTable[0, 0] == 1 && _gameTable[1, 0] == 1 && _gameTable[2, 0] == 1)
            {
                _isOWinning = true;
            }
            else if (_gameTable[0, 1] == 1 && _gameTable[1, 1] == 1 && _gameTable[2, 1] == 1)
            {
                _isOWinning = true;
            }
            else if (_gameTable[0, 2] == 1 && _gameTable[1, 2] == 1 && _gameTable[2, 2] == 1)
            {
                _isOWinning = true;
            }
            else if (_gameTable[0, 0] == 1 && _gameTable[1, 1] == 1 && _gameTable[2, 2] == 1)
            {
                _isOWinning = true;
            }
            else if (_gameTable[0, 2] == 1 && _gameTable[1, 1] == 1 && _gameTable[2, 0] == 1)
            {
                _isOWinning = true;
            }


            // X Winning Condition. 
            if (_gameTable[0, 0] == -1 && _gameTable[0, 1] == -1 && _gameTable[0, 2] == -1)
            {
                _isXWinning = true;
            }
            else if (_gameTable[1, 0] == -1 && _gameTable[1, 1] == -1 && _gameTable[1, 2] == -1)
            {
                _isXWinning = true;
            }
            else if (_gameTable[2, 0] == -1 && _gameTable[2, 1] == -1 && _gameTable[2, 2] == -1)
            {
                _isXWinning = true;
            }
            else if (_gameTable[0, 0] == -1 && _gameTable[1, 0] == -1 && _gameTable[2, 0] == -1)
            {
                _isXWinning = true;
            }
            else if (_gameTable[0, 1] == -1 && _gameTable[1, 1] == -1 && _gameTable[2, 1] == -1)
            {
                _isXWinning = true;
            }
            else if (_gameTable[0, 2] == -1 && _gameTable[1, 2] == -1 && _gameTable[2, 2] == -1)
            {
                _isXWinning = true;
            }
            else if (_gameTable[0, 0] == -1 && _gameTable[1, 1] == -1 && _gameTable[2, 2] == -1)
            {
                _isXWinning = true;
            }
            else if (_gameTable[0, 2] == -1 && _gameTable[1, 1] == -1 && _gameTable[2, 0] == -1)
            {
                _isXWinning = true;
            }

            if(_isOWinning)
            {
                Console.WriteLine("O is the winner.");
            }
            else if(_isXWinning)
            {
                Console.WriteLine("X is the winner.");
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            //TODO: Draw circle and Cross
            // 0 -> Not draw.
            // 1 -> Circle is drawn.
            // -1 -> Cross is drawn.
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if(_gameTable[i, j] == 0)
                    {
                        // Nothing
                    }
                    else if(_gameTable[i, j] == 1)
                    {
                        // Circle
                        _spriteBatch.Draw(_circle, new Vector2(j * 200, i * 200), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    }
                    else if(_gameTable[i, j] == -1)
                    {
                        // Cross 
                        _spriteBatch.Draw(_cross, new Vector2(j * 200, i * 200), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    }
                }
            }


            //Draw Lines for game board
            _spriteBatch.Draw(_line, new Vector2(0, 200), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(_line, new Vector2(0, 400), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);


            _spriteBatch.Draw(_line, new Vector2(200, 0), null, Color.White, MathHelper.Pi / 2, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(_line, new Vector2(400, 0), null, Color.White, MathHelper.Pi / 2, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            //TODO: Draw Finish line


            _spriteBatch.End();

            _graphics.BeginDraw();

            base.Draw(gameTime);
        }
    }
}
