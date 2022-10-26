using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Text;

namespace _4_Way_Chess
{
    class Board
    {
        Texture2D BoardSprite;
        Rectangle BoardRect;

        int M = 420;
        int T = 100;

        public static Rectangle[] WhiteBox = new Rectangle[160];
        int[] i = new int[8];
        int[] ii = new int[14];
       
        public Board(ContentManager contentManager)
        {

            LoadContent(contentManager);


            for (int o = 0; o < 8; o++)
            {
                i[o] = o;
            }
            for (int oo = 0; oo < 14; oo++)
            {
                ii[oo] = oo;
            }





            foreach (int o in i)
            {
                WhiteBox[o] = new Rectangle(M + 230 + o * 55,T + 45, 55, 55);
            }
            foreach (int o in i)
            {
                int r = 8 + o;
                WhiteBox[r] = new Rectangle(M + 230 + o * 55, T + 100, 55, 55);
            }
            foreach (int o in i)
            {
                int r = 16 + o;
                WhiteBox[r] = new Rectangle(M + 230 + o * 55, T + 155, 55, 55);
            }
            foreach (int o in ii)
            {
                int r = 24 + o;
                WhiteBox[r] = new Rectangle(M + 65 + o * 55, T + 210, 55, 55);
            }
            foreach (int o in ii)
            {
                int r = 38 + o;
                WhiteBox[r] = new Rectangle(M + 65 + o * 55, T + 265, 55, 55);
            }
            foreach (int o in ii)
            {
                int r = 52 + o;
                WhiteBox[r] = new Rectangle(M + 65 + o * 55, T + 320, 55, 55);
            }
            foreach (int o in ii)
            {
                int r = 66 + o;
                WhiteBox[r] = new Rectangle(M + 65 + o * 55, T + 375, 55, 55);
            }
            foreach (int o in ii)
            {
                int r = 80 + o;
                WhiteBox[r] = new Rectangle(M + 65 + o * 55, T + 430, 55, 55);
            }
            foreach (int o in ii)
            {
                int r = 94 + o;
                WhiteBox[r] = new Rectangle(M + 65 + o * 55, T + 485, 55, 55);
            }
            foreach (int o in ii)
            {
                int r = 108 + o;
                WhiteBox[r] = new Rectangle(M + 65 + o * 55, T + 540, 55, 55);
            }
            foreach (int o in ii)
            {
                int r = 122 + o;
                WhiteBox[r] = new Rectangle(M + 65 + o * 55, T + 595, 55, 55);
            }


            foreach (int o in i)
            {
                int r = 136 + o;
                WhiteBox[r] = new Rectangle(M + 230 + o * 55, T + 650, 55, 55);
            }
            foreach (int o in i)
            {
                int r = 144 + o;
                WhiteBox[r] = new Rectangle(M + 230 + o * 55, T + 705, 55, 55);
            }
            foreach (int o in i)
            {
                int r = 152 + o;
                WhiteBox[r] = new Rectangle(M + 230 + o * 55, T + 760, 55, 55);
            } 

        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(BoardSprite, BoardRect, Color.White);
         
        }

        public void LoadContent(ContentManager contentManager)
        {
            BoardSprite = contentManager.Load<Texture2D>("BestBoard");
            BoardRect = new Rectangle(M + 65, T + 50, BoardSprite.Width, BoardSprite.Height);
        }
    }
}
