using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TilingEngine
{
    /// <summary>
    /// This is the main class.
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Keyboard and mouse state variables
        KeyboardState currentKeyboardState;
        MouseState currentMouseState;

        //The debug font
        SpriteFont font;

        //Tiling engine variable
        TilingEngine tiler;

        //Texture for the cursor
        Texture2D cursor;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load the cursor texture
            cursor = Content.Load<Texture2D>("pointer");

            font = Content.Load<SpriteFont>("Segoe UI Mono");

            //Create the tiling engine
            tiler = new TilingEngine(Content, 10, 27);

            //Initialize the user input states
            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
        }

        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Update the keyboard state
            KeyboardState newKeyboardState = Keyboard.GetState();

            //On escape key up, exit
            if (currentKeyboardState.IsKeyDown(Keys.Escape) && newKeyboardState.IsKeyUp(Keys.Escape))
                this.Exit();

            //Record the keyboard state
            currentKeyboardState = newKeyboardState;

            //Update the mouse state
            MouseState newMouseState = Mouse.GetState();

            //If the left button is pressed down and we're within the viewport, cycle the tile at that mouse location
            if (currentMouseState.LeftButton == ButtonState.Released && newMouseState.LeftButton == ButtonState.Pressed)
                if (GraphicsDevice.Viewport.Bounds.Contains(currentMouseState.X, currentMouseState.Y))
                    tiler.cycleTileAtLocation(currentMouseState.X, currentMouseState.Y);

            //Record the mouse state
            currentMouseState = newMouseState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Draw the tiles
            tiler.Draw(spriteBatch, font);

            //Draw the cursor
            spriteBatch.Begin();
            spriteBatch.Draw(cursor, new Vector2(currentMouseState.X, currentMouseState.Y),new Rectangle(0,0,18,18), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
