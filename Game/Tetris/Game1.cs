using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    /// <summary>
    ///     This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        private const float Timer = 0.3f;
        private readonly BlockFactory _blockFactory;
        private Texture2D _background;
        private Block _fallingBlock;
        private KeyboardState _oldState;
        private SpriteFont _scoreFont;
        private SpriteBatch _spriteBatch;
        private float _timer;

        public Game1()
        {
            var graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                PreferredBackBufferHeight = 640,
                PreferredBackBufferWidth = 640
            };
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";

            _timer = Timer;
            _blockFactory = new BlockFactory(Content);
        }

        /// <summary>
        ///     LoadContent will be called once per game and is the place to load
        ///     all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _background = Content.Load<Texture2D>("background");
            _scoreFont = Content.Load<SpriteFont>("ScoreFont");
        }

        /// <summary>
        ///     UnloadContent will be called once per game and is the place to unload
        ///     all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        ///     Allows the game to run logic such as updating the world,
        ///     checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState currentKeyState = Keyboard.GetState();

            // Allows the game to exit
            if (currentKeyState.IsKeyDown(Keys.Escape))
                Exit();
            else if (currentKeyState.IsKeyDown(Keys.Left) && !_oldState.IsKeyDown(Keys.Left))
            {
                _fallingBlock.MoveLeft();
            }
            else if (currentKeyState.IsKeyDown(Keys.Right) && !_oldState.IsKeyDown(Keys.Right))
            {
                _fallingBlock.MoveRight();
            }
            else if (currentKeyState.IsKeyDown(Keys.Up) && !_oldState.IsKeyDown(Keys.Up))
            {
                _fallingBlock.Rotate();
            }
            else if (currentKeyState.IsKeyDown(Keys.Space) && !_oldState.IsKeyDown(Keys.Space))
            {
                if (GameBoard.GameOver)
                {
                    GameBoard.Instance.NewGame();
                }
                else
                {
                    while (_fallingBlock.MoveDown())
                    {
                        
                    }
                }
            } else if (currentKeyState.IsKeyDown(Keys.Down) && !_oldState.IsKeyDown(Keys.Down))
            {
                _fallingBlock.MoveDown();
                _fallingBlock.MoveDown();
                _fallingBlock.MoveDown();
            }
            
            _oldState = currentKeyState;

            // logic to make fallingBlock fall
            var elapsed = (float) gameTime.ElapsedGameTime.TotalSeconds;
            _timer -= elapsed;
            if (_timer < 0)
            {
                if (!_fallingBlock.MoveDown())
                {
                    _fallingBlock = _blockFactory.GenerateBlock();
                    
                }
                _timer = Timer;
            }

            base.Update(gameTime);
        }

        /// <summary>
        ///     This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (GameBoard.GameOver)
            {
                _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                _spriteBatch.DrawString(_scoreFont, "Game over :-(", new Vector2(300.0f, 20.0f), Color.LightGreen,
                    0,
                    _scoreFont.MeasureString("Game over :-(")/2, 1.0f, SpriteEffects.None, 0.5f);
                const string output1 = "Score:";
                var output2 = GameBoard.Instance.Score + "";
                _spriteBatch.DrawString(_scoreFont, output1, new Vector2(300.0f, 40.0f), Color.LightGreen, 0,
                    _scoreFont.MeasureString(output1) / 2, 1.0f, SpriteEffects.None, 0.5f);
                _spriteBatch.DrawString(_scoreFont, output2, new Vector2(300.0f, 60.0f), Color.LightGreen, 0,
                    _scoreFont.MeasureString(output2) / 2, 1.0f, SpriteEffects.None, 0.5f);
                _spriteBatch.End();
            }
            else
            {
                GraphicsDevice.Clear(Color.White);

                _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
                const string output1 = "Score:";
                var output2 = GameBoard.Instance.Score + "";
                _spriteBatch.DrawString(_scoreFont, output1, new Vector2(380.0f, 20.0f), Color.LightGreen, 0,
                    _scoreFont.MeasureString(output1) / 2, 1.0f, SpriteEffects.None, 0.5f);
                _spriteBatch.DrawString(_scoreFont, output2, new Vector2(380.0f, 40.0f), Color.LightGreen, 0,
                    _scoreFont.MeasureString(output2) / 2, 1.0f, SpriteEffects.None, 0.5f);

                if (_fallingBlock == null)
                    _fallingBlock = _blockFactory.GenerateBlock();
                foreach (var square in _fallingBlock.Squares)
                {
                    _spriteBatch.Draw(square.Texture, square.Position, Color.White);
                }
                foreach (var square in GameBoard.Instance.Board)
                {
                    if (square != null)
                        _spriteBatch.Draw(square.Texture, square.Position, Color.White);
                }
                _spriteBatch.End();
            }
            
            base.Draw(gameTime);
        }
    }
}