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
using System.Collections;


namespace _4_Way_Chess
{

    public class QueLayout
    {
        Texture2D Que;
        Rectangle[] QueBox = new Rectangle[4];
        public Rectangle[] menuOpt = new Rectangle[2];
        string[] menus = new string[] { "Start Game", "Back" };
        ContentManager Content;
        SpriteFont font;
        SoundEffect MenuNote;
        Color[] clr = new Color[4];
        int x1tst;


        public QueLayout(ContentManager contentManager, SpriteFont fnt)
        {
            Content = contentManager;
            font = fnt;
        }


        public void LoadContent()
        {
            font = Content.Load<SpriteFont>("SpriteFont1");
            MenuNote = Content.Load<SoundEffect>("MenuTone");
            Que = Content.Load<Texture2D>("Que");
            for (int i = 0; i < clr.Count(); i++)
            {
                clr[i] = Color.Black;
            }
        }



        public void Update(GameTime gameTime)
        {
            Que = Content.Load<Texture2D>("Que");
            font = Content.Load<SpriteFont>("SpriteFont1");
            x1tst = (int)((Game1.testW * Resolution.ratio) / 2);

           
            menuOpt[0] = new Rectangle(50, (int)((Game1.testH * Resolution.ratio) - 100), 100, 100);
            menuOpt[1] = new Rectangle((int)((Game1.testW * Resolution.ratio) - font.MeasureString(menus[0]).X - 50), (int)((Game1.testH * Resolution.ratio) - 100), (int)font.MeasureString(menus[0]).X, (int)font.MeasureString(menus[0]).Y);
           
            for (int i = 0; i < 4; i++)
            {
                QueBox[i] = new Rectangle((int)((Game1.testW * Resolution.ratio) / 2) - (int)((QueBox[i].Width / 2)), 300 + (i * 180), Que.Width, Que.Height);
           
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(Que, QueBox[i], Color.White);
            }
            if (Game1.menuEnum == Game1.MenuState.HostPrivate || Game1.menuEnum == Game1.MenuState.HostLan)
            {
              spriteBatch.DrawString(font, menus[0], new Vector2((int)((Game1.testW * Resolution.ratio) - font.MeasureString(menus[0]).X - 50), (int)((Game1.testH * Resolution.ratio) - 100)), Color.White);
            }
                spriteBatch.DrawString(font, menus[1], new Vector2(50, (int)((Game1.testH * Resolution.ratio) - 100)), Color.White);
        }
    }
}
