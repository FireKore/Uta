using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Uta
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Uta : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TextEngine.TextEngine textEngine;
		bool spacePressed = false;
        Song song;

        public Uta()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            textEngine = new TextEngine.TextEngine("..\\..\\..\\..\\Content\\ブラック★ロックシューター.ass");
            textEngine.Initialize(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            song = Content.Load<Song>("BRS");
            spriteBatch = new SpriteBatch(GraphicsDevice);
			textEngine.setFont(Content.Load<SpriteFont>("font"));
            MediaPlayer.Play(song);

            // TODO: use this.Content to load your game content here
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

			if(Keyboard.GetState().IsKeyDown(Keys.Space))
			{
				spacePressed = true;
			}
			if(spacePressed && Keyboard.GetState().IsKeyUp(Keys.Space))
            {
				spacePressed = false;
                switch (MediaPlayer.State)
                {
                    case MediaState.Stopped:
                        MediaPlayer.Play(song);
                        break;
                    case MediaState.Playing:
                        MediaPlayer.Pause();
                        break;
                    case MediaState.Paused:
                        MediaPlayer.Resume();
                        break;
                    default:
                        break;
                }
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //TODO: Add your drawing code here
			spriteBatch.Begin();
            textEngine.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
