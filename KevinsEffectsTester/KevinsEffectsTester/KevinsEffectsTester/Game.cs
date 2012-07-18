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
using KevinsEffects.FullScreenEffects;

namespace KevinsEffectsTester
{
    /// <summary>
    /// Main class for blur test.
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Represents the current (sometimes previous) state of the keyboard.
        KeyboardState keyboardState;
        MouseState mouseState;

        //The variable blur effect
        VariableBlurEffect variableBlur;

        //The background texture
        Texture2D background;

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

            //Load the background
            background = Content.Load<Texture2D>("mario_and_friends");

            //Initialize the variable blur effect
            variableBlur = new VariableBlurEffect(Content, GraphicsDevice, background.Bounds, 32, 50, 6);
            
            //Initialize the user input states
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        protected override void UnloadContent()
        {
            //Unload the variable blur effect
            variableBlur.UnloadContent();
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

            //Capture the current keyboard state
            KeyboardState newKeyboardState = Keyboard.GetState();

            //Compare the escape key to find if it has gone from down to up
            if (keyboardState.IsKeyDown(Keys.Escape) && newKeyboardState.IsKeyUp(Keys.Escape))
                this.Exit();

            //Record the keyboard state
            keyboardState = newKeyboardState;

            //Capture the current mouse state
            MouseState newMouseState = Mouse.GetState();

            //Record the current mouse state
            mouseState = newMouseState;

            //Update the variable blur effect
            variableBlur.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //Get the render target representing the blurred frame
            RenderTarget2D blurredFrame = variableBlur.RenderFrame(GraphicsDevice, spriteBatch, background, gameTime);

            //Set the render target to the screen and draw the blurred frame
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin();
            spriteBatch.Draw(blurredFrame, new Rectangle(0, 0, background.Bounds.Width, background.Bounds.Height), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        
    }
}
