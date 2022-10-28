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
        ContentManager Content;

        public QueLayout(ContentManager contentManager, string txtTitle, MenuBase[] menuList, string[] strList) : base(contentManager, txtTitle, menuList, strList)
        {
            Content = contentManager;
        }


        public override void LoadContent()
        {
            Que = Content.Load<Texture2D>("Que");
            base.y = Que.Height * 4 + 400;
            base.LoadContent();

        }



        public override void Update(GameTime gameTime)
        {

            for (int i = 0; i < 4; i++)
            {
                QueBox[i] = new Rectangle((int)(Game1.displayWidth * Resolution.ratio / 2) - QueBox[i].Width / 2, 300 + i * 180, Que.Width, Que.Height);

            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(Que, QueBox[i], Color.White);
            }
            base.Draw(spriteBatch);
            /*
            if (Game1.menuEnum == Game1.MenuState.HostPrivate || Game1.menuEnum == Game1.MenuState.HostLan)
            {
                spriteBatch.DrawString(font, menus[0], new Vector2((int)(Game1.displayWidth * Resolution.ratio - font.MeasureString(menus[0]).X - 50), (int)(Game1.displayHeight * Resolution.ratio - 100)), Color.White);
            }
            */
        }
    }
}
