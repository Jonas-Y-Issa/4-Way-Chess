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
using System.Reflection.Metadata;


namespace _4_Way_Chess
{

    public class StartMenu
    {
        ContentManager cont;
        Texture2D Header;
        Rectangle HeaderBox;
        Texture2D WallpaperTexture;
        Rectangle Wallpaper = new Rectangle();
        Color tint = Color.White;
        double currPos;
        double targPos;
        float t = 0;
        Vector2 VersionPosition;
        Vector2 CreditPosition;
        float Scale = 0.5f;
        float g = 0;
        SpriteFont corp;
        SpriteFont Arial;


        public StartMenu(ContentManager contentManager)
        {
            cont = contentManager;
        }


        public void LoadContent()
        {
            Header = cont.Load<Texture2D>("Header");
            WallpaperTexture = cont.Load<Texture2D>("BackgroundTest");
            corp = cont.Load<SpriteFont>("Corporation");
            Arial = cont.Load<SpriteFont>("Arial");

        }


        public void Update(GameTime gameTime)
        {
            HeaderBox = new Rectangle((int)((Game1.displayWidth * Resolution.ratio) / 2) - (int)((HeaderBox.Width / 2)), 15, Header.Width / 3, Header.Height / 3);
            Wallpaper = new Rectangle((int)(-(WallpaperTexture.Width * 1.5)+ (Game1.displayWidth * Resolution.ratio)+t), (int)(-(WallpaperTexture.Height * 1.5) + (Game1.displayHeight * Resolution.ratio) + g), (int)(WallpaperTexture.Width * 1.5), (int)(WallpaperTexture.Height * 1.5));

            if (Game1.Active.title == "Private")
            {
                tint = Color.Yellow;
            }
            else
            {
                tint = Color.White;
            }
            g += 2;
            t += 2;
            currPos = (Math.Sqrt(Math.Pow(g, 2) + Math.Pow(t, 2)));
            targPos = Math.Sqrt(Math.Pow(WallpaperTexture.Width * 1.5, 2) + Math.Pow(WallpaperTexture.Height * 1.5, 2)) / 2;

            if (currPos > targPos)
            {
                t = 0;
                g = 0;
            }
            CreditPosition = new Vector2(15, Game1.displayHeight * Resolution.ratio - 50);

            VersionPosition = new Vector2(Game1.displayWidth * Resolution.ratio - 200, Game1.displayHeight * Resolution.ratio - 50);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(WallpaperTexture, Wallpaper, tint);
            spriteBatch.Draw(Header, HeaderBox, Color.White);
            spriteBatch.DrawString(corp, "Game by: Younes Issa", CreditPosition, Color.White, new float(),new Vector2(), Scale,new SpriteEffects(), new float());

            spriteBatch.DrawString(Arial, "Beta V6.20", VersionPosition, Color.Red);

        }
    }
}
