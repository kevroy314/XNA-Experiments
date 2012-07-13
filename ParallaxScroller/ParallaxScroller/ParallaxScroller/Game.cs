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

namespace ParallaxScroller
{
    /// <summary>
    /// This is the main class.
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Represents the current (sometimes previous) state of the keyboard.
        KeyboardState keyboard_state;
        MouseState mouse_state;

        //Represents the parallax background
        ParallaxBackground background;

        //Font of debug interface
        SpriteFont font;

        //Debug interface enable switch
        bool debugInterfaceEnabled;

        //Represents the users choice as to how many layers of clouds are visible
        int visibleCloudLayers;

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

            //Create the parallax background and add the layers
            background = new ParallaxBackground();
            background.AddLayer(Content.Load<Texture2D>("background"), new Vector2(0.0f, 0.0f), new Vector2(0.0f, 0.0f), true);
            background.AddLayer(Content.Load<Texture2D>("clouds_layer1"), new Vector2(0.0f, 0.0f), new Vector2(-0.5f, -0.4f), true);
            background.AddLayer(Content.Load<Texture2D>("clouds_layer2"), new Vector2(0.0f, 0.0f), new Vector2(-0.7f, -0.5f), true);
            background.AddLayer(Content.Load<Texture2D>("clouds_layer3"), new Vector2(0.0f, 0.0f), new Vector2(-0.4f, -0.3f), true);

            //Load the default font
            font = Content.Load<SpriteFont>("Segoe UI Mono");

            //Debug interface is disable by default
            debugInterfaceEnabled = false;

            //Set the default visibility to all
            visibleCloudLayers = 3;

            //Initialize user input states
            keyboard_state = Keyboard.GetState();
            mouse_state = Mouse.GetState();
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

            //Capture the new keyboard state
            KeyboardState current_keyboard_state = Keyboard.GetState(); ;

            //Compare the escape key to find if it has gone from down to up
            if (keyboard_state.IsKeyDown(Keys.Escape) && current_keyboard_state.IsKeyUp(Keys.Escape))
                this.Exit();

            //Toggle the debug enable when the ~ key is pressed
            if (keyboard_state.IsKeyUp(Keys.OemTilde) && current_keyboard_state.IsKeyDown(Keys.OemTilde))
                debugInterfaceEnabled = !debugInterfaceEnabled;

            //Record the new keyboard state
            keyboard_state = current_keyboard_state;

            //Capture the new keyboard state
            MouseState current_mouse_state = Mouse.GetState();

            //Adjust the cloud visibility based on the mouse wheel
            if (mouse_state.ScrollWheelValue > current_mouse_state.ScrollWheelValue)
                visibleCloudLayers--;
            else if (mouse_state.ScrollWheelValue < current_mouse_state.ScrollWheelValue)
                visibleCloudLayers++;

            //Coerce the cloud visibility to the appropriate bounds
            visibleCloudLayers = visibleCloudLayers < 0 ? 0 : visibleCloudLayers > background.NumberOfLayers ? background.NumberOfLayers : visibleCloudLayers;

            //Set the bottom layers visible
            background.SetVisibilityRange(0, visibleCloudLayers, true);

            //Set the top layers invisible
            if (visibleCloudLayers != background.NumberOfLayers - 1)
                background.SetVisibilityRange(visibleCloudLayers + 1, background.NumberOfLayers - 1, false);

            //Record the new mouse state
            mouse_state = current_mouse_state;

            //Iterate the background
            background.IteratePositions(1);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //Clear the background
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Draw the background
            background.Draw(GraphicsDevice, spriteBatch);

            //Draw the debug interface if it is enabled
            if(debugInterfaceEnabled)
                background.DrawOverlay(GraphicsDevice, spriteBatch, font);

            base.Draw(gameTime);
        }
    }
}
