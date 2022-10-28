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
using _4_Way_Chess;


namespace Class.GameSetup
{

    public class QueLayout : MenuBase
    {
        Texture2D Que;
        Rectangle[] QueBox = new Rectangle[4];
        public Rectangle[] menuOpt = new Rectangle[2];
        string[] menus = new string[] { "Start Game", "Back" };
        ContentManager Content;
        SpriteFont font;

        public QueLayout(ContentManager contentManager, string txtTitle, MenuBase[] menuList, string[] strList) : base(contentManager, txtTitle, menuList, strList)
        {
            Content = contentManager;
            title = txtTitle;
            Menus = menuList;
            for (int i = 0; i < menuList.Length; i++)
            {
                text.Add(menuList[i].title);
            }
            for (int i = 0; i < strList.Length; i++)
            {
                text.Add(strList[i]);
            }
        }


        public void LoadContent()
        {
            Que = Content.Load<Texture2D>("Que");
            font = Content.Load<SpriteFont>("SpriteFont1");

        }



        public void Update(GameTime gameTime)
        {

            menuOpt[0] = new Rectangle(50, (int)(Game1.displayHeight * Resolution.ratio - 100), 100, 100);
            menuOpt[1] = new Rectangle((int)(Game1.displayWidth * Resolution.ratio - font.MeasureString(menus[0]).X - 50), (int)(Game1.displayHeight * Resolution.ratio - 100), (int)font.MeasureString(menus[0]).X, (int)font.MeasureString(menus[0]).Y);

            for (int i = 0; i < 4; i++)
            {
                QueBox[i] = new Rectangle((int)(Game1.displayWidth * Resolution.ratio / 2) - QueBox[i].Width / 2, 300 + i * 180, Que.Width, Que.Height);

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(Que, QueBox[i], Color.White);
            }
            /*
            if (Game1.menuEnum == Game1.MenuState.HostPrivate || Game1.menuEnum == Game1.MenuState.HostLan)
            {
                spriteBatch.DrawString(font, menus[0], new Vector2((int)(Game1.displayWidth * Resolution.ratio - font.MeasureString(menus[0]).X - 50), (int)(Game1.displayHeight * Resolution.ratio - 100)), Color.White);
            }
            */
            spriteBatch.DrawString(font, menus[1], new Vector2(50, (int)(Game1.displayHeight * Resolution.ratio - 100)), Color.White);
        }
    }
}
