using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using DPSF.ParticleSystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Tutorial_1
{
    class Campfire
    {
        private Texture2D logs;

        // Declare our Particle System variable
        private FireParticleSystem mcParticleSystem = null;

        private Game parentGame;

        public Campfire(Game game)
        {
            parentGame = game;
            // TODO: use this.Content to load your game content here
            logs = parentGame.Content.Load<Texture2D>("logs");

            // Declare a new Particle System instance and Initialize it
            mcParticleSystem = new FireParticleSystem(parentGame);
            mcParticleSystem.AutoInitialize(parentGame.GraphicsDevice, parentGame.Content, null);
        }

        public void UnloadContent()
        {
            // Destroy the Particle System
            mcParticleSystem.Destroy();
        }

        public void Update(GameTime gameTime, Vector3 cameraPos)
        {
            // Update the Particle System
            mcParticleSystem.CameraPosition = cameraPos;
            mcParticleSystem.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void Draw(SpriteBatch batch, Vector3 cameraPos)
        {
            // Set up the Camera's View matrix
            Matrix sViewMatrix = Matrix.CreateLookAt(cameraPos, new Vector3(0, 0, 0), Vector3.Up);

            // Setup the Camera's Projection matrix by specifying the field of view (1/4 pi), aspect ratio, and the near and far clipping planes
            Matrix sProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)parentGame.GraphicsDevice.Viewport.Width / (float)parentGame.GraphicsDevice.Viewport.Height, 1, 10000);


            // Draw the Particle System
            mcParticleSystem.SetWorldViewProjectionMatrices(Matrix.Identity, sViewMatrix, sProjectionMatrix);

            Vector2 scale = new Vector2(0.5f, 0.45f);
            batch.Begin();
            batch.Draw(logs, new Vector2(parentGame.GraphicsDevice.Viewport.Width / 2 - ((logs.Bounds.Width / 2) * scale.X), parentGame.GraphicsDevice.Viewport.Height / 2 - ((logs.Bounds.Height / 2) * scale.Y)), null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
            batch.End();

            mcParticleSystem.Draw();
        }
    }
}
