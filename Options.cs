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

using System.Media;
using System.Windows;

namespace _4_Way_Chess
{
    public class Options
    {

        ContentManager Content;
        SpriteFont font;
        public Rectangle[] menuOpt = new Rectangle[1];
        string[] menus = new string[] { "Back" };
        int x1tst;
        SoundEffect MenuNote;
        Color[] clr = new Color[1];
        int g = -1;

        public Options(ContentManager contentManager, SpriteFont fnt)
        {
            font = fnt;
            Content = contentManager;
        }

        public void LoadContent()
        {
            font = Content.Load<SpriteFont>("SpriteFont1");
            MenuNote = Content.Load<SoundEffect>("MenuTone");

            for (int i = 0; i < clr.Count(); i++)
            {
                clr[i] = Color.Black;
            }
        }

        public void Update(GameTime gameTime)
        {
            font = Content.Load<SpriteFont>("SpriteFont1");
            MenuNote = Content.Load<SoundEffect>("MenuTone");

            for (int i = 0; i < menus.Count(); i++)
            {
                menuOpt[i] = new Rectangle((int)(x1tst - ((font.MeasureString(menus[i]).X / 2))), 350 + (i * 60), (int)font.MeasureString(menus[i]).X, 50);
                x1tst = (int)((Game1.testW * Resolution.ratio) / 2);
                if (menuOpt[i].Contains(Game1.mousePoint))
                {
                    clr[i] = Color.White;
                    if (g != i)
                    {
                        MenuNote.Play(0.1f, 0.5f, 0.5f);
                        g = i;
                    }
                }
                else
                {
                    clr[i] = Color.Black;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < menus.Count(); i++)
            {
                spriteBatch.DrawString(font, menus[i], new Vector2(x1tst - ((int)(font.MeasureString(menus[i]).X / 2)), 350 + (i * 60)), clr[i]);
            }
        }

    }
}
