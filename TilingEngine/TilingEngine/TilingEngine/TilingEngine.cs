using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TilingEngine
{
    class TilingEngine
    {
        //The random number generator, if needed, for generating a map of random tiles
        private Random rand;

        //The tile width and heigh for a given tile set
        public static readonly int tileWidth = 64;
        public static readonly int tileHeight = 64;

        //The number of rows, columns and total for a given tile set
        public static readonly int tileSetRows = 6;
        public static readonly int tileSetCols = 4;
        public static readonly int tileSetTotal = 24;

        //The texture representing the tile set
        private Texture2D tileSet;

        //A map representing the tiles to be rendered in order of top left to bottom right
        private int[,] map;

        //The width and height of the map
        private int mapWidth;
        private int mapHeight;

        public TilingEngine(ContentManager content, int width, int height)
        {
            //Set the width and height to the user selected values
            mapWidth = width;
            mapHeight = height;

            //Create a random number generator
            rand = new Random();

            //Load the tile set
            tileSet = content.Load<Texture2D>("grass_and_water");

            //Initialize the map
            map = new int[mapWidth, mapHeight];

            //Set each tile to a random value between 0 and 3 (the grass tiles in the test set)
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    map[i, j] = rand.Next(0, 3);
        }

        //Get the location in the tile set for a given row/col tile
        private Vector2 getTileLocation(int tileRow, int tileCol)
        {
            int row = tileRow;
            if (tileRow < 0)
                row = 0;
            else if (tileRow >= tileSetRows)
                row = tileSetRows - 1;

            int col = tileCol;
            if (tileCol < 0)
                col = 0;
            else if (tileCol >= tileSetCols)
                col = tileSetCols - 1;

            return new Vector2(col * tileWidth, row * tileHeight);
        }

        //Get the location in the tile set for a given row/col tile by index ordered top left to bottom right
        private Vector2 getTileLocation(int tileNumber)
        {
            if (tileNumber < 0)
                return getTileLocation(0, 0);
            if (tileSetCols > tileSetTotal)
                return getTileLocation(tileSetRows - 1, tileSetCols - 1);
            else
                return getTileLocation((int)Math.Truncate((double)(tileNumber / tileSetCols)), tileNumber % tileSetCols);
        }

        //Increment the tile number on a location on the map based on pixel coordinates (not tile location)
        public void cycleTileAtLocation(int x, int y)
        {
            //Get grid location
            int j = (int)Math.Ceiling((double)x / (double)tileWidth) - 1;
            int i = ((int)Math.Ceiling((double)y / ((double)tileHeight / 2.0)) - 1) * 2;

            int translated_x = x%tileWidth;
            int translated_y = y%tileHeight;

            //Check top corners
            if (translated_y < Math.Abs(translated_x - tileWidth / 2))
            {
                if(translated_x<tileWidth/2) //Top Left
                {
                    j--;
                    i--;
                }
                else if(translated_x>tileWidth/2) //Top Right
                    i--;
            }
            else if(translated_y > -Math.Abs(translated_x - tileWidth/2)+tileHeight)
            {
                if(translated_x<tileWidth/2) //Bottom Left
                {
                    j--;
                    i++;
                }
                else if (translated_x>tileWidth/2) //Bottom Right
                    i++;
            }

            if (i > 0 && j > 0 && i < map.GetLength(1) && j < map.GetLength(0))
                map[j, i] = (map[j, i] + 1) % tileSetTotal;
        }

        //Draw the tiles in the proper order and alpha blend them together
        public void Draw(SpriteBatch batch, SpriteFont font)
        {
            batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            for (int i = 0; i < map.GetLength(1); i++)
                for (int j = 0; j < map.GetLength(0); j++)
                {
                    Vector2 tileLoc = getTileLocation(map[j, i]);
                    float x = (float)((j - 0) + (double)((double)(i % 2) / 2)) * tileWidth;
                    float y = (float)((i - 0) * tileHeight / 4);
                    if (i % 2 == 1)
                    {
                        batch.Draw(tileSet, new Vector2(x, y), new Rectangle((int)tileLoc.X, (int)tileLoc.Y, tileWidth, tileHeight), Color.White);
                        batch.DrawString(font, j + "," + i, new Vector2(x, y), Color.Red);
                    }
                }

            batch.End();
        }
    }
}
