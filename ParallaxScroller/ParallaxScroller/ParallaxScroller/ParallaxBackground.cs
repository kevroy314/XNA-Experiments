using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ParallaxScroller
{
    class ParallaxBackground
    {
        //List of the texture layers
        private List<Texture2D> layers;
        //List of the texture positions
        private List<Vector2> layerPositions;
        //List of the texture velocities
        private List<Vector2> layerVelocities;
        //List of the texture visibilities
        private List<bool> layerVisibilities;

        //Constructor initializes various lists
        public ParallaxBackground()
        {
            layers = new List<Texture2D>();
            layerPositions = new List<Vector2>();
            layerVelocities = new List<Vector2>();
            layerVisibilities = new List<bool>();
        }

        //Add a new layer with initial properties and return the index in which it was placed
        public int AddLayer(Texture2D layer, Vector2 initialPosition, Vector2 initialVelocity, bool initialVisibility)
        {
            layers.Add(layer);
            layerPositions.Add(initialPosition);
            layerVelocities.Add(initialVelocity);
            layerVisibilities.Add(initialVisibility);
            return layers.Count;
        }

        //Remove a layer
        public void RemoveLayer(Texture2D layer)
        {
            int index = layers.IndexOf(layer);
            RemoveLayer(index);
        }

        //Remove a layer in an index
        public void RemoveLayer(int layerIndex)
        {
            layers.RemoveAt(layerIndex);
            layerPositions.RemoveAt(layerIndex);
            layerVelocities.RemoveAt(layerIndex);
            layerVisibilities.RemoveAt(layerIndex);
        }

        //Property node to get the number of layers present
        public int NumberOfLayers
        {
            get { return layers.Count; }
        }

        //Change the visibility of a specific layer
        public void ChangeLayerVisibility(Texture2D layer, bool visibility)
        {
            int index = layers.IndexOf(layer);
            ChangeLayerVisibility(index, visibility);
        }

        //Change the visibility of a specific layer at a specific index
        public void ChangeLayerVisibility(int layerIndex, bool visibility)
        {
            layerVisibilities[layerIndex] = visibility;
        }

        //Toggle the visibility of a layer
        public void ToggleLayerVisibility(Texture2D layer)
        {
            int index = layers.IndexOf(layer);
            ToggleLayerVisibility(index);
        }

        //Toggle the visibility of a layer at a specific index
        public void ToggleLayerVisibility(int layerIndex)
        {
            layerVisibilities[layerIndex] = !layerVisibilities[layerIndex];
        }

        //Toggle all layers visibilities
        public void ToggleLayerVisibilities()
        {
            for (int i = 0; i < layers.Count; i++)
                layerVisibilities[i] = !layerVisibilities[i];
        }

        //Set the visibility of a range of layers
        public void SetVisibilityRange(int min, int max, bool visibility)
        {
            for (int i = min; i <= max && i < layers.Count; i++)
                ChangeLayerVisibility(i, visibility);
        }

        //Change the position of a layer
        public void ChangeLayerPosition(Texture2D layer, Vector2 position)
        {
            int index = layers.IndexOf(layer);
            ChangeLayerPosition(index, position);
        }
        
        //Change the position of a layer at a specific index
        public void ChangeLayerPosition(int layerIndex, Vector2 position)
        {
            layerPositions[layerIndex] = position;
        }

        //Change the velocity of a layer
        public void ChangeLayerVelocity(Texture2D layer, Vector2 velocity)
        {
            int index = layers.IndexOf(layer);
            ChangeLayerVelocity(index, velocity);
        }

        //Change the velocity of a layer at a specific index
        public void ChangeLayerVelocity(int layerIndex, Vector2 velocity)
        {
            layerVelocities[layerIndex] = velocity;
        }

        //Add velocity to a layer
        public void AddLayerVelocity(Texture2D layer, Vector2 velocityModifier)
        {
            int index = layers.IndexOf(layer);
            AddLayerVelocity(index, velocityModifier);
        }

        //Add velocity to a layer at a specific index
        public void AddLayerVelocity(int layerIndex, Vector2 velocityModifier)
        {
            layerVelocities[layerIndex] += velocityModifier;
        }

        //Add velocities to all layers
        public void AddLayerVelocities(Vector2 velocityModifier)
        {
            for (int i = 0; i < layers.Count; i++)
                layerVelocities[i] += velocityModifier;
        }

        //Subtract velocity from a layer
        public void SubLayerVelocity(Texture2D layer, Vector2 velocityModifier)
        {
            int index = layers.IndexOf(layer);
            SubLayerVelocity(index, velocityModifier);
        }

        //Subtract velocity from a layer at a specific index
        public void SubLayerVelocity(int layerIndex, Vector2 velocityModifier)
        {
            layerVelocities[layerIndex] -= velocityModifier;
        }

        //Subtract velocities from all layers
        public void SubLayerVelocities(Vector2 velocityModifier)
        {
            for (int i = 0; i < layers.Count; i++)
                layerVelocities[i] -= velocityModifier;
        }

        //Multiply velocity of a layer
        public void MulLayerVelocity(Texture2D layer, Vector2 velocityModifier)
        {
            int index = layers.IndexOf(layer);
            MulLayerVelocity(index, velocityModifier);
        }

        //Multiply velocity of a layer at a specific index
        public void MulLayerVelocity(int layerIndex, Vector2 velocityModifier)
        {
            layerVelocities[layerIndex] *= velocityModifier;
        }

        //Multiply velocities of all layers
        public void MulLayerVelocities(Vector2 velocityModifier)
        {
            for (int i = 0; i < layers.Count; i++)
                layerVelocities[i] *= velocityModifier;
        }

        //Divide velocity of a layer
        public void DivLayerVelocity(Texture2D layer, Vector2 velocityModifier)
        {
            int index = layers.IndexOf(layer);
            DivLayerVelocity(index, velocityModifier);
        }

        //Divide velocity of a layer at a specific index
        public void DivLayerVelocity(int layerIndex, Vector2 velocityModifier)
        {
            layerVelocities[layerIndex] /= velocityModifier;
        }

        //Divide velocities of all layers
        public void DivLayerVelocities(Vector2 velocityModifier)
        {
            for (int i = 0; i < layers.Count; i++)
                layerVelocities[i] /= velocityModifier;
        }

        //Iterate the position of a layer based on it's internal velocity
        public void IterateLayerPosition(Texture2D layer, float dt)
        {
            int index = layers.IndexOf(layer);
            IterateLayerPosition(index, dt);
        }

        //Iterate the position of a layer at a specific index based on it's internal velocity
        public void IterateLayerPosition(int layerIndex, float dt)
        {
            layerPositions[layerIndex] += layerVelocities[layerIndex] * dt;
            layerPositions[layerIndex] = new Vector2(layerPositions[layerIndex].X % (float)layers[layerIndex].Bounds.Width, layerPositions[layerIndex].Y % (float)layers[layerIndex].Bounds.Height);
        }

        //Iterate the positions of all layers based on their internal velocity
        public void IteratePositions(float dt)
        {
            for (int i = 0; i < layers.Count; i++)
                IterateLayerPosition(i, dt);
        }

        //Draw the layers, tiling each around the viewport
        public void Draw(GraphicsDevice device, SpriteBatch batch)
        {
            batch.Begin(SpriteSortMode.Immediate,BlendState.AlphaBlend,SamplerState.LinearWrap,DepthStencilState.Default,RasterizerState.CullNone);
            Rectangle destRect = device.Viewport.Bounds;
            for (int i = 0; i < layers.Count; i++)
                if (layerVisibilities[i])
                {
                    destRect.Offset((int)layerPositions[i].X, (int)layerPositions[i].Y);
                    destRect = Rectangle.Union(device.Viewport.Bounds, destRect);
                    batch.Draw(layers[i], layerPositions[i], destRect, Color.White, 0, layerPositions[i], 1, SpriteEffects.None, 0);
                }
            batch.End();
        }

        //Draw a position overlay for debugging
        public void DrawOverlay(GraphicsDevice device, SpriteBatch batch, SpriteFont font)
        {
            batch.Begin();
            for (int i = 0; i < layerPositions.Count; i++)
                batch.DrawString(font, "LAY_" + i +
                    "[VIS: " + layerVisibilities[i].ToString() + ", " +
                    "VEL(" + layerVelocities[i].X.ToString("0.00") + "," + layerVelocities[i].Y.ToString("0.00") + "), " +
                    "POS(" + layerPositions[i].X.ToString("0.00") + "," + layerPositions[i].Y.ToString("0.00") + ")]",
                    new Vector2(5.0f, 5.0f + i * 15.0f), Color.Red);
            batch.End();
        }
    }
}
