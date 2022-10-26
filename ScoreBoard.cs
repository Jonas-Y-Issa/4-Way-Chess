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

    public class ScoreBoard
    {
        ContentManager cont;
        Texture2D Cog;
        Rectangle CogHitbox = new Rectangle();
        Texture2D SideBar;
        Rectangle SideBarbox = new Rectangle();

        public static bool Active;

        public ScoreBoard(ContentManager contentManager)
        {
            cont = contentManager;
        }

        public void LoadContent()
        {
            Cog = cont.Load<Texture2D>("score");
            SideBar = cont.Load<Texture2D>("Settings");
        }

        public void Update(GameTime gameTime)
        {
            Cog = cont.Load<Texture2D>("score");
            SideBar = cont.Load<Texture2D>("Settings");

            CogHitbox = new Rectangle(Game1.testW - 180, 5, 80, 80);

            SideBarbox = new Rectangle(CogHitbox.X - SideBar.Width, 0, SideBar.Width, SideBar.Height);
            MB(Game1._currentMouseState1, Game1._previousMouseState1,
               new Point(Game1._currentMouseState1.X, Game1._currentMouseState1.Y), CogHitbox);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Cog, CogHitbox, Color.White);
            if (Active)
            {
                spriteBatch.Draw(SideBar, SideBarbox, Color.White);
            }
        }

        public void MB(MouseState mB, MouseState pMB, Point mP, Rectangle Box)
        {
            if (Box.Contains(mP))
            {
                Cog = cont.Load<Texture2D>("score2");
            }

            if (mB.LeftButton == ButtonState.Released && pMB.LeftButton == ButtonState.Pressed && Box.Contains(mP))
            {
                if (Active)
                {
                    Active = false;
                }
                else
                {
                    Active = true;
                }
            }
        }
    }
}
