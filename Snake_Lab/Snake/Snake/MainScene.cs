using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Snake
{
    public class MainScene : Game
    {
        public const int WIDTH = 800;
        public const int HEIGHT = 500;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D _rect;
        SpriteFont _font;

        float _tick;

        enum GameState
        {
            GamePlaying,
            GamePaused,
            GameEnded
        };

        GameState _currentGameState;

        Texture2D _pellet;

        Vector2 _foodPos;
        List<Vector2> _snakePelletsPos;

        float _moveSpeed;

        Vector2 _currentDirection;

        int _score;
        long _timer;

        KeyboardState _previousKey, _currentKey;

        public MainScene()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = WIDTH;
            _graphics.PreferredBackBufferHeight = HEIGHT + 100;
            _graphics.ApplyChanges();

            _currentGameState = GameState.GamePlaying;

            _snakePelletsPos = new List<Vector2>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _pellet = this.Content.Load<Texture2D>("Pellet");

            _rect = new Texture2D(_graphics.GraphicsDevice, 1, 1);

            Color[] data = new Color[1];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            _rect.SetData(data);

            _font = Content.Load<SpriteFont>("GameFont");

            Reset();
        }

        protected override void Update(GameTime gameTime)
        {
            _currentKey = Keyboard.GetState();

            switch (_currentGameState)
            {
                case GameState.GamePlaying:

                    // handle the pause
                    if (_currentKey.IsKeyDown(Keys.Escape) && !_currentKey.Equals(_previousKey))
                        _currentGameState = GameState.GamePaused;

                    // handle the input
                    if (_currentKey.IsKeyDown(Keys.Left))
                    {
                        if (_currentDirection.X == 0)
                        {
                            _currentDirection = new Vector2(-Math.Abs(_currentDirection.Y), 0);
                        }
                    }
                    if (_currentKey.IsKeyDown(Keys.Right))
                    {
                        if (_currentDirection.X == 0)
                        {
                            _currentDirection = new Vector2(Math.Abs(_currentDirection.Y), 0);
                        }
                    }
                    if (_currentKey.IsKeyDown(Keys.Up))
                    {
                        if (_currentDirection.Y == 0)
                        {
                            _currentDirection = new Vector2(0, -Math.Abs(_currentDirection.X));
                        }
                    }
                    if (_currentKey.IsKeyDown(Keys.Down))
                    {
                        if (_currentDirection.Y == 0)
                        {
                            _currentDirection = new Vector2(0, Math.Abs(_currentDirection.X));
                        }
                    }

                    //move body
                    _tick += gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;

                    if (_tick >= 1 / _moveSpeed)
                    {
                        //move snake by add head
                        AddNewHead();

                        _tick = 0;

                        //check head
                        //eat food
                        if (_snakePelletsPos[0].Equals(_foodPos))
                        {
                            AddNewHead();
                            _foodPos = GetNewFoodPos();
                            _moveSpeed *= 1.1f;

                            _score++;
                        }
                        //check crash
                        else if (CheckSnakeCrashed())
                        {
                            _currentGameState = GameState.GameEnded;
                        }
                        else
                        {
                            // if the food is not eaten, remove tail
                            RemoveTail();
                        }
                    }

                    _timer += gameTime.ElapsedGameTime.Ticks;

                    break;
                case GameState.GamePaused:
                    // unpause
                    if (_currentKey.IsKeyDown(Keys.Escape) && !_currentKey.Equals(_previousKey))
                        _currentGameState = GameState.GamePlaying;
                    break;
                case GameState.GameEnded:
                    // handle the input
                    if (_currentKey.IsKeyDown(Keys.Space) && !_currentKey.Equals(_previousKey))
                    {
                        _currentGameState = GameState.GamePlaying;
                        Reset();
                    }
                    break;
            }

            _previousKey = _currentKey;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DimGray);

            _spriteBatch.Begin();

            //BG drawing
            _spriteBatch.Draw(_rect, Vector2.Zero, null, Color.Black, 0f, Vector2.Zero, new Vector2(WIDTH, HEIGHT), SpriteEffects.None, 0f);

            //Snake drawing
            foreach (Vector2 sp in _snakePelletsPos)
            {
                if (sp.X >= 0 && sp.X < MainScene.WIDTH &&
                    sp.Y >= 0 && sp.Y < MainScene.HEIGHT)
                {
                    _spriteBatch.Draw(_pellet, sp, Color.White);
                }
            }

            _spriteBatch.Draw(_pellet, _foodPos, Color.Lime);

            _spriteBatch.DrawString(_font, "SCORE : " + _score, new Vector2(10, HEIGHT + 40), Color.White);
            _spriteBatch.DrawString(_font, "TIME : " + String.Format("{0}:{1:00}", _timer / 600000000, (_timer / 10000000) % 60), new Vector2(WIDTH * 3 / 4, HEIGHT + 40), Color.White);

            if (_currentGameState == GameState.GamePaused)
            {
                Vector2 fontSize = _font.MeasureString("Pause");
                _spriteBatch.DrawString(_font, "Pause", new Vector2((WIDTH - fontSize.X) / 2, (HEIGHT - fontSize.Y) / 2), Color.YellowGreen);
            }
            else if (_currentGameState == GameState.GameEnded)
            {
                Vector2 fontSize = _font.MeasureString("Game Over");
                _spriteBatch.DrawString(_font, "Game Over", new Vector2((WIDTH - fontSize.X) / 2, (HEIGHT - fontSize.Y) / 2), Color.YellowGreen);
                fontSize = _font.MeasureString("Press SPACE to restart");
                _spriteBatch.DrawString(_font, "Press SPACE to restart", new Vector2((WIDTH - fontSize.X) / 2, (HEIGHT - fontSize.Y) / 2 + 20), Color.YellowGreen);
            }

            _spriteBatch.End();

            _graphics.BeginDraw();

            base.Draw(gameTime);
        }

        protected void Reset()
        {
            _foodPos = GetNewFoodPos();

            _snakePelletsPos.Clear();

            for (int i = 1; i <= 5; i++)
            {
                _snakePelletsPos.Add(new Vector2(MainScene.WIDTH / 2 - 10 * i, MainScene.HEIGHT / 2));
            }

            _currentDirection = new Vector2(1f, 0);
            _moveSpeed = 5f;

            _tick = 0;

            _score = 0;
            _timer = 0;
        }

        protected void AddNewHead()
        {
            _snakePelletsPos.Insert(0, GetNewHead());
        }

        protected void RemoveTail()
        {
            _snakePelletsPos.RemoveAt(_snakePelletsPos.Count - 1);
        }

        protected Vector2 GetNewFoodPos()
        {
            Random rnd = new Random();

            Vector2 returningPoint = new Vector2();

            do
            {
                returningPoint.X = (int)(rnd.Next(WIDTH / 10)) * 10;
                returningPoint.Y = (int)(rnd.Next(HEIGHT / 10)) * 10;

            } while (CheckSnakePelletsPos(returningPoint));

            return returningPoint;
        }

        protected bool CheckSnakePelletsPos(Vector2 checkingPos)
        {
            foreach (Vector2 sp in _snakePelletsPos)
            {
                if (sp.Equals(checkingPos)) return true;
            }

            return false;
        }

        protected bool CheckSnakeCrashed()
        {
            if (_snakePelletsPos[0].X < 0 || _snakePelletsPos[0].X >= MainScene.WIDTH ||
                _snakePelletsPos[0].Y < 0 || _snakePelletsPos[0].Y >= MainScene.HEIGHT)
            {
                return true;
            }

            for (int i = 1; i < _snakePelletsPos.Count; i++)
            {
                if (_snakePelletsPos[0].Equals(_snakePelletsPos[i])) return true;
            }

            return false;
        }

        protected Vector2 GetNewHead()
        {
            Vector2 newHeadPos = new Vector2(_snakePelletsPos[0].X + _currentDirection.X * 10f, _snakePelletsPos[0].Y + _currentDirection.Y * 10f);

            return newHeadPos;
        }
    }
}